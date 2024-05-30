using System;
using System.Collections.Generic;
using System.Linq;

namespace Abyss.StartupManager
{
	public class AggregatedProgress : IProgressSender
	{
		#region Properties
		public float ProgressValue { get; private set; }
		#endregion

		#region Events
		public event Action<float> OnProgressUpdated;
		public event Action<string> OnMessageUpdated;
		#endregion

		#region Private Fields
		private IProgressSender[] _progressSenders;
		private List<IProgressReceiver> _progressListeners = new List<IProgressReceiver>();
		#endregion

		#region Constructors
		public AggregatedProgress(params IProgressSender[] progressSenders)
		{
			_progressSenders = progressSenders;

			foreach (var sender in _progressSenders)
			{
				sender.OnProgressUpdated += HandleProgressUpdated;
				sender.OnMessageUpdated += HandleDescriptionUpdated;
			}
		}
		#endregion

		#region Interface Implementations
		public void Dispose()
		{
			if (_progressSenders != null)
			{
				foreach (var sender in _progressSenders)
				{
					sender.OnProgressUpdated -= HandleProgressUpdated;
					sender.OnMessageUpdated -= HandleDescriptionUpdated;
				}
			}

			OnProgressUpdated = null;
			OnMessageUpdated = null;

			_progressListeners = null;
			_progressSenders = null;
			ProgressValue = -1;
		}
		#endregion

		#region Public Members
		public void AddListeners(IEnumerable<IProgressReceiver> listeners)
		{
			if (listeners != null)
				_progressListeners.AddRange(listeners);
		}

		public void AddListeners(params IProgressReceiver[] listeners)
		{
			if (listeners != null && listeners.Length > 0)
				_progressListeners.AddRange(listeners);
		}
		#endregion

		#region Private Members
		private void HandleDescriptionUpdated(string message)
		{
			_progressListeners.ForEach(progress => progress.Report(message));
			OnMessageUpdated?.Invoke(message);
		}

		private void HandleProgressUpdated(float value)
		{
			ProgressValue = _progressSenders.Aggregate(0.0f,
												(factor, sessionWithProgress) =>
													factor + sessionWithProgress.ProgressValue);

			var averageValue = ProgressValue / _progressSenders.Length;

			_progressListeners.ForEach(progress => progress.Report(averageValue));
			OnProgressUpdated?.Invoke(averageValue);
		}
		#endregion
	}
}