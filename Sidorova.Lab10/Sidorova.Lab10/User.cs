using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class User       //аккаунт пользователя
    {
        string LOgin;         //логин пользователя
        string PAssword;      //пароль для входа
        int Count;            //внутренний счёт для оплаты
        public List<Item> Basket;     //корзина с товарами
        public User(string s1, string s2)    //инициализация происходит при регистрации
        {
            LOgin = s1;
            PAssword = s2;
            Count = 0;
            Basket = new List<Item>();
        }
        public string Login     //изменение логина в настройках аккаунта и возможность просмотреть логин
        {
            get
            {
                return LOgin;
            }
            set
            {
                LOgin = value;
            }
        }
        public string Password    //изменение пароля в настройках аккаунта и возможность просмотреть пароль
        {
            get
            {
                return PAssword;
            }
            set
            {
                PAssword = value;
            }
        }
        public int MOney             //перечисление денег на счет
        {
            get
            {
                return Count;
            }
            set
            {
                Count = value;
            }
        }

    }
}