using Microsoft.Extensions.Logging;
using System;
using static System.Console;

namespace Working_With_EFCore
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class ConsoleLogger : ILogger
    {
        // if logger used unmanaged recourses, return the class that implements IDisposable
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // to avoid overlogging, you can filter on the log level
            switch (logLevel)
            {
                case LogLevel.Trace:        // detailed messages (NEVER RELEASE IN PRODUCTION)
                case LogLevel.Information:  // general use, long term value
                case LogLevel.None:         // Not used for writing log messages. Specifies that a logging category should not write any messages.
                    return false;
                case LogLevel.Debug:        // used for interactive investigation during development. Useful for debugging, no long-term value.
                case LogLevel.Warning:      // highlight an abnormal or unexpected event in the application flow
                case LogLevel.Error:        // highlight when the current flow of execution is stopped due to a failure
                case LogLevel.Critical:     // unrecoverable application or system crash, catastrophic failure that requires immediate attention.
                default:
                    return true;
            };
        }

        public void Log<TState>(LogLevel LogLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (eventId.Id == 20101)    // displays SQL statements to gather DB data in console
            {
                // log the level and event identifier
                Write($"Level: {LogLevel}, Event ID: {eventId.Id}");
                // only output the state or exception if it exists
                if (state != null)
                {
                    Write($", State: {state}");
                }
                if (exception != null)
                {
                    Write($", Exception: {exception.Message}");
                }
                WriteLine();
            }
        }
    }
}