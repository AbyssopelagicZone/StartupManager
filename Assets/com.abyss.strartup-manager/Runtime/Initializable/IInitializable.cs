namespace Abyss.StartupManager
{
	public interface IInitializable : IInitializableInternal
	{
		#region Public Members
		void Initialize();
		#endregion
	}
}