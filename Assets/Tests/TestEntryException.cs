using System.Collections.Generic;
using Abyss.StartupManager;

public class TestEntryException : EntryPointMonoBehaviour
{
	#region Overrides
	public override void ResolveObjectGraph()
	{
		var initializables = new List<IInitializableInternal>
		{
			new InitializableService1(),
			new InitializableWithException(),
			new InitializableService2()
		};

		initializables.ForEach(i => AddInitializable(i));
	}
	#endregion
}