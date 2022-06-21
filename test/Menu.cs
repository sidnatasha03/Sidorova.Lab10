using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carsharing
{
    public class Menu
    {
        public static void InputMenu()
        {
            Console.WriteLine("Взять в аренду (1) Сдать (2) Выход из программы (3)");
            string s = Console.ReadLine();     //выбираем действие
            if (s == "1")           //регистрация нового пользователя
            {
                Registration();
            }
            else if (s == "2")
            {
                Account account =new Account();
                Cars car = new Cars();
                Console.WriteLine("Введите Ваш номер телефона:");
                account.Phone =Console.ReadLine();
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
                InputMenu();
            }
        }
        public static void Registration()     //заполнение анкеты
        {
            Account account = new Account();
            Cars car = new Cars();
           

            Console.WriteLine("Введите ФИО");
            account.Name = Console.ReadLine();


            Console.WriteLine("Введите страну");
            account.Country = Console.ReadLine();


            Console.WriteLine("Введите возраст");
            account.Age = Convert.ToInt32(Console.ReadLine());
            try
            {

                if (Check.Finding1(account)==false)
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

            
            Console.WriteLine("Введите категорию Вашего водительского удостоверения: A, B, D");
            car.Category = Console.ReadLine();
            if (Check.Finding3(car)==false)
            {
                Console.WriteLine("Транспорта данной категории нет. Хотите продолжить?");
                string answ = Console.ReadLine().ToUpper();
                if (answ == "ДА")
                {
                    InputMenu();

                }
                else
                {
                    Environment.Exit(0);
                }
            }
                Console.WriteLine("Показываем доступный транспорт, соответствующий Вашей категории...");
                car.Operation("Вывести доступный транспорт и взять в аренду", car.Category,account);
              
                
              
            

        }
    }
}
