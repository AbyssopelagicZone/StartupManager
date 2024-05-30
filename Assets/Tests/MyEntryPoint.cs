using System.Collections.Generic;
using Abyss.StartupManager;
using UnityEngine;

[CreateAssetMenu(fileName = "MyEntryPoint", menuName = "Startup Manager/MyEntryPoint", order = 1)]
public class MyEntryPoint : EntryPointScriptableObject
{
	#region Overrides
	public override void ApplySettings()
	{
		Application.targetFrameRate = 60;
	}

	public override void ResolveObjectGraph()
	{
		var initializables = new List<IInitializableInternal>
		{
			new InitializableService1(),
			new InitializableService2(),
			new AsyncInitializableService1()
		};

		initializables.ForEach(i => AddInitializable(i));
	}
	#endregion
}