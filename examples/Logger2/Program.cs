/*
 * This sample program is ported by C# from examples\logger_ex_2.cpp.
*/

using System;
using DlibDotNet;

namespace Logger2
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
            SetupLoggers();

            // print our first message.  It will go to cout because that is the default.
            LogP.WriteLine(LogLevel.Info, "This is an informational message.");

            var variable = 8;

            // Here is a debug message.  It won't print though because its log level is too low (it is below LINFO).
            LogP.WriteLine(LogLevel.Debug, $"The integer variable is set to {variable}");


            if (variable > 4)
                LogP.WriteLine(LogLevel.Warn, $"The variable is bigger than 4!  Its value is {variable}");

            LogP.WriteLine(LogLevel.Info, "make two threads");
            var action = new Dlib.ThreadAction(Thread);
            Dlib.CreateNewThread(action, new IntPtr(0));
            Dlib.CreateNewThread(action, new IntPtr(0));

            var myTest = new Test();
            myTest.Warning();

            LogP.WriteLine(LogLevel.Info, "we are going to sleep for half a second.");
            // sleep for half a second
            Dlib.Sleep(500);
            LogP.WriteLine(LogLevel.Info, "we just woke up");



            LogP.WriteLine(LogLevel.Info, "program ending");


            // It is also worth pointing out that the logger messages are atomic.  This means, for example, that
            // in the above log statements that involve a string literal and a variable, no other thread can
            // come in and print a log message in-between the literal string and the variable.  This is good
            // because it means your messages don't get corrupted.  However, this also means that you shouldn't 
            // make any function calls inside a logging statement if those calls might try to log a message 
            // themselves since the atomic nature of the logger would cause your application to deadlock.
        }

        #region Helpers

        private static void Thread(IntPtr obj)
        {
            LogT.WriteLine(LogLevel.Info, "entering our thread");


            var myTest = new Test();
            myTest.Warning();

            Dlib.Sleep(200);

            LogT.WriteLine(LogLevel.Info, "exiting our thread");
        }
        
        private static void SetupLoggers()
        {
            // Create a logger that has the same name as our root logger logp.  This isn't very useful in 
            // this example program but if you had loggers defined in other files then you might not have
            // easy access to them when starting up your program and setting log levels.  This mechanism
            // allows you to manipulate the properties of any logger so long as you know its name.
            using (var tempLog = new Logger("example"))
            {
                // For this example I don't want to log debug messages so I'm setting the logging level of 
                // All our loggers to LINFO.  Note that this statement sets all three of our loggers to this
                // logging level because they are all children of temp_log.   
                tempLog.SetLevel(LogLevel.Info);


                // In addition I only want the example.test_class to print LWARN or higher messages so I'm going
                // to set that here too.  Note that we set this value after calling temp_log.set_level(). If we 
                // did it the other way around the set_level() call on temp_log would set logc_temp.level() and 
                // logc.level() back to LINFO since temp_log is a parent of logc_temp.
                using (var logcTemp = new Logger("example.test_class"))
                    logcTemp.SetLevel(LogLevel.Warn);


                // Finally, note that you can also configure your loggers from a text config file.  
                // See the documentation for the configure_loggers_from_file() function for details.
            }
        }

        #endregion

        #endregion

        private sealed class Test
        {

            #region Constructors

            public Test()
            {
                // this message won't get logged because LINFO is too low
                LogC.WriteLine(LogLevel.Info, "constructed a test object");
            }

            ~Test()
            {
                // this message won't get logged because LINFO is too low
                LogC.WriteLine(LogLevel.Info, "destructed a test object");
            }

            #endregion

            #region Methods

            public void Warning()
            {
                LogC.WriteLine(LogLevel.Warn, "warning!  someone called warning()!");
            }

            #endregion

        }

    }

}