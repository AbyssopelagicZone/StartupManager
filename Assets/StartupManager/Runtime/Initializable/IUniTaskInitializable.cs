#if STARTUP_MANAGER_UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#endif

namespace Abyss.StartupManager
{
#if STARTUP_MANAGER_UNITASK_SUPPORT
	public interface IUniTaskInitializable : IInitializableInternal
	{
		#region Public Members
		UniTask Initialize(IProgressReceiver progressReceiver);
		#endregion
	}
#endif
}