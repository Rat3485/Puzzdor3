using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroLogs
{
	public interface ILogEvents
	{
		event EventHandler<OnLogEventArgs> OnLogEvent;
	}


}
