using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMS.DataBase
{
    public class User
    {
        private int ID { get; set; }
        private bool IsAdmin { get; set; }
        private string Login { get; set; }
        private string Password { get; set; }
        private string Fio { get; set; }
        private string Email { get; set; }
        private string Phone { get; set; }
        private string StartDate { get; set; }
        private int Salary { get; set; }
        private string Position { get; set; }
        private string ImageUrl { get; set; }

        public User(int ID, bool IsAdmin, string Login, string Password, string Fio, string Email, string Phone, string StartDate, int Salary, string Position, string ImageUrl)
        {
            this.ID = ID;
            this.IsAdmin = IsAdmin;
            this.Login = Login;
            this.Password = Password;
            this.Fio = Fio;
            this.Email = Email;
            this.Phone = Phone;
            this.StartDate = StartDate;
            this.Salary = Salary;
            this.Position = Position;
            this.ImageUrl = ImageUrl;
        }
        public int GetID()
        {
            return ID;
        }
        public bool GetIsAdmin()
        {
            return IsAdmin;
        }
        public string GetLogin()
        {
            return Login;
        }
        public string GetPassword()
        {
            return Password;
        }
        public string GetFio()
        {
            return Fio;
        }
        public string GetEmail()
        {
            return Email;
        }
        public string GetPhone()
        {
            return Phone;
        }
        public string GetStartDate()
        {
            return StartDate;
        }
        public int GetSalary()
        {
            return Salary;
        }
        public string GetPosition()
        {
            return Position;
        }
        public string GetImageUrl()
        {
            return ImageUrl;
        }
    }
}