using System;

namespace Abyss.StartupManager
{
	public class Progress : IProgressReceiver, IProgressSender
	{
		#region Properties
		public float ProgressValue { get; private set; }
		#endregion

		#region Events
		public event Action<float> OnProgressUpdated;
		public event Action<string> OnMessageUpdated;
		#endregion

		#region Interface Implementations
		public void Report(float value)
		{
			ProgressValue = value;
			OnProgressUpdated?.Invoke(value);
		}

		public void Report(string message)
		{
			OnMessageUpdated?.Invoke(message);
		}

		public void Report(float value, string message)
		{
			ProgressValue = value;
			OnProgressUpdated?.Invoke(value);
			OnMessageUpdated?.Invoke(message);
		}

		public void Dispose()
		{
		}
		#endregion
	}
}