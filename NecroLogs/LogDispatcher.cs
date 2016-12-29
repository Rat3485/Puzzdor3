using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroLogs
{
	public class LogDispatcher
	{
		public List<LogTrigger> Triggers { get; private set; }
		public List<TriggerEvent> Events { get; private set; }

		public LogDispatcher(ILogEvents logEvents)
		{
			logEvents.OnLogEvent += _OnLogEvents;
			Triggers = new List<LogTrigger>();
			Events = new List<TriggerEvent>();
		}

		private void _OnLogEvents(object sender, OnLogEventArgs e)
		{
			foreach (var trigger in Triggers.Where(t => t.Check(e.Line)))
			{
				foreach (var triggerEvent in Events.Where(te => te.TriggerId == trigger.Id))
				{
					triggerEvent.Triggered();
				}
			}
		}
	}
}
