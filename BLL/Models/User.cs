using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSocialWork.BLL.Models
{
    public class User
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string FavoriteMovie { get; set; }
        public string FavoriteBook { get; set; }
        public IEnumerable<Messages> IncomingMessages { get; }
        public IEnumerable<Messages> OutgoingMessages { get; }
        public IEnumerable<Friends> friends { get; }

        public User(
            int id,
            string firstName,
            string lastName,
            string password,
            string email,
            string photo,
            string favoriteMovie,
            string favoriteBook, 
            IEnumerable<Messages> incomingMessages,
            IEnumerable<Messages> outgoingMessages)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Email = email;
            this.Photo = photo;
            this.FavoriteMovie = favoriteMovie;
            this.FavoriteBook = favoriteBook;
            this.IncomingMessages = incomingMessages;
            this.OutgoingMessages = outgoingMessages;
        }
    }
}
