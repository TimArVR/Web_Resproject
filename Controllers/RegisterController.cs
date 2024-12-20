using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Web_siteResume.BL.Auth;
using Web_siteResume.ViewMapper;
using Web_siteResume.ViewModels;

namespace Web_siteResume.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBL authBL;
        public RegisterController(IAuthBL authBL) 
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }        
        [HttpPost]
        [Route("/register")]
        public IActionResult IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                authBL.CreateUser(AuthMapper.MapRegisterViewModelToUserModel (model));
                return Redirect("/");
            }
            return View("Index", model);
        }

    }
}
