using Microsoft.AspNetCore.Mvc;
using ClockManagement.Models;
namespace ClockManagement.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}
