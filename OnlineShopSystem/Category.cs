
//Ketaki Lipare

using System;

namespace OnlineShopSystem
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string Name { get; set; }

        // Constructor to initialize category properties
        public Category(int categoryId, string name)
        {
            CategoryID = categoryId;
            Name = name;
        }

        // Override ToString() method to display category details
        public override string ToString()
        {
            return $"{CategoryID} - {Name}";
        }
    }
}