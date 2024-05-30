using System.Text;

namespace Abyss.StartupManager
{
	public class ReportLogEntry
	{
		#region Properties
		public long ElapsedMilliseconds { get; }
		public string Message { get; }

		public int PotentialCharSize => 64 + Message.Length;
		#endregion

		#region Constructors
		public ReportLogEntry(string message, long elapsedMilliseconds)
		{
			Message = message;
			ElapsedMilliseconds = elapsedMilliseconds;
		}
		#endregion

		#region Overrides
		public override string ToString()
		{
			var sb = new StringBuilder(PotentialCharSize);
			sb.Append("Message: ");
			sb.Append(Message);
			sb.Append(" Duration: ");
			sb.Append(ElapsedMilliseconds.ToString());
			sb.Append(" ms.");

			return sb.ToString();
		}
		#endregion
	}
}