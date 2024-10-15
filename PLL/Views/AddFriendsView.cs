using ConsoleSocialWork.BLL.Exceptions;
using ConsoleSocialWork.BLL.Models;
using ConsoleSocialWork.BLL.services;
using ConsoleSocialWork.DAL.Intefeces;
using ConsoleSocialWork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSocialWork.PLL.Views
{
    public class AddFriendsView
    {
        UserServices userService;
       
        public  AddFriendsView(UserServices userService)
        {
            this.userService = userService;
           
        }   

        public void Show(User user)
        {
            try
            {
                var userAddFriend = new UserAddFriend();
                Console.WriteLine("Введите электронную почту вашего потенциального друга :)");
                userAddFriend.email = Console.ReadLine();
                userAddFriend.Id = user.Id;
                userService.AddFriend(userAddFriend);
                SuccessMessage.Show("У вас новый друг!");
            }
            catch (UserNotFoundException)
            {
                AllertMessages.Show("Пользователя с указанным почтовым адресом не существует!");
            }

            catch (Exception)
            {
                AllertMessages.Show("Произоша ошибка при добавлении пользотваеля в друзья!");
            }

        }
}
