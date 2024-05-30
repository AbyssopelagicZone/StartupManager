Startup Manager
===============
## Overview
Srartup Manager provides a simple and structured way to manage the application initialization process in Unity. 
It introduces interfaces such as `IInitializable` and `ICoroutineInitializable` 
to ensure consistent and ordered initialization of your systems in Unity projects.

## Features
- **Asynchronous initialization**: Initialize systems asynchronously using coroutines or UniTasks.
- **Initialization order**: Define the order of initialization for systems.
- **Loading screen**: Show a loading screen while initializing systems.
- **Accumulated Initialization Progress**: Track and display the overall initialization progress to provide feedback to users.

## Get Started
### Basic Usage

1. Create a class that inherits from `EntryPointMonoBehaviour` or `EntryPointScriptableObject`.
2. Write the initialization logic.
3. In case of `EntryPointMonoBehaviour`, attach the script to a GameObject in the scene.
4. In case of `EntryPointScriptableObject`, create scriptable object asset in Resources folder with name EntryPoint.
5. Run the scene.

## Entry Point Priority

`EntryPointMonoBehaviour` and `EntryPointScriptableObject` have a priority property that determines which entry point to use.
If you have multiple entry points in the scene and in the resources folder, the entry point with the highest priority will be used.
This way you can start up some scenes with different initialization logic. This is useful in development ant testing ideas.

## Examples

### EntryPointMonoBehaviour
```csharp
using Abyss.StartupManager;

public class MyEntryPoint : EntryPointMonoBehaviour
{
	#region Overrides
	public override void ResolveObjectGraph()
	{
		var service = new InitializableService1();
		AddInitializable(service);
	}
	#endregion
}
```

### EntryPointScriptableObject
```csharp
using Abyss.StartupManager;
using UnityEngine;

[CreateAssetMenu(fileName = "MyEntryPoint", menuName = "Startup Manager/MyEntryPoint", order = 1)]
public class MyEntryPoint : EntryPointScriptableObject
{
	#region Overrides
	public override void ResolveObjectGraph()
	{
		var service = new InitializableService1();
		AddInitializable(service);
	}
	#endregion
}
```


### Initializables
```csharp   
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
```
