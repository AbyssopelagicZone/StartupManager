namespace Abyss.StartupManager
{
	public readonly struct InitializableProgress
	{
		#region Public Fields
		public readonly InitializableWrapper Initializable;
		public readonly Progress Progress;
		public readonly bool IgnoreInprogress;
		#endregion

		#region Constructors
		public InitializableProgress
			(InitializableWrapper initializable, Progress progress, bool ignoreInprogress)
		{
			Initializable = initializable;
			Progress = progress;
			IgnoreInprogress = ignoreInprogress;
		}
		#endregion
	}
}