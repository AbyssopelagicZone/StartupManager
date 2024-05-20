using System;
using System.Collections;
#if STARTUP_MANAGER_UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#endif

namespace Abyss.StartupManager
{
	public class InitializableWrapper : ICoroutineInitializable
	{
		#region Properties
		internal IInitializableInternal InitializableInternal => _initializableType switch
		{
			InitializableType.Sync => _syncInitializable,
			InitializableType.Coroutine => _coroutineInitializable,

#if STARTUP_MANAGER_UNITASK_SUPPORT
			InitializableType.UniTask => _uniTaskInitializable,

#endif

			_ => throw new ArgumentOutOfRangeException()
		};
		#endregion

		#region Private Fields
		private readonly IInitializable _syncInitializable;
		private readonly ICoroutineInitializable _coroutineInitializable;

#if STARTUP_MANAGER_UNITASK_SUPPORT
		private readonly IUniTaskInitializable _uniTaskInitializable;
#endif
		private readonly InitializableType _initializableType;
		#endregion

		#region Constructors
		public InitializableWrapper(IInitializableInternal initializableInternal)
		{
			switch (initializableInternal)
			{
				case IInitializable syncInitializable:
					_syncInitializable = syncInitializable;
					_initializableType = InitializableType.Sync;

					break;
				case ICoroutineInitializable coroutineInitializable:
					_coroutineInitializable = coroutineInitializable;
					_initializableType = InitializableType.Coroutine;

					break;
#if STARTUP_MANAGER_UNITASK_SUPPORT
				case IUniTaskInitializable uniTaskInitializable:
					_uniTaskInitializable = uniTaskInitializable;
					_initializableType = InitializableType.UniTask;

					break;

#endif

				default: throw new ArgumentOutOfRangeException();
			}
		}
		#endregion

		#region Interface Implementations
		public IEnumerator Initialize(IProgressReceiver progressReceiver)
		{
			switch (_initializableType)
			{
				case InitializableType.Sync:
					_syncInitializable.Initialize();

					yield break;
				case InitializableType.Coroutine:
					yield return _coroutineInitializable.Initialize(progressReceiver);

					break;

#if STARTUP_MANAGER_UNITASK_SUPPORT

				case InitializableType.UniTask:
					yield return _uniTaskInitializable.Initialize(progressReceiver).ToCoroutine();

					break;

#endif

				default: throw new ArgumentOutOfRangeException();
			}
		}
		#endregion

		#region Nested Types
		private enum InitializableType
		{
			Sync = 0,
			Coroutine = 1,
			UniTask = 2
		}
		#endregion
	}
}