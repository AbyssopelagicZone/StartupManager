using System.Collections;
using Abyss.StartupManager;
using UnityEngine;
using UnityEngine.TestTools;

public class Test
{
	#region Public Members
	[UnityTest]
	public IEnumerator TestBootstrapOk()
	{
		var go = new GameObject("Application Entry");

		var testEntryOk = go.AddComponent<TestEntryOk>();

		testEntryOk.Settings.Priority = 100;

		var progress = new Progress();

		EntryPointRunner.Run(testEntryOk);

		testEntryOk.AddProgressReceiver(progress);

		float progressValue = 0;
		progress.OnProgressUpdated += p => progressValue = p;

		yield return new WaitWhile(() => !Mathf.Approximately(progressValue, 1.0f));
		yield return new WaitForSeconds(1.0f);
	}

#if TESTS_UNITASK_SUPPORT

	[UnityTest]
	public IEnumerator TestBootstrapOkWithUniTask()
	{
		var go = new GameObject("Application Entry");

		var testEntryOk = go.AddComponent<UniTaskSupportTests.TestEntryOk>();

		testEntryOk.Settings.Priority = 100;

		var progress = new Progress();

		EntryPointRunner.Run(testEntryOk);

		testEntryOk.AddProgressReceiver(progress);

		float progressValue = 0;
		progress.OnProgressUpdated += p => progressValue = p;

		yield return new WaitWhile(() => !Mathf.Approximately(progressValue, 1.0f));
		yield return new WaitForSeconds(1.0f);
	}

#endif
	#endregion
}