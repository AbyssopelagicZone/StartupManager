using System.Collections;
using UnityEngine;

namespace Abyss.StartupManager.Samples
{
	public class CustomInitializableService : ICoroutineInitializable
	{
		#region Interface Implementations
		public IEnumerator Initialize(IProgressReceiver progressReceiver)
		{
			for (var i = 0; i < 10; i++)
			{
				var message = $"Initializing: {GetType().Name} step: {i}";
				progressReceiver.Report(0.1f * i, message);

				yield return new WaitForSeconds(0.1f);
			}
		}
		#endregion
	}
}