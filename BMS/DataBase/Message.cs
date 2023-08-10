using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMS.DataBase
{
    public class Message
    {
        private int ID { get; set; }
        private bool IsRead { get; set; }
        private string From { get; set; }
        private string To { get; set; }
        private string Date { get; set; }
        private string Time { get; set; }
        private string Subject { get; set; }
        private string MessageText { get; set; }

        public Message(int ID, bool IsRead, string From, string To, string Date, string Time, string Subject, string MessageText)
        {
            this.ID = ID;
            this.IsRead = IsRead;
            this.From = From;
            this.To = To;
            this.Date = Date;
            this.Time = Time;
            this.Subject = Subject;
            this.MessageText = MessageText;
        }
        public int GetID()
        {
            return ID;
        }
        public bool GetIsRead()
        {
            return IsRead;
        }
        public string GetFrom()
        {
            return From;
        }
        public string GetTo()
        {
            return To;
        }
        public string GetDate()
        {
            return Date;
        }
        public string GetTime()
        {
            return Time;
        }
        public string GetSubject()
        {
            return Subject;
        }
        public string GetMessageText()
        {
            return MessageText;
        }
    }
}