#if TESTS_UNITASK_SUPPORT

using System;
using Abyss.StartupManager;
using Cysharp.Threading.Tasks;

namespace UniTaskSupportTests
{
	public class InitializableWithException : IUniTaskInitializable
	{
		#region Interface Implementations
		public async UniTask Initialize(IProgressReceiver progressReceiver)
		{
			for (var i = 0; i < 10; i++)
			{
				if (i == 6)
					throw new ThrownException("Very unexpected error.");

				progressReceiver.Report(i * 0.1f);
				await UniTask.Delay(100);
			}
		}
		#endregion

		#region Nested Types
		public class ThrownException : Exception
		{
			#region Constructors
			public ThrownException(string message) : base(message)
			{
			}
			#endregion
		}
		#endregion
	}

	public abstract class InitializableExampleService : IUniTaskInitializable
	{
		#region Interface Implementations
		public UniTask Initialize(IProgressReceiver progressReceiver)
		{
			progressReceiver.Report(1.0f);

			return UniTask.CompletedTask;
		}
		#endregion
	}

	public abstract class AsyncInitializableExampleService : IUniTaskInitializable
	{
		#region Interface Implementations
		public async UniTask Initialize(IProgressReceiver progressReceiver)
		{
			await UniTask.Delay(100);
			progressReceiver.Report(1.0f);
		}
		#endregion
	}

	public abstract class InitializableWithReportExampleService : IUniTaskInitializable
	{
		#region Interface Implementations
		public async UniTask Initialize(IProgressReceiver progressReceiver)
		{
			for (var i = 0; i < 10; i++)
			{
				var message = $"Initializing: {GetType().Name} step: {i}";
				progressReceiver.Report(0.1f * i, message);
				await UniTask.Delay(100);
			}
		}
		#endregion
	}

	public class InitializableService1 : InitializableExampleService
	{
	}

	public class InitializableService2 : InitializableExampleService
	{
	}

	public class InitializableService3 : InitializableExampleService
	{
	}

	public class AsyncInitializableService1 : AsyncInitializableExampleService
	{
	}

	public class AsyncInitializableService2 : AsyncInitializableExampleService
	{
	}

	public class AsyncInitializableService3 : AsyncInitializableExampleService
	{
	}

	public class InitializableWithReportExample1 : InitializableWithReportExampleService
	{
	}

	public class InitializableWithReportExample2 : InitializableWithReportExampleService
	{
	}

	public class InitializableWithReportExample3 : InitializableWithReportExampleService
	{
	}
}

#endif