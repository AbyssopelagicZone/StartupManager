using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Abyss.StartupManager.Samples
{
	public class UniTaskEntryPoint : EntryPointMonoBehaviour
	{
		#region Overrides
		public override void PreInitialize()
		{
			Debug.Log("PreInitialize");
		}

		public override void ResolveObjectGraph()
		{
			var initializableService = new UniTaskInitializableService();
			AddInitializable(initializableService);
		}

		public override void PostInitialize()
		{
			Debug.Log("PostInitialize");
		}

		public override UniTask UniTaskPostInitialize()
		{
			Debug.Log("UniTaskPostInitialize");

			return UniTask.CompletedTask;
		}

		public override UniTask UniTaskPreInitialize()
		{
			Debug.Log("UniTaskPreInitialize");

			return UniTask.CompletedTask;
		}
		#endregion
	}
}