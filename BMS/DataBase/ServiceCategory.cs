using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMS.DataBase
{
    public class ServiceCategory
    {
        private int ID { get; set; }
        private int Group { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }

        public ServiceCategory(int ID, int Group, string Name, string Description)
        {
            this.ID = ID;
            this.Group = Group;
            this.Name = Name;
            this.Description = Description;
        }
        public int GetID()
        {
            return ID;
        }
        public int GetGroup()
        {
            return Group;
        }
        public string GetName()
        {
            return Name;
        }
        public string GetDescription()
        {
            return Description;
        }

        public class Service
        {
            private int ID { get; set; }
            private int Group { get; set; }
            private int CategoryID { get; set; }
            private string Name { get; set; }
            private int CostOfSales { get; set; }
            private string Description { get; set; }
            private string Image { get; set; }

            public Service(int ID, int Group, int CategoryID, string Name, int CostOfSales, string Description, string Image)
            {
                this.ID = ID;
                this.Group = Group;
                this.CategoryID = CategoryID;
                this.Name = Name;
                this.CostOfSales = CostOfSales;
                this.Description = Description;
                this.Image = Image;
            }
            public int GetID()
            {
                return ID;
            }
            public int GetGroup()
            {
                return Group;
            }
            public int GetCategoryID()
            {
                return CategoryID;
            }
            public string GetName()
            {
                return Name;
            }
            public int GetCostOfSales()
            {
                return CostOfSales;
            }
            public string GetDescription()
            {
                return Description;
            }
            public string GetImage()
            {
                return Image;
            }
        }
        public class ServiceReports
        {
            private int ID;
            private int Group;
            private int ServiceID;
            private string UserName;
            private string Time;
            private string Date;

            public ServiceReports(int ID, int Group, int ServiceID, string UserName, string Time, string Date)
            {
                this.ID = ID;
                this.Group = Group;
                this.ServiceID = ServiceID;
                this.UserName = UserName;
                this.Time = Time;
                this.Date = Date;
            }
            public int GetID()
            {
                return ID;
            }
            public int GetGroup()
            {
                return Group;
            }
            public int GetServiceID()
            {
                return ServiceID;
            }
            public string GetUserName()
            {
                return UserName;
            }
            public string GetTime()
            {
                return Time;
            }
            public string GetDate()
            {
                return Date;
            }
        }
    }
}