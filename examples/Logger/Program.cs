/*
 * This sample program is ported by C# from examples\logger_ex.cpp.
*/

using DlibDotNet;

namespace Logger
{

    internal class Program
    {

        #region Fields

        private static readonly DlibDotNet.Logger DLog = new DlibDotNet.Logger("example");

        #endregion

        #region Methods

        private static void Main()
        {
            // Every logger has a logging level (given by dlog.level()).  Each log message is tagged with a
            // level and only levels equal to or higher than dlog.level() will be printed.  By default all 
            // loggers start with level() == LERROR.  In this case I'm going to set the lowest level LALL 
            // which means that dlog will print all logging messages it gets.
            DLog.SetLevel(LogLevel.All);


            // print our first message.  It will go to cout because that is the default.
            DLog.WriteLine(LogLevel.Info, "This is an informational message.");

            // now print a debug message.
            var variable = 8;
            DLog.WriteLine(LogLevel.Debug, $"The integer variable is set to {variable}");

            // the logger can be used pretty much like any ostream object.  But you have to give a logging
            // level first.  But after that you can chain << operators like normal.

            if (variable > 4)
                DLog.WriteLine(LogLevel.Warn, $"The variable is bigger than 4!  Its value is {variable}");



            DLog.WriteLine(LogLevel.Info, "we are going to sleep for half a second.");
            // sleep for half a second
            Dlib.Sleep(500);
            DLog.WriteLine(LogLevel.Info, "we just woke up");



            DLog.WriteLine(LogLevel.Info, "program ending");
        }

        #endregion

    }

}