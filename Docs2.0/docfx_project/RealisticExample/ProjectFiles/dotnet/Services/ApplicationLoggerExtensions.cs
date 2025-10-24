using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace RealisticExampleProject.Services
{
    public static class ApplicationLoggerExtensions
    {
        #region Debug
        public static ILogResult LogDebug(this IApplicationLogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Debug, eventId, exception, message, args);
        }
        
        public static ILogResult LogDebug(this IApplicationLogger logger, EventId eventId, string message, params object[] args)
        {
            return logger.Log(LogLevel.Debug, eventId, message, args);
        }
        
        public static ILogResult LogDebug(this IApplicationLogger logger, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Debug, exception, message, args);
        }
        
        public static ILogResult LogDebug(this IApplicationLogger logger, string message, params object[] args)
        {
            return logger.Log(LogLevel.Debug, message, args);
        }
        #endregion
        #region Trace
        
        public static ILogResult LogTrace(this IApplicationLogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Trace, eventId, exception, message, args);
        }
        
        public static ILogResult LogTrace(this IApplicationLogger logger, EventId eventId, string message, params object[] args)
        {
            return logger.Log(LogLevel.Trace, eventId, message, args);
        }

        
        public static ILogResult LogTrace(this IApplicationLogger logger, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Trace, exception, message, args);
        }
        
        public static ILogResult LogTrace(this IApplicationLogger logger, string message, params object[] args)
        {
            return logger.Log(LogLevel.Trace, message, args);
        }
        #endregion
        #region Information
        public static ILogResult LogInformation(this IApplicationLogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Information, eventId, exception, message, args);
        }
        
        public static ILogResult LogInformation(this IApplicationLogger logger, EventId eventId, string message, params object[] args)
        {
            return logger.Log(LogLevel.Information, eventId, message, args);
        }
        
        public static ILogResult LogInformation(this IApplicationLogger logger, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Information, exception, message, args);
        }
        
        public static ILogResult LogInformation(this IApplicationLogger logger, string message, params object[] args)
        {
            return logger.Log(LogLevel.Information, message, args);
        }
        #endregion
        #region Warning
        public static ILogResult LogWarning(this IApplicationLogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Warning, eventId, exception, message, args);
        }
        
        public static ILogResult LogWarning(this IApplicationLogger logger, EventId eventId, string message, params object[] args)
        {
            return logger.Log(LogLevel.Warning, eventId, message, args);
        }
        
        public static ILogResult LogWarning(this IApplicationLogger logger, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Warning, exception, message, args);
        }
        
        public static ILogResult LogWarning(this IApplicationLogger logger, string message, params object[] args)
        {
            return logger.Log(LogLevel.Warning, message, args);
        }
        #endregion
        #region Error
        public static ILogResult LogError(this IApplicationLogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Error, eventId, exception, message, args);
        }
        
        public static ILogResult LogError(this IApplicationLogger logger, EventId eventId, string message, params object[] args)
        {
            return logger.Log(LogLevel.Error, eventId, message, args);
        }
        
        public static ILogResult LogError(this IApplicationLogger logger, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Error, exception, message, args);
        }
        
        public static ILogResult LogError(this IApplicationLogger logger, string message, params object[] args)
        {
            return logger.Log(LogLevel.Error, message, args);
        }
        #endregion
        #region Critical
        public static ILogResult LogCritical(this IApplicationLogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Critical, eventId, exception, message, args);
        }
        
        public static ILogResult LogCritical(this IApplicationLogger logger, EventId eventId, string message, params object[] args)
        {
            return logger.Log(LogLevel.Critical, eventId, message, args);
        }
        
        public static ILogResult LogCritical(this IApplicationLogger logger, Exception exception, string message, params object[] args)
        {
            return logger.Log(LogLevel.Critical, exception, message, args);
        }
        
        public static ILogResult LogCritical(this IApplicationLogger logger, string message, params object[] args)
        {
            return logger.Log(LogLevel.Critical, message, args);
        }
        #endregion
        #region Log
        public static ILogResult Log(this IApplicationLogger logger, LogLevel logLevel, string message, params object[] args)
        {
            return logger.Log(logLevel, 0, null, message, args);
        }

        public static ILogResult Log(this IApplicationLogger logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            return logger.Log(logLevel, eventId, null, message, args);
        }

        public static ILogResult Log(this IApplicationLogger logger, LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            return logger.Log(logLevel, 0, exception, message, args);
        }

        public static ILogResult Log(this IApplicationLogger logger, LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
        {
            return logger.Log(logLevel, eventId, new FormattedLogValues(message, args), exception, MessageFormatter);
        }
        #endregion

        public static ILoggingBuilder AddApplicationLogger(
            this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddSingleton<IApplicationLoggerProvider, ApplicationLoggerProvider>();
            builder.Services.TryAddSingleton<ILoggerProvider, ApplicationLoggerProvider>();

            LoggerProviderOptions.RegisterProviderOptions
                <ApplicationLoggerConfiguration, ApplicationLoggerProvider>(builder.Services);

            return builder;
        }
        private static string MessageFormatter(FormattedLogValues state, Exception error)
        => state.ToString();
        private struct FormattedLogValues
        {
            internal readonly string Message;
            internal readonly object[] Args;
            internal FormattedLogValues(string message, params object[] args)
            {
                Message = message;
                Args = args;
            }

            public override string ToString()
            => String.Format(Message, Args);
        }
    }
}