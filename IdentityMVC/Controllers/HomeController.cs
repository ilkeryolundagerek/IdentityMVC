using IdentityMVC.Models;
using IdentityMVC.Services;
using IdentityMVC.Toolbox.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;

namespace IdentityMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUploadService _upload;
        private IWebHostEnvironment _environment;
        public HomeController(ILogger<HomeController> logger, IUploadService upload, IWebHostEnvironment environment)
        {
            _logger = logger;
            _upload = upload;
            _environment = environment;
        }

        public IActionResult Index()
        {

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult XMLTool()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XMLResult(IFormCollection collection, IFormFile xmldata)
        {
            string file_path = await _upload.UploadFile(xmldata);
            if (!string.IsNullOrEmpty(file_path))
            {
                string path = Path.Combine(_environment.WebRootPath,"uploads", file_path);
                List<Employee> data = new List<Employee>();
                using (var reader = XmlReader.Create(path))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            var name = reader.Name.ToString();
                            //var value = reader.ReadContentAsString();
                        }
                    }
                }
                //string str_data = System.IO.File.ReadAllText(path);
                //XMLParserService<EmployeesViewModel> parser = new XMLParserService<EmployeesViewModel>();
                //var data = parser.DeserializeData(str_data);
                return View(data);
            }
            return RedirectToAction("XMLTool");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}