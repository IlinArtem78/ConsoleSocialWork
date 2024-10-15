using ConsoleSocialWork.BLL.Exceptions;
using ConsoleSocialWork.BLL.Models;
using ConsoleSocialWork.DAL.Entities;
using ConsoleSocialWork.DAL.Intefeces;
using ConsoleSocialWork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSocialWork.BLL.services
{
    public class UserServices
    {
        IUserRepository userRepository;
        MessageServices messageServices;
        IFriendRepository friendRepository;
        public UserServices()
        {
            userRepository = new UserRepository();
            messageServices = new MessageServices();
            friendRepository = new FriendRepository();  
        }
        public void Register(UserRegistrationData userRegistrationData)
        {
            //исключения
            if (string.IsNullOrEmpty(userRegistrationData.FirstName)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(userRegistrationData.LastName)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(userRegistrationData.password) || (userRegistrationData.password.Length < 8)) throw new ArgumentNullException();
            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email) || string.IsNullOrEmpty(userRegistrationData.Email)) throw new ArgumentNullException();
            if (userRepository.FindByEmail(userRegistrationData.Email) != null)
            {
                throw new ArgumentNullException();
            }

            var userUnity = new UserEntity()
            {
                firstname = userRegistrationData.FirstName,
                lastname = userRegistrationData.LastName,
                email = userRegistrationData.Email,
                password = userRegistrationData.password
            };

            if (this.userRepository.Create(userUnity) == 0)
            {
                throw new Exception();
            }
        }
        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (findUserEntity.password != userAuthenticationData.Password)
                throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }
        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        /*
         Вашей же задачей становится добавление логики на уровень PLL и BLL. Процесс добавления пользователя в друзья должен быть следующим:
        1. Пользователь вводит почтовый адрес друга.
        2. Если почтовый адрес существует, то выполняем добавление.
        3. Если почтового адреса не существует, то выбрасываем исключение UserNotFoundException.
        На уровне PLL по аналогии с другими классами View создайте новую View, в которую поместите основную логику представления для добавления в друзья.
        
         */
        public IEnumerable<User> GetFriendById(int id)
        {
            //поиск по всем пользователям по id которое удовлетворяет условию: 
           return friendRepository.FindAllByUserId(id).Select(m =>
            {
                var GetUserId = userRepository.FindById(m.friend_id);
      
            });
          
        }
        
        public void AddFriend(UserAddFriend userAddFriend)
        {
            //если почтовый адрес существует, то выполняем добавление.
            var findUserFriend = userRepository.FindByEmail(userAddFriend.email);   
            if (findUserFriend is null) throw new UserNotFoundException();  

            var friendEntity = new FriendEntity()
            {
               user_id = userAddFriend.Id,
               friend_id = findUserFriend.id
            };

            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }

        public void Update(User user)
        {
            var updatableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstname = user.FirstName,
                lastname = user.LastName,
                password = user.Password,
                email = user.Email,
                photo = user.Photo,
                favorite_movie = user.FavoriteMovie,
                favorite_book = user.FavoriteBook,
              
            };

            if (this.userRepository.Update(updatableUserEntity) == 0)
                throw new Exception();
        }

        private User ConstructUserModel(UserEntity userEntity)
        {
            var incomingMessages = messageServices.GetIncomingMessagesByUserId(userEntity.id);
            var outgoingMessages = messageServices.GetOutcomingMessagesByUserId(userEntity.id);
            var friendRepository = GetFriendById(userEntity.id);  

            return new User(userEntity.id,
                          userEntity.firstname,
                          userEntity.lastname,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favorite_movie,
                          userEntity.favorite_book,
                          incomingMessages,
                          outgoingMessages,
                          friendRepository
                          );
        }
    }
}
