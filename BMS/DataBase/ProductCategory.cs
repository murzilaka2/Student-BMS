using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMS.DataBase
{
    public class ProductCategory
    {
        private int ID { get; set; }
        private int Group { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }

        public ProductCategory(int ID, int Group, string Name, string Description)
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

        public class Product
        {
            private int ID { get; set; }
            private int Group { get; set; }
            private int CategoryID { get; set; }
            private string Name { get; set; }
            public int Count { get; set; }
            public int PurchasePrice { get; set; }
            public int CostOfSales { get; set; }
            private string Description { get; set; }
            private string Image { get; set; }

            public Product(int ID, int Group, int CategoryID, string Name, int Count, int PurchasePrice, int CostOfSales, string Description, string Image)
            {
                this.ID = ID;
                this.Group = Group;
                this.CategoryID = CategoryID;
                this.Name = Name;
                this.Count = Count;
                this.PurchasePrice = PurchasePrice;
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
            public int GetCount()
            {
                return Count;
            }
            public int GetPurchasePrice()
            {
                return PurchasePrice;
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
        public class ProductReports
        {
            private int ID;
            private int Group;
            private int ProductID;
            private string UserName;
            private string Time;
            private string Date;

            public ProductReports(int ID, int Group, int ProductID, string UserName, string Time, string Date)
            {
                this.ID = ID;
                this.Group = Group;
                this.ProductID = ProductID;
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
            public int GetProductID()
            {
                return ProductID;
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