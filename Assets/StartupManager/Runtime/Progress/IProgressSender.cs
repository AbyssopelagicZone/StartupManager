using System;

namespace Abyss.StartupManager
{
	public interface IProgressSender : IDisposable
	{
		#region Properties
		float ProgressValue { get; }
		#endregion

		#region Events
		event Action<float> OnProgressUpdated;
		event Action<string> OnMessageUpdated;
		#endregion
	}
}