/*
 * This sample program is ported by C# from examples\pipe_ex.cpp.
*/

using System;
using System.Runtime.InteropServices;
using DlibDotNet;

namespace Pipe
{

    internal class Program
    {

        #region Fields

        private static readonly Logger DLog = new Logger("pipe_example");

        #endregion

        #region Constructors

        static Program()
        {
            ContainerBridgeRepository.Add(new JobContainerBridge());
        }

        #endregion

        #region Methods

        private static void Main()
        {
            // Set the dlog object so that it logs everything.
            DLog.SetLevel(LogLevel.All);

            using (var pe = new PipeExample())
            {
                for (var i = 0; i < 15; ++i)
                {
                    DLog.WriteLine(LogLevel.Info, $"Add job {i} to pipe");
                    var j = new Job
                    {
                        Id = i
                    };

                    // Add this job to the pipe.  One of our three threads will get it and process it.
                    // It should also be pointed out that the enqueue() function uses the global
                    // swap function to move jobs into the pipe.  This means that it modifies the
                    // jobs we are passing in to it.  This allows you to implement a fast swap 
                    // operator for your jobs.  For example, std::vector objects have a global
                    // swap and it can execute in constant time by just swapping pointers inside 
                    // std::vector.  This means that the dlib::pipe is effectively a zero-copy 
                    // message passing system if you setup global swap for your jobs.   
                    pe.JobPipe.Enqueue(j);
                }

                DLog.WriteLine(LogLevel.Info, "main ending");

                // the main function won't really terminate here.  It will call the destructor for pe
                // which will block until all the jobs have been processed.
            }
        }

        #endregion

        private sealed class PipeExample : CustomMultithreadedObject
        {

            #region Fields

            private readonly VoidActionMediator _ActionMediator;

            #endregion

            #region Constructors

            public PipeExample()
            {
                // This 4 here is the size of our job_pipe.The significance is that
                // if you try to enqueue more than 4 jobs onto the pipe then enqueue() will
                // block until there is room.  
                this.JobPipe = new Pipe<Job>(4);

                this._ActionMediator = new VoidActionMediator(this.Thread);

                // register 3 threads
                this.RegisterThread(this._ActionMediator);
                this.RegisterThread(this._ActionMediator);
                this.RegisterThread(this._ActionMediator);

                // start the 3 threads we registered above
                this.Start();
            }

            #endregion

            #region Properties

            public Pipe<Job> JobPipe
            {
                get;
            }

            #endregion

            #region Methods

            #region Helpers

            private void Thread()
            {
                // Here we loop on jobs from the job_pipe.  
                while (this.JobPipe.Dequeue(out var j))
                {
                    // process our job j in some way.
                    DLog.WriteLine(LogLevel.Info, $"got job {j.Id}");

                    // sleep for 0.1 seconds
                    Dlib.Sleep(100);
                }

                DLog.WriteLine(LogLevel.Info, "thread ending");
            }

            #endregion

            #region Overrids

            protected override void DisposingUnmanaged()
            {
                base.DisposingUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;
                
                DLog.WriteLine(LogLevel.Info, "destructing pipe object: wait for job_pipe to be empty");
                // wait for all the jobs to be processed
                this.JobPipe.WaitUntilEmpty();

                DLog.WriteLine(LogLevel.Info, "destructing pipe object: job_pipe is empty");

                // now disable the job_pipe.  doing this will cause all calls to 
                // job_pipe.dequeue() to return false so our threads will terminate
                this.JobPipe.Disable();

                // now block until all the threads have terminated
                this.Wait();
                DLog.WriteLine(LogLevel.Info, "destructing pipe object: all threads have ended");
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                this.JobPipe.Dispose();
                this._ActionMediator.Dispose();
            }

            #endregion

            #endregion

        }

        private sealed class Job
        {

            #region Constructors

            public Job()
            {
                this.NativePtr = Marshal.AllocCoTaskMem(sizeof(int));
            }

            public Job(IntPtr ptr)
            {
                this.NativePtr = ptr;
            }

            #endregion

            #region Properties

            public IntPtr NativePtr
            {
                get;
            }

            public int Id
            {
                get
                {
                    var ret = Marshal.ReadInt32(this.NativePtr, 0);
                    return ret;
                }
                set
                {
                    Marshal.WriteInt32(this.NativePtr, 0, value);
                }
            }

            #endregion

        }

        private sealed class JobContainerBridge : ContainerBridge<Job>
        {

            #region Methods

            #region Overrids

            public override Job Create(IntPtr ptr, IParameter parameter = null)
            {
                return new Job(ptr);
            }

            public override IntPtr GetPtr(Job item)
            {
                return item.NativePtr;
            }

            #endregion

            #endregion

        }

    }

}