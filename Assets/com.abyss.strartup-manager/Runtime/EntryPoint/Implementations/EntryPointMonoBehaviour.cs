using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if STARTUP_MANAGER_UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#endif

namespace Abyss.StartupManager
{
	public class EntryPointMonoBehaviour : MonoBehaviour, IEntryPoint
	{
		#region UnitySerialized
		[SerializeField]
		private EntryPointSettings settings = new EntryPointSettings();
		#endregion

		#region Properties
		public IReadOnlyCollection<InitializableProgress> Initializables => _initializables;
		public IReadOnlyCollection<IProgressReceiver> ProgressReceivers => _progressReceivers;
		public EntryPointSettings Settings => settings;

		protected ILogReadOnly InitializationLog { get; private set; }
		#endregion

		#region Private Fields
		private readonly List<IProgressReceiver> _progressReceivers = new List<IProgressReceiver>();
		private readonly List<InitializableProgress> _initializables = new List<InitializableProgress>();
		#endregion

		#region Interface Implementations
		public void SetLog(ILogReadOnly log)
		{
			InitializationLog = log;
		}

		public virtual void ApplySettings()
		{
		}

		public virtual void ResolveObjectGraph()
		{
		}

		public virtual void PreInitialize()
		{
		}

		public virtual void PostInitialize()
		{
		}

		public virtual IEnumerator CoroutinePreInitialize()
		{
			yield break;
		}

		public virtual IEnumerator CoroutinePostInitialize()
		{
			yield break;
		}

		public virtual LoadingScreen LoadLoadingScreen() => LoadingScreen.LoadDefaultLoadingScreen();

		public void Dispose()
		{
			_initializables.Clear();
			_progressReceivers.Clear();
		}
		#endregion

		#region Public Members
		public void AddProgressReceiver(IProgressReceiver progressReceiver)
		{
			if (_progressReceivers.Contains(progressReceiver))
			{
				throw new
					StartupManagerException($"Cannot add the same progress receiver twice. Progress receiver: {progressReceiver}");
			}

			_progressReceivers.Add(progressReceiver);
		}
		#endregion

		#region Protected Members
		protected void AddInitializable(IInitializableInternal initializable, bool ignoreInProgress = false)
		{
			if (_initializables.FindIndex(x => x.Initializable == initializable) >= 0)
			{
				throw new
					StartupManagerException($"Cannot add the same initializable twice. Initializable: {initializable}");
			}

			var initializableProgress =
				new InitializableProgress(initializable.ToInitializableWrapper(), new Progress(), ignoreInProgress);

			_initializables.Add(initializableProgress);
		}
		#endregion

#if STARTUP_MANAGER_UNITASK_SUPPORT

		public virtual UniTask UniTaskPreInitialize() => UniTask.CompletedTask;

		public virtual UniTask UniTaskPostInitialize() => UniTask.CompletedTask;
#endif
	}
}