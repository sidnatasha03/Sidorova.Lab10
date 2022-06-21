using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Menu
    {
        public static void InputMenu(Base B)
        {
            Console.WriteLine("Войти (1) Зарегистрироваться (2) Выход из программы (3)");
            string a = Console.ReadLine();     //выбираем действие
            if (a == "1")        //вход в аккаунт
            {
                Input(B);
            }
            else if (a == "2")    //регистрация нового пользователя
            {
                RegistrationUser(B);
            }
            else if (a == "3")   //выход из программы
            {
                Environment.Exit(0);
            }
            else               //если введен непредусмотренный символ
            {
                InputMenu(B);
            }


        }
        public static void InputMenu1(Base B)
        {
            Console.WriteLine("Взять в аренду (1) Сдать (2) Выход из программы (3)");
            string s = Console.ReadLine();     //выбираем действие
            if (s == "1")           //регистрация нового пользователя
            {
                Registration(B);
            }
            else if (s == "2")
            {
                Account account = new Account();
                Cars car = new Cars();
                Console.WriteLine("Введите Ваш номер телефона:");
                account.Phone = Console.ReadLine();
                account.Phone = Console.ReadLine();

                var CharArray = account.Phone.ToCharArray();
                while (Check.Finding2(account) == false)
                {
                    Console.WriteLine("Введите номер телефона повторно.");
                    account.Phone = Console.ReadLine();
                }
                Console.WriteLine("Введите категорию транспорта,который Вы арендовали:");
                car.Category = Console.ReadLine();
                car.Operation("Сдать", car.Category, account);
                Console.WriteLine("Спасибо за пользование нашего сервиса! До свидания!");

            }
            else if (s == "3")   //выход из программы
            {
                Environment.Exit(0);
            }
            else               //если введен непредусмотренный символ
            {
                InputMenu1(B);
            }
        }

        public static void Input(Base B)    //вход в систему
        {
            Console.WriteLine("Введите логин");
            string s1 = Console.ReadLine();     //ввод логина
            List<User> D = B.L;
            if (FInL.Finding(D, s1) == false)  //если аккаунта с введенным логином нет в списке аккаунтов, то либо вводим заново либо регистрируемся
            {
                Console.WriteLine("Пользователя с таким логином не существует. Ввести заново (1) - Зарегистрироваться (2)");
                string c = Console.ReadLine();
                if (c == "2")    //регистрируем аккаунт
                {
                    Registration(B);
                }
                else           //снова вводим логин
                {
                    Input(B);
                }
            }
            else       //если аккаунт есть в списке
            {
                User A = D.Find(AA => AA.Login == s1);
                Console.WriteLine("Введите пароль");
                string s2 = Console.ReadLine();
                while (A.Password != s2) //если пароль не соответствует ни одному из логинов
                {
                    Console.WriteLine("Пароль не совпадает с логином");
                    s2 = Console.ReadLine();
                }
                Console.WriteLine("Вход совершен");    //пароль и логин совпадают
                B.Current(A);                        //новый текущий пользователь в базе
                InputMenu1(B);                           //вход в главное меню
            }
        }

        public static void RegistrationUser(Base B)     //регистрация
          {
              Account account = new Account();
              Console.WriteLine("Введите логин");
              string s1 = Console.ReadLine();
              List<User> D = B.L;
              while (FInL.Finding(D, s1) == true)    //пользователь с таким логином уже существует
              {
                  Console.WriteLine("Пользователь с таким логином уже существует введите другой");
                  s1 = Console.ReadLine();
                  account.Login = s1;
              }
              Console.WriteLine("Введите пароль");
              string s2 = Console.ReadLine();
              account.Password = s2;
              User A = new User(s1, s2);    //создание нового аккаунта с введенными логином и паролем
              D.Add(A);                        //новый аккаунт добавляется в список

            Cars car = new Cars();
            //Account account = new Account();

            Console.WriteLine("Введите ФИО");
            account.Name = Console.ReadLine();


            Console.WriteLine("Введите страну");
            account.Country = Console.ReadLine();


            Console.WriteLine("Введите возраст");
            account.Age = Convert.ToInt32(Console.ReadLine());
            try
            {

                if (Check.Finding1(account) == false)
                {
                    Console.WriteLine("Вам недостаточно лет :( Мы не сможем Вас обслужить. До свидания!");
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Неверно введён возраст");
            }

            Console.WriteLine("Введите номер телефона (+7...)");
            account.Phone = Console.ReadLine();

            var CharArray = account.Phone.ToCharArray();
            while (Check.Finding2(account) == false)
            {
                Console.WriteLine("Введите номер телефона повторно.");
                account.Phone = Console.ReadLine();
            }

            Console.WriteLine("Введите почту");
            account.Email = Console.ReadLine();


            



            Console.WriteLine("Пользователь зарегистрирован");
              B.Current(A);                         //текущий пользователь обновляется
              InputMenu(B);                          //вход в главное меню
            }


          public static void Registration(Base B)     //заполнение анкеты
        {
            Cars car = new Cars();
            Account account = new Account();
            Console.WriteLine("Введите категорию Вашего водительского удостоверения: A, B, D");
            car.Category = Console.ReadLine();
            if (Check.Finding3(car) == false)
            {
                Console.WriteLine("Транспорта данной категории нет. Хотите продолжить?");
                string answ = Console.ReadLine().ToUpper();
                if (answ == "ДА")
                {
                    InputMenu(B);

    }
                else
                {
                    Environment.Exit(0);
                }
            }
            Console.WriteLine("Показываем доступный транспорт, соответствующий Вашей категории...");
            car.Operation("Вывести доступный транспорт и взять в аренду", car.Category, account);
        }
    }
    static class FInL
    {
        public static bool Finding(List<User> L, string s)
        {
            bool F = false;
            foreach (var P in L)
            {
                if (P.Login == s)
                {
                    F = true;
                }
            }
            return F;
        }

        public static bool Finding(List<Item> L, string s)
        {
            bool F = false;
            foreach (var P in L)
            {
                if (P.Name == s)
                {
                    F = true;
                }
            }
            return F;
        }
    }
}
