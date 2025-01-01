using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Web_siteResume.ViewModels;

namespace Web_siteResume.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }
        [HttpPost]
        [Route("/profile")]
        public async Task<IActionResult> IndexSave() //Сохраняем картинку профиля в папки на сервере (не в БД, чтобы не раздувать БД)
        {
            //if (ModelState.IsValid) { }
            string filename = "";
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                MD5 md5hash = MD5.Create(); //создаем объект для работы с MD5
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imageData.FileName); //преобразуем имя файла в массив байтов
                byte[] hashBytes = md5hash.ComputeHash(inputBytes); //получаем хэш в виде массива байтов

                string hash = Convert.ToHexString(hashBytes); //преобразуем хэш из массива в строку, состоящую из шестнадцатеричных символов в верхнем регистре

                var dir = "./wwwroot/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);//из хэша для первого уровня папки берем 2 первых символа, для 2 уровня папки - 4 символа
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                filename = dir + "/" + imageData.FileName;
                using (var stream = System.IO.File.Create(filename)) //стример для копирования данных в файл
                {
                    await imageData.CopyToAsync(stream);
                }
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
