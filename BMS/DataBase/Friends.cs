using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMS.DataBase
{
    public class Friends
    {
        private int ID { get; set; }
        private string Name { get; set; }
        private string FriendName { get; set; }

        public Friends(int ID, string Name, string FriendName)
        {
            this.ID = ID;
            this.Name = Name;
            this.FriendName = FriendName;
        }
        public int GetID()
        {
            return ID;
        }
        public string GetName()
        {
            return Name;
        }
        public string GetFriendName()
        {
            return FriendName;
        }
    }
}