using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abyss.StartupManager
{
	public class Log : ILog
	{
		#region Properties
		public IEnumerable<ReportLogEntry> Entries => _entries.Reverse().ToArray();
		#endregion

		#region Events
		public event Action<ReportLogEntry> OnEntryAdded;
		#endregion

		#region Private Fields
		private readonly Stack<ReportLogEntry> _entries = new Stack<ReportLogEntry>();
		#endregion

		#region Interface Implementations
		public string GetStringLog()
		{
			var capacity = _entries.Sum(x => x.PotentialCharSize) + _entries.Count * 8;
			var result = new StringBuilder(capacity);

			var reversedEntries = _entries.Reverse();

			foreach (var entry in reversedEntries)
			{
				result.Append(entry);
				result.Append('\n');
			}

			return result.ToString();
		}

		public void AddEntry(ReportLogEntry entry)
		{
			_entries.Push(entry);
			OnEntryAdded?.Invoke(entry);
		}
		#endregion
	}
}