using UnityEngine;

namespace Abyss.StartupManager
{
	public partial class EntryPointRunner
	{
		#region Private Members
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Main()
		{
			var entryScriptableObject = Resources.Load<EntryPointScriptableObject>("EntryPoint");

			if (entryScriptableObject && entryScriptableObject.IsEnabled)
			{
				var soRunner = new EntryPointRunner(entryScriptableObject);
				CoroutineRunner.Instance.StartCoroutine(soRunner.Run());
				return;
			}

			var entryMonoBehaviour = Object.FindObjectOfType<EntryPointMonoBehaviour>();

			if (!entryMonoBehaviour) return;
			var mbRunner = new EntryPointRunner(entryMonoBehaviour);
			CoroutineRunner.Instance.StartCoroutine(mbRunner.Run());
		}
		
		public static void Run(IEntryPoint entryPoint)
		{
			var runner = new EntryPointRunner(entryPoint);
			CoroutineRunner.Instance.StartCoroutine(runner.Run());
		}
		
		#endregion
	}
}