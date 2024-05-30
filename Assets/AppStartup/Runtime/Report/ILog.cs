namespace Abyss.StartupManager
{
	public interface ILog : ILogReadOnly
	{
		#region Public Members
		void AddEntry(ReportLogEntry entry);
		#endregion
	}
}