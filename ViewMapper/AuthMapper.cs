using Web_siteResume.DAL.Models;
using Web_siteResume.ViewModels;

namespace Web_siteResume.ViewMapper
{
    //Создаем класс AuthMapper - это "клей", чтобы наши представления ничего не знали что у нас происходит на BL уровне
    //Мы из вьюшки получаем модель RegisterViewModel и превращаеть ее в DAL.User модель и отправлять ее на тот уровень
    public class AuthMapper
    {
        public static UserModel MapRegisterViewModelToUserModel (RegisterViewModel model) 
        {
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }    
}
