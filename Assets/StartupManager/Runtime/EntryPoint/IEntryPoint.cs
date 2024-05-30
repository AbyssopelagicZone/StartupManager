using System;
using System.Collections;
using System.Collections.Generic;
#if STARTUP_MANAGER_UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#endif

namespace Abyss.StartupManager
{
	public interface IEntryPoint : IDisposable
	{
		#region Properties
		IReadOnlyCollection<InitializableProgress> Initializables { get; }
		IReadOnlyCollection<IProgressReceiver> ProgressReceivers { get; }
		EntryPointSettings Settings { get; }
		#endregion

		#region Public Members
		void SetLog(ILogReadOnly log);
		void ApplySettings();
		void ResolveObjectGraph();
		void PreInitialize();
		void PostInitialize();
		IEnumerator CoroutinePreInitialize();
		IEnumerator CoroutinePostInitialize();
		LoadingScreen LoadLoadingScreen();
		#endregion

#if STARTUP_MANAGER_UNITASK_SUPPORT

		UniTask UniTaskPreInitialize();

		UniTask UniTaskPostInitialize();
#endif
	}
}