using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMS.DataBase
{
    public class Objectives
    {
        private int ID { get; set; }
        private bool IsDone { get; set; }
        private int Group { get; set; }
        private string Author { get; set; }
        private string Text { get; set; }
        private string Date { get; set; }

        public Objectives(int ID, bool IsDone, int Group, string Author, string Text, string Date)
        {
            this.ID = ID;
            this.IsDone = IsDone;
            this.Group = Group;
            this.Author = Author;
            this.Text = Text;
            this.Date = Date;
        }
        public int GetID()
        {
            return ID;
        }
        public bool GetIsDone()
        {
            return IsDone;
        }
        public int GetGroup()
        {
            return Group;
        }
        public string GetAuthor()
        {
            return Author;
        }
        public string GetText()
        {
            return Text;
        }
        public string GetDate()
        {
            return Date;
        }
    }
}