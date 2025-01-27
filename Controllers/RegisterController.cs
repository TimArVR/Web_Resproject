using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Web_siteResume.BL.Auth;
using Web_siteResume.BL.Exeption;
using Web_siteResume.ViewMapper;
using Web_siteResume.ViewModels;

namespace Web_siteResume.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuth authBL;
        public RegisterController(IAuth authBL)
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
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (DuplicateEmailException) 
                {
                    ModelState.TryAddModelError("Email", "Email уже существует");
                }
            }
            return View("Index", model);
        }

    }
}
