using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public static class Menu
    {
        public static void InputMenu(Base B)
        {
            Console.WriteLine("Войти (1) Зарегистрироваться (2) Выход из программы (3)");
            string s = Console.ReadLine();     //выбираем действие
            if (s == "1")        //вход в аккаунт
            {
                Input(B);
            }
            else if (s == "2")    //регистрация нового пользователя
            {
                Registration(B);
            }
            else if (s == "3")   //выход из программы
            {
                Environment.Exit(0);
            }
            else               //если введен непредусмотренный символ
            {
                InputMenu(B);
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
                MainMenu(B);                           //вход в главное меню
            }
        }

        public static void Registration(Base B)     //регистрация
        {
            Console.WriteLine("Введите логин");
            string s1 = Console.ReadLine();
            List<User> D = B.L;
            while (FInL.Finding(D, s1) == true)    //пользователь с таким логином уже существует
            {
                Console.WriteLine("Пользователь с таким логином уже существует введите другой");
                s1 = Console.ReadLine();
            }
            Console.WriteLine("Введите пароль");
            string s2 = Console.ReadLine();
            User A = new User(s1, s2);    //создание нового аккаунта с введенными логином и паролем
            D.Add(A);                        //новый аккаунт добавляется в список
            Console.WriteLine("Пользователь зарегистрирован");
            B.Current(A);                         //текущий пользователь обновляется
            MainMenu(B);                          //вход в главное меню
        }

        public static void MainMenu(Base B)        //главное меню после входа
        {
            Console.WriteLine("Товары (1) - Корзина (2) - Оформление заказа (3) - Настройки (4) - Выход (5)");
            string s = Console.ReadLine();    //выбираем действие
            if (s == "1")                               //переходим в список товаров
            {
                ItemsList(B);
            }
            else if (s == "2")                       //просмотр корзины
            {
                Baskets(B);
            }
            else if (s == "3")                      //оформление заказа
            {
                int Sum = 0;                 //сумма заказа
                List<Item> Book = new List<Item>();   //список товаров в заказе 
                Booking(B, Sum, Book);
            }
            else if (s == "4")                    //настройки
            {
                Settings(B);
            }
            else if (s == "5")                   //выход из аккаунта
            {
                InputMenu(B);
            }
            else                                 //ввод непредусмотренного символа
            {
                MainMenu(B);
            }
        }

        public static void ItemsList(Base B)
        {
            foreach (var V in CategoryList.CL)
            {
                Console.Write($"{V}  ");
            }
            Console.WriteLine();
            Console.WriteLine("Вернуться в главное меню (1)");
            string s = Console.ReadLine();
            if (CategoryList.CL.Contains(s) == true)
            {
                Console.WriteLine($"Товары категории {s}:");
                foreach (var P in B.It)        //выводим список товаров указанной пользователем категории (ищем такие товары в общем списке товаров в базе)
                {
                    if (P.Category == s)
                    {
                        Console.WriteLine($"{P.Name} Количество товаров в наличии: {P.Amount}  Стоимость: {P.Price}");
                    }
                }
                Console.WriteLine("Выберите товар, который хотите добавить в корзину. Если хотите вернуться назад, нажмите (0)");
                string s1 = Console.ReadLine();    //вводим название желаемого товара
                if (FInL.Finding(B.It, s1) == true)   //если товар с таким названием есть в базовом списке
                {
                    Console.WriteLine("Выберите количество товара");
                    int n = 0;
                    Console.WriteLine($"Количество данного товара {B.It.Find(P => P.Name == s1).Amount}");
                    n = int.Parse(Console.ReadLine());
                    if (n <= B.It.Find(P => P.Name == s1).Amount || n >= 1)
                    {
                        if (FInL.Finding(B.U.Basket, s1) == true)  //если такой товар уже имеется в корзине, то добавляем введенное количество к уже имеющемуся
                        {
                            B.U.Basket.Find(P => P.Name == s1).Amount += n;        //добавляем количество
                            B.It.Find(P => P.Name == s1).Amount -= n;              //отнимаем количество у товара в базовом списке
                        }
                        else  //если товара ещё нет в корзине
                        {
                            Item I = new Item(s1, B.It.Find(P => P.Name == s1).Price, s, n);
                            B.U.Basket.Add(I);
                            B.It.Find(P => P.Name == s1).Amount -= n;
                        }
                        Console.WriteLine("Товар добавлен корзину");
                    }
                    else
                    {
                        Console.WriteLine("Количество должно быть больше 0 и не больше имеющегося количества товаров");
                    }
                    ItemsList(B);
                }
                else
                {
                    ItemsList(B);
                }
            }
            else if (s == "1")     //вернуться в главное меню
            {
                MainMenu(B);
            }
            //если введенный символ не соответствует ни одной категории из данных
            else
            {
                ItemsList(B);
            }
        }

        public static void Baskets(Base B)
        {
            Console.WriteLine("Товары, находящиеся в корзине");
            foreach (var P in B.U.Basket)     //вывод списка товаров из корзины
            {
                Console.WriteLine($"{P.Name} Количество товаров в наличии: {P.Amount}  Стоимость: {P.Price}");
            }
            Console.WriteLine("Удалить товары из корзины (1) - Вернуться назад (2)");
            string s = Console.ReadLine();          //выбираем действие
            if (s == "1")
            {
                Console.WriteLine("Выберите товар, который хотите удалить. Если хотите вернуться, нажмите '0'");
                string s1 = Console.ReadLine();
                if (FInL.Finding(B.U.Basket, s1) == false && s1 != "0")
                {
                    Console.WriteLine("Такого товара нет в корзине");
                }
                else if (FInL.Finding(B.U.Basket, s1) == false && s1 == "0")
                {
                    Baskets(B);
                }
                else
                {
                    Console.WriteLine("Выберите количество удаляемого товара");
                    int n = int.Parse(Console.ReadLine());
                    if (n < 1 || n > B.U.Basket.Find(P => P.Name == s1).Amount)
                    {
                        Console.WriteLine("Количество должно быть больше 0 и не больше имеющегося количества товаров");
                    }
                    else
                    {
                        B.U.Basket.Find(P => P.Name == s1).Amount -= n;
                        B.It.Find(P => P.Name == s1).Amount += n;
                        Baskets(B);
                        Console.WriteLine("Товар удален из корзины");
                    }
                }
                Baskets(B);
            }
            else if (s == "2")
            {
                MainMenu(B);
            }
            else
            {
                Baskets(B);
            }
        }
        public static void Booking(Base B, int Sum, List<Item> Book)
        {
            Console.WriteLine("Товары, находящиеся в корзине");
            foreach (var P in B.U.Basket)     //вывод списка товаров из корзины
            {
                Console.WriteLine($"{P.Name} Количество товаров в наличии: {P.Amount}  Стоимость: {P.Price}");
            }
            Console.WriteLine("Добавить товар в заказ (1) - Удалить товар (2) - Оплатить (3) - Вернуться назад (4)");
            string s = Console.ReadLine();          //выбираем действие
            if (s == "1")
            {
                Console.WriteLine("Выберите товар, который хотите добавить в заказ. Если хотите вернуться, нажмите '0'");
                string s1 = Console.ReadLine();
                if (FInL.Finding(B.U.Basket, s1) == false && s1 != "0")
                {
                    Console.WriteLine("Такого товара нет в корзине");
                }
                else if (FInL.Finding(B.U.Basket, s1) == false && s1 == "0")
                {
                    Booking(B, Sum, Book);
                }
                else
                {
                    Item A = B.U.Basket.Find(P => P.Name == s1);
                    Console.WriteLine("Выберите количество добавляемого товара");
                    int n = int.Parse(Console.ReadLine());
                    if (n < 1 || n > A.Amount)
                    {
                        Console.WriteLine("Количество должно быть больше 0 и не больше имеющегося количества товаров");
                    }
                    else
                    {
                        A.Amount -= n;
                        Sum += n * (A.Price);
                        Item I1 = new Item(s1, A.Price, A.Category, n);
                        if (FInL.Finding(Book, s1) == false)  //товара ещё нет в заказе
                        {
                            Book.Add(I1);
                        }
                        else
                        {
                            Book.Find(P => P.Name == s1).Amount += n;
                        }
                        Console.WriteLine($"Товар добавлен в заказ. Текущая сумма заказа {Sum}. Ваш счёт {B.U.MOney}");
                        foreach (var P in Book)
                        {
                            Console.WriteLine($"{P.Name} Количество товаров в заказе: {P.Amount}  Стоимость: {P.Price}");
                        }
                    }
                }
                Booking(B, Sum, Book);
            }
            else if (s == "2")
            {
                Console.WriteLine("Выберите товар, который хотите удалить из заказа. Если хотите вернуться, нажмите '0'");
                string s1 = Console.ReadLine();
                if (FInL.Finding(Book, s1) == false && s1 != "0")
                {
                    Console.WriteLine("Такого товара нет в заказе");
                }
                else if (FInL.Finding(Book, s1) == false && s1 == "0")
                {
                    Booking(B, Sum, Book);
                }
                else
                {
                    Console.WriteLine("Выберите количество удаляемого товара");
                    int n = int.Parse(Console.ReadLine());
                    if (n < 1 || n > Book.Find(P => P.Name == s1).Amount)
                    {
                        Console.WriteLine("Количество должно быть больше 0 и не больше имеющегося количества товаров");
                    }
                    else
                    {
                        Book.Find(P => P.Name == s1).Amount -= n;
                        Sum -= n * (Book.Find(P => P.Name == s1).Price);
                        B.U.Basket.Find(P => P.Name == s1).Amount += n;
                        Console.WriteLine("Товар удален из заказа");
                    }
                }
                Booking(B, Sum, Book);
            }
            else if (s == "3")
            {
                if (Sum > B.U.MOney)
                {
                    Console.WriteLine("Не хватает денег для оплаты. Пополнить счёт (1) - Главное меню (2)");
                    string s4 = Console.ReadLine();
                    if (s4 == "1")
                    {
                        foreach (var P in Book)  //перед переходом в настройки всё что было в заказе возвращается в корзину
                        {
                            int a = P.Amount;
                            P.Amount -= P.Amount;
                            B.U.Basket.Find(T => T.Name == P.Name).Amount += a;
                        }
                        Settings(B);
                    }
                    else
                    {
                        MainMenu(B);
                    }
                }
                else
                {
                    B.U.MOney -= Sum;
                    Console.WriteLine($"Заказ оплачен.Ваш счёт {B.U.MOney}");
                    MainMenu(B);
                }
            }
            else if (s == "4")
            {
                MainMenu(B);
            }
            else
            {
                Booking(B, Sum, Book);
            }
        }

        public static void Settings(Base B)
        {
            Console.WriteLine("Пополнить счет/снять деньги (1) - Сменить пароль (2) - Удалить аккаунт (3) - Главное меню (4)");
            string s = Console.ReadLine();
            if (s == "1")
            {
                Console.WriteLine($"Ваш счёт {B.U.MOney}");
                Console.WriteLine("Выберите нужную сумму");
                int n = int.Parse(Console.ReadLine());
                if (B.U.MOney + n < 0)
                {
                    Console.WriteLine("Вы не можете снять денег больше чем есть на счёте.");
                }
                else
                {
                    B.U.MOney += n;
                    Console.WriteLine($"Счёт изменен. Ваш счёт {B.U.MOney}");
                }
                Settings(B);
            }
            if (s == "2")
            {
                Console.WriteLine("Введите новый пароль");
                string s1 = Console.ReadLine();
                B.U.Password = s1;        //меняем пароль у текущего пользователя
                B.L.Find(A => A.Login == B.U.Login).Password = s1;   //меняем пароль у того же пользователя в списке аккаунтов
                Console.WriteLine("Пароль изменен");
                Settings(B);
            }
            if (s == "3")
            {
                B.L.Remove(B.U);
                Console.WriteLine("Пользователь удален");
                InputMenu(B);
            }
            if (s == "4")
            {
                MainMenu(B);
            }
        }
    }
}
