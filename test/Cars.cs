using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carsharing
{
    public class Cars
    {
        public int Id { get; set; }
        public string Transport { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
       
        public void Operation(string typeoperation, string category, Account account)
        {
            var path = "Cars.csv";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding(1251);

            var lines = File.ReadAllLines(path, encoding);
            var cars = new Cars[lines.Length - 1];


            for (int i = 1; i < lines.Length; i++)
            {
                var splits = lines[i].Split(';');

                var car = new Cars();
                car.Id = Convert.ToInt32(splits[0]);
                car.Transport = splits[1];
                car.Category = splits[2];
                car.Quantity = Convert.ToInt32(splits[3]);
                car.Price = Convert.ToInt32(splits[4]);
                cars[i - 1] = car;


            }
            if (typeoperation == "Вывести доступный транспорт и взять в аренду")
           
            {
                var selecttransport = from p in cars
                                      where p.Category == category
                                      orderby p.Id
                                      select p;
                var selecttran1 = selecttransport.TakeLast(1);
                foreach (var x in selecttransport)
                {
                    Console.WriteLine(x);
                }
                Console.WriteLine("Выберите подходящий транспорт и введите его Id");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Вам сдан в аренду ");
                var o = cars[id-1];

                account.Order = cars[id - 1].Transport.ToString();
                var selecttransport1 = from s in cars
                                      where s.Id == id
                                      orderby s.Id 
                                      select s;
                var selecttran = selecttransport1.TakeLast(1);
                Console.WriteLine(o);

                account.DateOpen = DateTime.Now.ToString();
               
                Console.WriteLine("На какой срок Вы арендуете транспорт(сут.)?");
                var srok = Convert.ToInt32(Console.ReadLine());
               
                Console.WriteLine("Сумма к оплате:");
                Console.WriteLine(o.Price*srok);
                if(srok == 1)
                {
                    Console.WriteLine($"Спасибо за пользование нашего сервиса! Ждем Вас через {srok} днень!");
                }
                if (srok > 1 && srok < 5 )
                {
                    Console.WriteLine($"Спасибо за пользование нашего сервиса! Ждем Вас через {srok} дня!");
                }
                if (srok > 5)
                {
                    Console.WriteLine($"Спасибо за пользование нашего сервиса! Ждем Вас через {srok} дней!");
                }
                
                account.ChangeData("Взять в аренду", account);
                using (var writer = new StreamWriter(path, true, encoding))

                {
                    foreach (var e in selecttran)
                    {
                        var NewRecord = new List<Cars>()
                    {
                         new Cars { Id = e.Id , Transport = e.Transport,Category = e.Category,Quantity = e.Quantity-1, Price = e.Price}
                    };
                        foreach (var k in NewRecord)
                        {
                            writer.WriteLine(k.ToExcel());
                        }
                    }

                }

            }
            if (typeoperation == "Сдать")
            {
                Console.WriteLine("Назовите Id транспорта");
                int id = Convert.ToInt32(Console.ReadLine());
               
                var selecttransport1 = from s in cars
                                       where s.Id == id
                                       orderby s.Id 
                                       select s;
                var selecttran = selecttransport1.TakeLast(1);

                using (var writer = new StreamWriter(path, true, encoding))

                {
                    foreach (var e in selecttran)
                    {
                        var NewRecord = new List<Cars>()
                    {
                         new Cars { Id = e.Id , Transport = e.Transport,Category = e.Category,Quantity = e.Quantity+1, Price = e.Price,}
                    };
                        foreach (var k in NewRecord)
                        {
                            writer.WriteLine(k.ToExcel());
                        }
                    }

                }
                account.ChangeData("Сдать",account);

            }
        }

        public override string ToString()
        {
            return $"Id: {Id}\n Модель: {Transport}\n Категория : {Category }\n Количество: {Quantity}\n  Цена: {Price}\n ";
        }
        public string ToExcel()
        {
            return $"{Id};{Transport};{Category };{Quantity};{Price} ";
        }

      
    }
}
