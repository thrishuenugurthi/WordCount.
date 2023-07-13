using Microsoft.AspNetCore.Mvc;
using POC.Models;

namespace POC.Controllers
{
    //[Route("home")]
    public class LoginController : Controller
    {

        UsersRepository repository = new UsersRepository();
       

        [HttpGet]
        public IActionResult Login()
        {
            if (TempData["Username"] != null && TempData["password"] != null)
                return View();   
            else
                return View("Login");
        }
        
        [HttpGet]
        //[Route("CreateUser")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            TempData["Username"] = null;
            TempData["password"] = null;
           bool result = repository.Verify(Email, Password);

            if (result)
            {
                TempData["Username"] = Email;
                TempData["password"] = Password;
                

                return RedirectToAction("Upload","Upload");
            }
            return View("Login");
        }

        [HttpPost]
        public IActionResult SignUp(string name,string email,string password)
        {
            Users users = new Users();
            users.password = password;
            users.email = email;
            users.fullname = name;
           bool result= repository.Register(users);
            if(result)
            {
                TempData["message"] = "User have been registered successfully.";
                return RedirectToAction("Login");
            }
            TempData["message"] = "Registration Failed";
            return View("SignUp");
        }

    }
}

