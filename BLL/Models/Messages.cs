using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSocialWork.BLL.Models
{
    public class Messages
    {
        public int Id { get; }
        public string Content { get; }
        public string SenderEmail { get; }
        public string RecipientEmail { get; }
        public Messages(int id, string content, string senderEmail, string recipientEmail)
        {
            this.Id = id;
            this.Content = content;
            this.SenderEmail = senderEmail;
            this.RecipientEmail = recipientEmail;
        }   
    }
}
