using System.Collections;
using System.Collections.Generic;
using Abyss.StartupManager;
using UnityEngine;

public class TestEntryOk : EntryPointMonoBehaviour
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
			new AsyncInitializableService1(),
		};

		initializables.ForEach(i => AddInitializable(i));
	}

	public override void PreInitialize()
	{
	}

	public override void PostInitialize()
	{
	}

	public override IEnumerator CoroutinePreInitialize()
	{
		yield return new WaitForSeconds(1.0f);
	}

	public override IEnumerator CoroutinePostInitialize()
	{
		yield return new WaitForSeconds(1.0f);
	}
	#endregion
}