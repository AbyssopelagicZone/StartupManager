namespace Abyss.StartupManager
{
	public static class InitializableExtensions
	{
		#region Public Members
		public static ICoroutineInitializable ToCoroutineInitializable
			(this IInitializableInternal initializable) => new InitializableWrapper(initializable);
		
		public static InitializableWrapper ToInitializableWrapper
			(this IInitializableInternal initializable) => new InitializableWrapper(initializable);
		
		#endregion
	}
}