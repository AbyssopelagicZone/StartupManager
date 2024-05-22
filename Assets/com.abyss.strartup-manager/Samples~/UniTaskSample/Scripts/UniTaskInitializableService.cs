using Cysharp.Threading.Tasks;

namespace Abyss.StartupManager.Samples
{
	public class UniTaskInitializableService : IUniTaskInitializable
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
}