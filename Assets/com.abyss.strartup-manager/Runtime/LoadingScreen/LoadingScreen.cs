using UnityEngine;

namespace Abyss.StartupManager
{
	public abstract class LoadingScreen : MonoBehaviour, IProgressReceiver
	{
		#region Interface Implementations
		public abstract void Report(float value);
		public abstract void Report(string message);
		public abstract void Report(float value, string message);
		#endregion

		#region Public Members
		public abstract void Open();
		public abstract void Close();
		#endregion

		internal static LoadingScreen LoadDefaultLoadingScreen()
		{
			const string loadingScreenReleasePrefabName = "DefaultReleaseLoadingScreen";
			const string loadingScreenDebugPrefabName = "DefaultDebugLoadingScreen";

			var prefabName = Debug.isDebugBuild ? loadingScreenDebugPrefabName : loadingScreenReleasePrefabName;

			var loadingScreenPrefab = Resources.Load<LoadingScreen>(prefabName);

			if (!loadingScreenPrefab) throw new StartupManagerException($"Prefab with name: {prefabName} not found.");

			return loadingScreenPrefab;
		}
	}
}