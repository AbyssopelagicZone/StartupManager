#if TESTS_UNITASK_SUPPORT

using System.Collections;
using System.Collections.Generic;
using Abyss.StartupManager;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniTaskSupportTests
{
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
				new InitializableService3(),
				new AsyncInitializableService1(),
				new AsyncInitializableService2(),
				new AsyncInitializableService3(),
				new InitializableWithReportExample1(),
				new InitializableWithReportExample2(),
				new InitializableWithReportExample3()
			};

			initializables.ForEach(i => AddInitializable(i));
		}

		public override void PreInitialize()
		{
		}

		public override void PostInitialize()
		{
		}

		public override UniTask UniTaskPostInitialize()
		{
			Debug.Log("UniTaskPostInitialize");

			return UniTask.CompletedTask;
		}

		public override UniTask UniTaskPreInitialize()
		{
			Debug.Log("UniTaskPreInitialize");

			return UniTask.CompletedTask;
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
}
#endif