﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Валидация сделана на BL уровне, тут ее проверяем на уровне модели
                var errorModel = await authBL.ValidateEmail(model.Email ?? "");
                if (errorModel != null)
                {
                    ModelState.TryAddModelError("Email", errorModel.ErrorMessage!);
                }
            }

            if (ModelState.IsValid)
            {
                await authBL.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }
            return View("Index", model);
        }

    }
}
