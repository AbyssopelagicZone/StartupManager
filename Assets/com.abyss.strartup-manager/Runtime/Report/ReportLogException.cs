using System;

namespace Abyss.StartupManager
{
	public class ReportLogException : AggregateException
	{
		#region Constructors
		public ReportLogException(string message, Exception inner) : base(message, inner)
		{
		}

		public ReportLogException(string message) : base(message)
		{
		}
		#endregion
	}
}