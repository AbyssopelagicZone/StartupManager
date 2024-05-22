#if STARTUP_MANAGER_UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#endif
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Abyss.StartupManager
{
	public partial class EntryPointRunner
	{
		#region Private Fields
		private readonly IEntryPoint _entryPoint;
		private readonly Logger _logger = new Logger();
		private LoadingScreen _loadingScreen;
		#endregion

		#region Constructors
		private EntryPointRunner(IEntryPoint entryPoint) => _entryPoint = entryPoint;
		#endregion

		#region Private Members
		private IEnumerator Run()
		{
			_entryPoint.SetLog(_logger.Log);

			yield return ExecuteWithSample(_entryPoint.ApplySettings, "Apply Settings");

			if (_entryPoint.Settings.LoadingScreenMode == LoadingScreenMode.BeforeResolveObjectGraph)
				yield return ExecuteWithSample(OpenLoadingScreen, "Show Loading Screen");

			yield return ExecuteWithSample(_entryPoint.ResolveObjectGraph, "Resolve Object Graph");

			if (_entryPoint.Settings.LoadingScreenMode == LoadingScreenMode.AfterResolveObjectGraph)
				yield return ExecuteWithSample(OpenLoadingScreen, "Show Loading Screen");

			yield return ExecuteWithSample(_entryPoint.PreInitialize, "Pre Initialize");

			yield return ExecuteWithSample(_entryPoint.CoroutinePreInitialize, "Coroutine Pre Initialize");

#if STARTUP_MANAGER_UNITASK_SUPPORT
			yield return ExecuteWithSample(_entryPoint.UniTaskPreInitialize, "UniTask Pre Initialize");
#endif

			yield return ExecuteWithSample(Initialize, "Initialize");

			yield return ExecuteWithSample(_entryPoint.PostInitialize, "Post Initialize");

			yield return ExecuteWithSample(_entryPoint.CoroutinePostInitialize, "Coroutine Post Initialize");

#if STARTUP_MANAGER_UNITASK_SUPPORT
			yield return ExecuteWithSample(_entryPoint.UniTaskPostInitialize, "UniTask Post Initialize");
#endif

			yield return ExecuteWithSample(CloseLoadingScreen, "Close Loading Screen");

			yield return ExecuteWithSample(InstantiateDontDestroyOnLoadObjects,
										   "Instantiate Dont Destroy On Load Objects");

			CompleteInitialization();

			yield return null;
		}

		private void CompleteInitialization()
		{
			_entryPoint.Dispose();

			if (_entryPoint.Settings.PrintInitializationReport)
				PrintReport();
		}

		private void InstantiateDontDestroyOnLoadObjects()
		{
			if (_entryPoint.Settings.DontDestroyOnLoadObjects == null) return;

			foreach (var dontDestroyOnLoadObject in _entryPoint.Settings.DontDestroyOnLoadObjects)
			{
				var clone = Object.Instantiate(dontDestroyOnLoadObject);
				Object.DontDestroyOnLoad(clone);
			}
		}

		private IEnumerator Initialize()
		{
			var initializables = _entryPoint.Initializables.Where(p => !p.IgnoreInprogress).Select(p => p.Progress).
											 Cast<IProgressSender>().ToArray();

			var aggregatedSession = new AggregatedProgress(initializables);
			aggregatedSession.AddListeners(_entryPoint.ProgressReceivers.ToArray());
			aggregatedSession.AddListeners(_loadingScreen);

			foreach (var initializableProgress in _entryPoint.Initializables)
			{
				var sampleName =
					$"Initialize: {initializableProgress.Initializable.InitializableInternal.GetType().Name}";

				BeginSample(sampleName);
				IEnumerator enumerator = null;

				var hasNext = true;

				try
				{
					enumerator = initializableProgress.Initializable.Initialize(initializableProgress.Progress);
				}
				catch (Exception e)
				{
					EndSample(sampleName);
					Complete();
					CompleteInitialization();

					throw new StartupManagerException("Initialization process failed. For reason see inner exception.",
													  e);
				}

				while (hasNext)
				{
					try
					{
						hasNext = enumerator.MoveNext();
					}
					catch (Exception e)
					{
						EndSample(sampleName);
						Complete();
						CompleteInitialization();

						throw new
							StartupManagerException("Initialization process failed. For reason see inner exception.",
													e);
					}

					yield return enumerator.Current;
				}

				EndSample(sampleName);

				initializableProgress.Progress.Report(1.0f, "");
			}

			yield break;

			void Complete()
			{
				aggregatedSession.Dispose();
				foreach (var receiver in _entryPoint.ProgressReceivers) receiver.Report(1.0f, string.Empty);
			}
		}

		private void OpenLoadingScreen()
		{
			var loadingScreenPrefab = _entryPoint.LoadLoadingScreen();
			_loadingScreen = Object.Instantiate(loadingScreenPrefab);
			_loadingScreen.Open();
			_loadingScreen.Report(0.0f, "");
		}

		private void CloseLoadingScreen()
		{
			_loadingScreen?.Close();
			if (_loadingScreen) Object.Destroy(_loadingScreen.gameObject);
		}

#if STARTUP_MANAGER_UNITASK_SUPPORT
		private IEnumerator ExecuteWithSample(Func<UniTask> function, string sampleName)
		{
			BeginSample(sampleName);

			yield return function().ToCoroutine();
			EndSample(sampleName);
		}

#endif

		private IEnumerator ExecuteWithSample(Func<IEnumerator> function, string sampleName)
		{
			BeginSample(sampleName);

			yield return function();
			EndSample(sampleName);
		}

		private IEnumerator ExecuteWithSample(Action function, string sampleName)
		{
			BeginSample(sampleName);
			function();
			EndSample(sampleName);

			yield break;
		}

		private void BeginSample(string message)
		{
			_logger.BeginSample(message);
		}

		private void EndSample(string message)
		{
			_logger.EndSample(message);
		}

		private void PrintReport()
		{
			Debug.Log(_logger.Log.GetStringLog());
		}
		#endregion
	}
}