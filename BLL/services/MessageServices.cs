using ConsoleSocialWork.BLL.Exceptions;
using ConsoleSocialWork.BLL.Models;
using ConsoleSocialWork.DAL.Entities;
using ConsoleSocialWork.DAL.Intefeces;
using ConsoleSocialWork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSocialWork.BLL.services
{
    public class MessageServices
    {
        IMessageRepository messageRepository;
        IUserRepository userRepository;
        public MessageServices()
        {
            messageRepository = new MessageRepository();
            userRepository = new UserRepository();  
        }
        public IEnumerable<Messages> GetIncomingMessagesByUserId(int recipientId)
        {
            var messages = new List<Messages>();

            messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.sender_id);
                var recipientUserEntity = userRepository.FindById(m.recipient_id);

                messages.Add(new Messages(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });
            return messages;
        }
        public IEnumerable<Messages> GetOutcomingMessagesByUserId(int senderId)
        {
            var messages = new List<Messages>();

            messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.sender_id);
                var recipientUserEntity = userRepository.FindById(m.recipient_id);

                messages.Add(new Messages(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });

            return messages;
        }
        public void SendMessage(MessageSendingData messageSendingData)
        {
            if (String.IsNullOrEmpty(messageSendingData.Content) || messageSendingData.Content.Length > 5000)
                throw new ArgumentNullException();

            var findUserEntity = this.userRepository.FindByEmail(messageSendingData.Recipient_id);
            if (findUserEntity is null) throw new UserNotFoundException();

            var messageEntity = new MessageEntity()
            {
                content = messageSendingData.Content,
                sender_id = messageSendingData.SenderId,
                recipient_id = findUserEntity.id
            };

            if (this.messageRepository.Create(messageEntity) == 0)
                throw new Exception();
        }



    }

}
