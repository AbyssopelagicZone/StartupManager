using System.Collections.Generic;
using System.Diagnostics;

namespace Abyss.StartupManager
{
	public class Logger
	{
		#region Properties
		public ILogReadOnly Log => _log;
		#endregion

		#region Private Fields
		private readonly ILog _log = new Log();
		private readonly Dictionary<string, Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>();
		#endregion

		#region Public Members
		public void BeginSample(string messageKey)
		{
			if (_stopwatches.ContainsKey(messageKey))
				throw new ReportLogException($"Already started samples with key: {messageKey}");

			var watch = new Stopwatch();
			watch.Start();

			_stopwatches.Add(messageKey, watch);
		}

		public void EndSample(string messageKey)
		{
			if (!_stopwatches.TryGetValue(messageKey, out var watch))
				throw new ReportLogException($"There is no started samples with key: {messageKey}");

			watch.Stop();

			_stopwatches.Remove(messageKey);

			var reportLogEntry = new ReportLogEntry(messageKey, watch.ElapsedMilliseconds);
			_log.AddEntry(reportLogEntry);
		}
		#endregion
	}
}