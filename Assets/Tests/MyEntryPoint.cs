using Abyss.StartupManager;
using UnityEngine;

[CreateAssetMenu(fileName = "MyEntryPoint", menuName = "Startup Manager/MyEntryPoint", order = 1)]
public class MyEntryPoint : EntryPointScriptableObject
{
	#region Overrides
	public override void ResolveObjectGraph()
	{
		var service = new InitializableService1();
		AddInitializable(service);
	}
	#endregion
}