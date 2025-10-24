using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealisticExampleProject.Services
{
    public interface IApplicationLogger
    {
        IDisposable BeginScope<TState>(TState state);
        bool IsEnabled(LogLevel logLevel);
        ILogResult Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
    public class ApplicationLogger : IApplicationLogger
    {        
        private readonly IApplicationLoggerProvider LoggerProvider;
        private readonly string CategoryName;
        private readonly LogLevel LogLevel;
        public ApplicationLogger(IApplicationLoggerProvider loggerProvider, string categoryName, LogLevel logLevel) {
            LoggerProvider = loggerProvider;
            CategoryName = categoryName;
            LogLevel = logLevel;
        }
        
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        => logLevel >= LogLevel;
        
        public ILogResult Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return null;
            }
            return LoggerProvider.SendLog(CategoryName, Format(DateTime.Now, logLevel, CategoryName, state, exception, formatter));
        }
        private string Format<TState>(DateTime logged, LogLevel logLevel, string category, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = $"[{logged.ToString("yyyy-MM-dd HH:mm:ss.fff zzz")}]: {("[" + logLevel.ToString() + "] ")}{category} {formatter(state, exception)}";
            if (exception != null)
            {
                message += Environment.NewLine + exception.ToString();
            }
            return message;
        }
    }
    

    
}