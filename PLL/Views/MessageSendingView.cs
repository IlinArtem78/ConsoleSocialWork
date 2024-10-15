using ConsoleSocialWork.BLL.Exceptions;
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
    public class MessageSendingView
    {
        UserServices userService;
        MessageServices messageService;
        public MessageSendingView(MessageServices messageService, UserServices userService)
        {
            this.messageService = messageService;
            this.userService = userService;
        }

        public void Show(User user)
        {
            var messageSendingData = new MessageSendingData();

            Console.Write("Введите почтовый адрес получателя: ");
            messageSendingData.Recipient_id = Console.ReadLine();

            Console.WriteLine("Введите сообщение (не больше 5000 символов): ");
            messageSendingData.Content = Console.ReadLine();

            messageSendingData.SenderId = user.Id;

            try
            {
                messageService.SendMessage(messageSendingData);

                SuccessMessage.Show("Сообщение успешно отправлено!");

                user = userService.FindById(user.Id);
            }

            catch (UserNotFoundException)
            {
                AllertMessages.Show("Пользователь не найден!");
            }

            catch (ArgumentNullException)
            {
                AllertMessages.Show("Введите корректное значение!");
            }

            catch (Exception)
            {
                AllertMessages.Show("Произошла ошибка при отправке сообщения!");
            }

        }
    }
}
