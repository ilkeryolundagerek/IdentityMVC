using IdentityMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMVC.Controllers
{
    public class ExcelController : Controller
    {
        public IActionResult Index()
        {
            ExcelService.GetExcelFile();
            return View();
        }
    }
}
