using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DomainService.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace MyOBWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileService _fileService;

        public HomeController(IHostingEnvironment hostingEnvironment, IFileService fileService)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadFile()
        {
            IEnumerable<string> res = _fileService.process(Request.Form.Files);
            return Json(res);
        }
    }
}
