using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests
{

    public abstract class TestBase
    {

        #region Fields

        private readonly Random _Random;

        #endregion

        #region Constructors

        protected TestBase()
        {
            this._Random = new Random();
        }

        #endregion

        #region Properties

        public bool CanGuiDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        #endregion

        #region Methods

        public void DisposeAndCheckDisposedState(DlibObject obj)
        {
            obj.Dispose();
            Assert.IsTrue(obj.IsDisposed);
            Assert.IsTrue(obj.NativePtr == IntPtr.Zero);
        }

        public void DisposeAndCheckDisposedStates(IEnumerable<DlibObject> objs)
        {
            foreach (var obj in objs)
                this.DisposeAndCheckDisposedState(obj);
        }

        public static void DoTest<T>(Func<bool, T> outputImageFunc, bool expect, Action<T> successAction, Action finallyAction, Action failAction, Action exceptionAction)
            where T : class 
        {
            try
            {
                try
                {
                    var outputImage = outputImageFunc(expect);

                    if (!expect)
                    {
                        failAction();
                    }
                    else
                    {
                        successAction(outputImage);
                    }
                }
                catch (ArgumentException)
                {
                    if (!expect)
                        Console.WriteLine("OK");
                    else
                        throw;
                }
                catch (NotSupportedException)
                {
                    if (!expect)
                        Console.WriteLine("OK");
                    else
                        throw;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                exceptionAction();
                throw;
            }
            finally
            {
                finallyAction();
            }
        }

        public FileInfo GetDataFile(string filename)
        {
            return new FileInfo(Path.Combine("data", filename));
        }

        public IEnumerable<FileInfo> GetDataFiles(string directoryName)
        {
            var dir = new DirectoryInfo(Path.Combine("data", directoryName));
            return dir.GetFiles();
        }

        public string GetOutDir(string function)
        {
            var path = Path.Combine("out", function);
            Directory.CreateDirectory(path);
            return path;
        }

        public string GetOutDir(params string[] function)
        {
            var path = Path.Combine("out", Path.Combine(function));
            Directory.CreateDirectory(path);
            return path;
        }

        public int NextRandom()
        {
            return this._Random.Next();
        }

        public int NextRandom(int maxValue)
        {
            return this._Random.Next(maxValue);
        }

        public int NextRandom(int minValue, int maxValue)
        {
            return this._Random.Next(minValue, maxValue);
        }

        public double NextDoubleRandom()
        {
            return this._Random.NextDouble();
        }

        #endregion

    }

}
