using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    //список категорий товаров (класс статический, поскольку не подразумевает изменение списка пользователем)
    static class CategoryList
    {
        public static List<string> CL;
        static CategoryList()
        {
            CL = new List<string>();
            CL.Add("Электроника"); CL.Add("Товары для дома"); CL.Add("Одежда и обувь");
        }
    }
}