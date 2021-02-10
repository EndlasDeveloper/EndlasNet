using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web.Controllers
{
    public class PartImageUploadController : Controller
    {
        public IActionResult Index(List<IFormFile> files)
        {
    
          if(files.Count == 1)
            {
                Console.Out.WriteLine("FormFile list count = 1");
            }
            return Ok();
        }
    }
}
