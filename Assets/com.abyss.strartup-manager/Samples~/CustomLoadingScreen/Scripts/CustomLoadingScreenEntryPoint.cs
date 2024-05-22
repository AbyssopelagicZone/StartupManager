using System.Collections;
using UnityEngine;

namespace Abyss.StartupManager.Samples
{
	public class CustomLoadingScreenEntryPoint : EntryPointMonoBehaviour
	{
		#region Overrides
		public override void PreInitialize()
		{
			Debug.Log("PreInitialize");
		}

		public override IEnumerator CoroutinePreInitialize()
		{
			Debug.Log("CoroutinePreInitialize");

			yield return null;
		}

		public override void ResolveObjectGraph()
		{
			var loadingScreen = new CustomInitializableService();
			AddInitializable(loadingScreen);
		}

		public override void PostInitialize()
		{
			Debug.Log("PostInitialize");
		}

		public override IEnumerator CoroutinePostInitialize()
		{
			Debug.Log("CoroutinePostInitialize");

			yield return null;
		}

		public override LoadingScreen LoadLoadingScreen()
		{
			var prefabName = "CustomLoadingScreen";

			var loadingScreenPrefab = Resources.Load<LoadingScreen>(prefabName);

			if (!loadingScreenPrefab) throw new StartupManagerException($"Prefab with name: {prefabName} not found.");

			return loadingScreenPrefab;
		}
		#endregion
	}
}