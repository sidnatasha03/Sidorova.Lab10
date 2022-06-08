using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    //класс используется для списков из объектов классов, в которых в качестве поля имеется некоторая строковая переменная ("имя")
    //методы данного класса осуществляют поиск в списке элемента, "имя" которого совпадает с введенной строкой 

    static class FInL
    {
        public static bool Finding(List<User> L, string s)
        {
            bool BOOL = false;
            foreach (var P in L)
            {
                if (P.Login == s)
                {
                    BOOL = true;
                }
            }
            return BOOL;
        }

        public static bool Finding(List<Item> L, string s)
        {
            bool BOOL2 = false;
            foreach (var P in L)
            {
                if (P.Name == s)
                {
                    BOOL2 = true;
                }
            }
            return BOOL2;
        }
    }
}
