using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Item
    {
        public string Name;      //название товара
        public int Price;        //цена товара
        public string Category;   //категория товара
        public int Amount;       //количество данных товаров в наличии

        public Item(string name, int price, string categor, int amount)
        {
            Name = name;
            Price = price;
            Category = categor;
            Amount = amount;
        }
    }
}
