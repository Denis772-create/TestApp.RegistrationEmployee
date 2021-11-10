using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.RegistrationEmployee.Core.Interfaces;
using TestApp.RegistrationEmployee.Infrastructure.Services;

namespace TestApp.RegistrationEmployee.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        public HomeController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string name, string email, string message)
        {
            await _emailSender.SendEmailAsync(email, "admin", name, message);
            return RedirectToAction("Index");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
