using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RatAPI.Controllers
{
    [Route("api/[controller]")]
    public class LevelController : Controller
    {
		public LevelController(IFileProvider fileProvider)
		{
			_fileProvider = fileProvider;
		}
		IFileProvider _fileProvider;

		[HttpGet()]
		public IActionResult Get()
		{
			var contents = _fileProvider.GetDirectoryContents("wwwroot/levels");
			var files = contents.Select(fi => new { fi.Name, fi.LastModified, fi.Length });
			return new ObjectResult(files);
		}

		[HttpGet("{id}", Name = "GetLevel")]
		public IActionResult GetLevel(string id)
		{
			var contents = _fileProvider.GetDirectoryContents("wwwroot/levels");
			int i = 0;
			string filename = id;
			if (int.TryParse(id, out i))
			{
				filename = "level" + i.ToString();
			}
			var item = contents.FirstOrDefault(fi => fi.Name.StartsWith(filename));
			if (item == null)
				return NotFound();

			string fileContents = "";
			using (StreamReader reader = new StreamReader(item.CreateReadStream()))
			{
				fileContents = reader.ReadToEnd();
			}

			return new ObjectResult(fileContents);
		}
    }
}
