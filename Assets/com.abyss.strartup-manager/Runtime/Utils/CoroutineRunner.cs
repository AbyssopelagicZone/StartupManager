using UnityEngine;

namespace Abyss.StartupManager
{
	internal class CoroutineRunner : MonoBehaviour
	{
		#region Constants
		private static CoroutineRunner _instance;
		#endregion

		#region Properties
		public static CoroutineRunner Instance
		{
			get
			{
				if (_instance != null) return _instance;
				var runner = new GameObject("CoroutineRunner");
				_instance = runner.AddComponent<CoroutineRunner>();
				DontDestroyOnLoad(runner);

				return _instance;
			}
		}
		#endregion

		#region Public Members
		public static void Clear()
		{
			Destroy(_instance.gameObject);
		}
		#endregion
	}
}