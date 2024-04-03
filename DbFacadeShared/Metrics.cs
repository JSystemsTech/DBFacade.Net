
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace DbFacade
{
    #if DEBUG
    public static class Metrics
    {
        private static List<(string, double)> _MetricsList = new List<(string, double)>();
        public static IEnumerable<(string, double)> MetricsList => _MetricsList;
        public static ConcurrentDictionary<string, double> MetricsMap = new ConcurrentDictionary<string, double>();
        public static double ExecuteQuery_FillTime { get; set; }
        public static void Clear()
        {
            _MetricsList = new List<(string, double)>();
            MetricsMap.Clear(); 
        }
        public static Action Begin(string key)
        {
            var sw = new Stopwatch();
            MetricsMap.TryRemove(key, out double v);
            sw.Start();
            return () =>
            {
                sw.Stop();
                double value = sw.Elapsed.TotalSeconds;
                MetricsMap.TryAdd(key, value);
                _MetricsList.Add((key, value));
            };

        }
    }
#endif
    internal class MeasurableMetric : IDisposable
    {
        private Action Done { get; set; }
        private MeasurableMetric(string key)
        {
#if DEBUG
            Done = Metrics.Begin(key);
#else
            Done = ()=>{};
#endif
        }
        public static MeasurableMetric Create(string key) 
            => new MeasurableMetric(key);
        public void Dispose()
        {
            Done();
        }
    }
}
