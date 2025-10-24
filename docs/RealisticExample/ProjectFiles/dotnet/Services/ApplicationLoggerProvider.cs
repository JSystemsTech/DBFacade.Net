using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RealisticExampleProject.Services
{    
    public interface IApplicationLoggerProvider
    {
        ILogResult SendLog(string categoryName, string message);
    }
    public interface ILogResult
    {
        string LogId { get; }
        string Message { get; }
    }
    public sealed class ApplicationLoggerConfiguration
    {
        public string AppId { get; set; }
    }

    [ProviderAlias("ApplicationLogger")]
    public class ApplicationLoggerProvider : ILoggerProvider, IApplicationLoggerProvider, IDisposable
    {
        private ApplicationLoggerConfiguration _config;
        private readonly LogLevel _logLevel;
        private readonly ConcurrentDictionary<string, ApplicationLogger> _loggers =
            new ConcurrentDictionary<string, ApplicationLogger>(StringComparer.OrdinalIgnoreCase);
        private volatile bool _isDisposing;

        public ApplicationLoggerProvider(
            IOptionsMonitor<ApplicationLoggerConfiguration> pipeOption,
            IOptions<LoggerFilterOptions> filterOptions)
        {
            _config = pipeOption.CurrentValue;
            var provider = filterOptions?.Value.Rules.FirstOrDefault(r => r.ProviderName != null && r.ProviderName.Equals("ApplicationLogger", StringComparison.OrdinalIgnoreCase));
            _logLevel = provider?.LogLevel ?? LogLevel.Error;
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ApplicationLogger(this, categoryName, _logLevel));
        }

        #region IApplicationLoggerProvider implementation
        public ILogResult SendLog(string categoryName, string message)
        {            
            try
            {
                string logId = null;
                //ConnectedServiceLogger.LogMessage(_config.AppId, categoryName, message, out logId);
                return LogResult.Create(logId, message);
            }
            catch (Exception ex) {
                return LogResult.Create(ex, message);
            }
        }
        #endregion

        #region IDisposable implementation
        public void Dispose()
        {
            _isDisposing = true;
            _loggers.Clear();            
        }
        #endregion
        private sealed class LogResult: ILogResult
        {
            public string LogId { get; private set; }
            public string Message { get; private set; }
            public string Error { get; private set; }

            internal static ILogResult Create(string logId, string message)
                => new LogResult()
                {
                    LogId = logId,
                    Message = message
                };
            internal static ILogResult Create(Exception ex, string message)
                => new LogResult()
                {
                    LogId = "APP_LOGGER_ERROR",
                    Message = $"Error Logging Message: {message}",
                    Error = ex.Message
                };
        }
    }
}