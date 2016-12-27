using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogReader
{
	public class LReader
	{
		public string Dir { get; set; }
		public LReader(string dir)
		{
			Dir = dir;
		}

		public void Main()
		{
			if (!Directory.Exists(Dir))
			{
				return;
			}

			FileInfo lastMod = GetLastModified();

			if (lastMod == null)
				return;
			using (FileStream fs = File.Open(lastMod.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (StreamReader reader = new StreamReader(fs))
			{
				while (true)
				{
					string line = "";
					if (!reader.EndOfStream)
						line = reader.ReadLine();
				}
			}

		}

		FileInfo GetLastModified()
		{
			FileInfo lastModified = null;
			foreach (string file in Directory.EnumerateFiles(Dir))
			{
				FileInfo fi = new FileInfo(file);
				if (lastModified == null || lastModified.LastWriteTime < fi.LastWriteTime)
				{
					lastModified = fi;
				}
			}

			return lastModified;
		}
	}
}
