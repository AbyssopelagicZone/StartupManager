using System;
using System.Collections.Generic;

namespace Abyss.StartupManager
{
	public interface ILogReadOnly
	{
		#region Properties
		IEnumerable<ReportLogEntry> Entries { get; }
		#endregion

		#region Events
		event Action<ReportLogEntry> OnEntryAdded;
		#endregion

		#region Public Members
		string GetStringLog();
		#endregion
	}
}