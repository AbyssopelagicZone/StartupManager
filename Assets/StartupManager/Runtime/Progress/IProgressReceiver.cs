using System;

namespace Abyss.StartupManager
{
	public interface IProgressReceiver : IProgress<float>
	{
		#region Public Members
		void Report(string message);
		void Report(float value, string message);
		#endregion
	}
}