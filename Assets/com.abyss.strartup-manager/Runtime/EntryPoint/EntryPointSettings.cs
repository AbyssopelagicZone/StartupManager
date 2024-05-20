using System;
using UnityEngine;

namespace Abyss.StartupManager
{
	[Serializable]
	public class EntryPointSettings
	{
		#region Public Fields
		public bool PrintInitializationReport = true;

		public LoadingScreenMode LoadingScreenMode = LoadingScreenMode.BeforeResolveObjectGraph;

		public GameObject[] DontDestroyOnLoadObjects;
		#endregion
	}
}