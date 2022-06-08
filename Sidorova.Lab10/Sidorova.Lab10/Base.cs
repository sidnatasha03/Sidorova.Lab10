using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    public class Base   //внутренняя база данных
    {

        public List<User> L;   //список зарегистрированных пользователей (ключ - логин, значение - пользователь)
        public List<Item> It;  //cписок товаров, имеющихся в наличии
        public User U;         //текущий пользователь

        public Base()   //инициализация списков и определение товаров, имеющихся в наличии изначально

        {
            L = new List<User>();
            It = new List<Item>();
            Item Apple = new Item("Cмартфон", 70000, "Электроника", 1);
            It.Add(Apple);
            Item Laptop = new Item("Ноутбук", 80000, "Электроника", 5);
            It.Add(Laptop);
            Item TV = new Item("Телевизор", 100000, "Электроника", 2);
            It.Add(TV);
            Item Sneakers = new Item("Кроссовки", 6000, "Одежда и обувь", 4);
            It.Add(Sneakers);
            Item Sweater = new Item("Кофта", 40000, "Одежда и обувь", 4);
            It.Add(Sweater);
            Item Plate = new Item("Тарелка", 500, "Товары для дома", 5);
            It.Add(Plate);
            Item Mug = new Item("Кружка", 250, "Товары для дома", 4);
            It.Add(Mug);
        }
        public void Current(User U1)   //установка текущего пользователя
        {
            U = U1;
        }
    }
}