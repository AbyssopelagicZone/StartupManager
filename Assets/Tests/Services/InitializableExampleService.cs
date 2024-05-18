using System;
using System.Collections;
using Abyss.StartupManager;
using UnityEngine;

public class InitializableWithException : ICoroutineInitializable
{
	#region Interface Implementations
	public IEnumerator Initialize(IProgressReceiver progressReceiver)
	{
		for (var i = 0; i < 10; i++)
		{
			if (i == 6)
				throw new ThrownException("Very unexpected error.");

			progressReceiver.Report(i * 0.1f);

			yield return new WaitForSeconds(0.1f);
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

public abstract class InitializableExampleService : ICoroutineInitializable
{
	#region Interface Implementations
	public IEnumerator Initialize(IProgressReceiver progressReceiver)
	{
		progressReceiver.Report(1.0f);

		yield break;
	}
	#endregion
}

public abstract class AsyncInitializableExampleService : ICoroutineInitializable
{
	#region Interface Implementations
	public IEnumerator Initialize(IProgressReceiver progressReceiver)
	{
		yield return new WaitForSeconds(0.1f);
		progressReceiver.Report(1.0f);
	}
	#endregion
}

public abstract class InitializableWithReportExampleService : ICoroutineInitializable
{
	#region Interface Implementations
	public IEnumerator Initialize(IProgressReceiver progressReceiver)
	{
		for (var i = 0; i < 10; i++)
		{
			var message = $"Initializing: {GetType().Name} step: {i}";
			progressReceiver.Report(0.1f * i, message);

			yield return new WaitForSeconds(0.1f);
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