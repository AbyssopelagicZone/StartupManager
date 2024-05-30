using System.Collections;

namespace Abyss.StartupManager
{
	public interface ICoroutineInitializable : IInitializableInternal
	{
		#region Public Members
		IEnumerator Initialize(IProgressReceiver progressReceiver);
		#endregion
	}
}