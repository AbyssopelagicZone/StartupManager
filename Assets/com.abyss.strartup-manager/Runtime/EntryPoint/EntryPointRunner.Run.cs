using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abyss.StartupManager
{
	public partial class EntryPointRunner
	{
		#region Public Members
		public static void Run(IEntryPoint entryPoint)
		{
			var runner = new EntryPointRunner(entryPoint);
			CoroutineRunner.Instance.StartCoroutine(runner.Run());
		}
		#endregion

		#region Private Members
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Main()
		{
			var entryScriptableObject = Resources.Load<EntryPointScriptableObject>("EntryPoint");
			var entryMonoBehaviour = Object.FindObjectOfType<EntryPointMonoBehaviour>();

			var entryPoints = new List<IEntryPoint>();

			if (entryScriptableObject && entryScriptableObject.IsEnabled) entryPoints.Add(entryScriptableObject);

			if (entryMonoBehaviour) entryPoints.Add(entryMonoBehaviour);

			var priorEntryPoint = entryPoints.OrderByDescending(e => e.Settings.Priority).FirstOrDefault();

			if (priorEntryPoint == null) return;
			var soRunner = new EntryPointRunner(priorEntryPoint);
			CoroutineRunner.Instance.StartCoroutine(soRunner.Run());
		}
		#endregion
	}
}