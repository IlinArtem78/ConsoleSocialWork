using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSocialWork.BLL.Models
{
    public class Friends
    {
        public int Id { get; }
        public int user_id { get; }
        public int friend_id { get; set; }

        public Friends(int  id, int user_id, int friend_id)
        {
            this.Id = id;
            this.user_id = user_id;
            this.friend_id = friend_id;
        }



    }
}
