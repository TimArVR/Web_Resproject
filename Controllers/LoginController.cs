using Microsoft.AspNetCore.Mvc;
using Web_siteResume.BL;
using Web_siteResume.BL.Auth;
using Web_siteResume.ViewMapper;
using Web_siteResume.ViewModels;

namespace Web_siteResume.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuth authBL;
        public LoginController(IAuth authBL)
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверны");
                }
            }
            return View("Index", model);
        }

    }
}
