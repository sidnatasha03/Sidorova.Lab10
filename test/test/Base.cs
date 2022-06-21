using System;
using System.Collections.Generic;
using test;

namespace test
{

    public class Base   //внутренняя база данных
    {

        public List<User> L;   //список зарегистрированных пользователей (ключ - логин, значение - пользователь)
       
        public User U;         //текущий пользователь

        public Base()   //инициализация списков и определение товаров, имеющихся в наличии изначально

        {

            L = new List<User>();
            

        }
        public void Current(User U1)   //установка текущего пользователя
        {
            U = U1;
        }
    }
}
