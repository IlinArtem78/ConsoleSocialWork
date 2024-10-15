using ConsoleSocialWork.BLL.Models;
using ConsoleSocialWork.BLL.services;
using ConsoleSocialWork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSocialWork.PLL.Views
{
    public class RegistrationView
    {
        UserServices userService;
        public RegistrationView(UserServices userService)
        {
            this.userService = userService;
        }

        public void Show()
        {
            var userRegistrationData = new UserRegistrationData();

            Console.WriteLine("Для создания нового профиля введите ваше имя:");
            userRegistrationData.FirstName = Console.ReadLine();

            Console.Write("Ваша фамилия:");
            userRegistrationData.LastName = Console.ReadLine();

            Console.Write("Пароль:");
            userRegistrationData.password = Console.ReadLine();

            Console.Write("Почтовый адрес:");
            userRegistrationData.Email = Console.ReadLine();

            try
            {
                this.userService.Register(userRegistrationData);

                SuccessMessage.Show("Ваш профиль успешно создан. Теперь Вы можете войти в систему под своими учетными данными.");
            }

            catch (ArgumentNullException)
            {
                AllertMessages.Show("Введите корректное значение.");
            }

            catch (Exception)
            {
                AllertMessages.Show("Произошла ошибка при регистрации.");
            }
        }
    }
}
