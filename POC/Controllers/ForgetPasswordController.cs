using Microsoft.AspNetCore.Mvc;
using POC.Models;

namespace POC.Controllers
{
    public class ForgetPasswordController : Controller
    {
        UsersRepository repository = new UsersRepository();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ForgetPassword(string email,string password,string newpassword)
        {
          bool success=false;
          bool result = repository.ValidateEmail(email);
            if (result)
            {
                if (password == newpassword)
                {


                    success = repository.ForgetPassword(email, newpassword);

                }
            }
            if(success) 
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View("ForgetPassword");
            }
            
        }
    }
}
