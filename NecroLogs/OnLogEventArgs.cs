using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroLogs
{
	public class OnLogEventArgs : EventArgs
	{
		public LogLine Line { get; private set; }
		public OnLogEventArgs(LogLine line)
		{
			Line = line;
		}
	}
}
