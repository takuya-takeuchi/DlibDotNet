/*
 * This sample program is ported by C# from examples\logger_custom_output_ex.cpp.
*/

using System;
using System.IO;
using DlibDotNet;

namespace LoggerCustomOutput
{

    internal class Program
    {

        #region Fields

        private static readonly Logger LogP = new Logger("example");

        private static readonly Logger LogT = new Logger("example.thread");

        private static readonly Logger LogC = new Logger("example.test_class");

        #endregion

        #region Methods

        private static void Main()
        {
            using (var hook = new MyHook())
            {
                // This tells all dlib loggers to send their logging events to the hook object.  That
                // is, any time a logger generates a message it will call hook.log() with the message
                // contents.  Additionally, hook.log() will also only be called from one thread at a
                // time so it is safe to use this kind of hook in a multi-threaded program with many
                // loggers in many threads.
                Dlib.SetAllLoggingOutputHooks(hook);
                // It should also be noted that the hook object must not be destructed while the
                // loggers are still in use.  So it is a good idea to declare the hook object 
                // somewhere where it will live the entire lifetime of the program, as we do here.


                using (var dlog = new Logger("main"))
                {
                    // Tell the dlog logger to emit a message for all logging events rather than its
                    // default behavior of only logging LERROR or above. 
                    dlog.SetLevel(LogLevel.All);

                    // All these message go to my_log_file.txt, but only the last two go to the console.
                    dlog.WriteLine(LogLevel.Debug, "This is a debugging message.");
                    dlog.WriteLine(LogLevel.Info, "This is an informational message.");
                    dlog.WriteLine(LogLevel.Error, "An error message!");
                }
            }
        }

        #endregion

        private sealed class MyHook : CustomLogger
        {

            #region Fields

            private readonly FileStream _FileStream;

            private readonly StreamWriter _StreamWriter;

            #endregion

            #region Constructors

            public MyHook()
            {
                this._FileStream = new FileStream("my_log_file.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                this._StreamWriter = new StreamWriter(this._FileStream);
            }

            #endregion

            #region Methods

            #region Overrids

            public override void Log(string logName, LogLevel logLevel, string levelName, ulong threadId, string message)
            {
                // Log all messages from any logger to our log file.
                this._StreamWriter.WriteLine($"{levelName} [{threadId}] {logName}: {message}");

                // But only log messages that are of LINFO priority or higher to the console.
                if (logLevel >= LogLevel.Info)
                    Console.WriteLine($"{levelName} [{threadId}] {logName}: {message}");
            }

            /// <summary>
            /// Releases all unmanaged resources.
            /// </summary>
            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                this._StreamWriter.Dispose();
                this._FileStream.Dispose();
            }

            #endregion

            #endregion

        }

    }

}