using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NecroLogs
{
	public abstract class LogTrigger
	{
		public LogTrigger(int id)
		{
			Id = id;
		}
		public abstract Regex Condition
		{
			get;
		}

		public int Id { get; private set; }

		public bool Check(LogLine line)
		{
			if (line == null)
				return false;

			return Check(line.Text);
		}

		public virtual bool Check(string line)
		{
			if (Condition == null || line == null)
				return false;

			return Condition.IsMatch(line);
		}
	}
}
