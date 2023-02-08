using IdentityMVC.Models;
using IdentityMVC.Services;
using IdentityMVC.Toolbox.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

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
                string path = Path.Combine(_environment.WebRootPath, "uploads", file_path);
                EmployeesViewModel data;
                XmlRootAttribute xroot = new XmlRootAttribute();
                xroot.ElementName="employees";
                xroot.IsNullable=true;
                XmlSerializer serializer = new XmlSerializer(typeof(EmployeesViewModel), xroot);

                using (var reader = XmlReader.Create(path))
                {
                    data = (EmployeesViewModel)serializer.Deserialize(reader);
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