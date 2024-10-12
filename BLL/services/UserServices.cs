using ConsoleSocialWork.BLL.Models;
using ConsoleSocialWork.DAL.Entities;
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
        public UserServices() 
        { 
            userRepository = new UserRepository();
        }
        public void Register(UserRegistrationData userRegistrationData)
        {
            if (string.IsNullOrEmpty(userRegistrationData.FirstName)) throw new ArgumentNullException(); 
            if (string.IsNullOrEmpty(userRegistrationData.LastName)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(userRegistrationData.password) || (userRegistrationData.password.Length < 8))  throw new ArgumentNullException();
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

    }
}
