using System.Collections;
using System.ComponentModel.DataAnnotations;
using Web_siteResume.DAL.Models;

namespace Web_siteResume.ViewModels
{
    public class RegisterViewModel: IValidatableObject
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$",
            ErrorMessage ="Пароль слишком простой")]
        public string? Password { get; set; }

        //Еще один способ валидации - реализовать здесь интерфейс IValidatableObject и его метод Validate
        //Тогда при вызове в RegisterController ModelState.IsVaild будут вызваны эти дополнительные проверки
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) 
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Password == "Qwert!2345") 
            {
                errors.Add(new ValidationResult("Пароль слишком примитивен", new[] { "Password" }));                
                //эта ошибка появится в файле D:\WEB_Projects\Web_siteResume\Views\Register\Index.cshtml
                //в строчке <div class="error">@Html.ValidationMessageFor(m => m.Password)</div>
                //ошибка появится под полем m.Password
            }

            if (Email == "123@123.12")
            {
                errors.Add(new ValidationResult("Email слишком примитивен", new[] { "Email" }));

            }

            return errors;
        }
    }
}
