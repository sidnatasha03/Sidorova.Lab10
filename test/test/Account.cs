using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Account
    {
        public int Id { get; set; }
        
        public string Login { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Order { get; set; }
        public bool Status_Order { get; set; }  //авто
        public string DateOpen { get; set; }
        public string DateClose { get; set; }

        public  void ChangeData(string typeoperation,Account account)
        {
            var path = @"C:\Users\пёс\source\repos\test\Accounts.csv";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding(1251);

            var lines = File.ReadAllLines(path, encoding);
            var accounts = new Account[lines.Length - 1];

            List<int> listI = new List<int>();
          
            
            for (int i = 1; i < lines.Length; i++)
            {
                var splits = lines[i].Split(';');

                var accountt = new Account();
                accountt.Id = Convert.ToInt32(splits[0]);
                accountt.Name = splits[1];
                accountt.Age = Convert.ToInt32(splits[2]);
                accountt.Country = splits[3];
                accountt.Phone = splits[4];
                accountt.Email = splits[5];
                accountt.Order = splits[6];
                accountt.Status_Order = Convert.ToBoolean(splits[7]);
                //account.DateOpen = splits[8];
                //account.DateClose = splits[9];
                account.Login = splits[8];
                account.Password = splits[9];
                accounts[i - 1] = accountt;
                listI.Add(Convert.ToInt32(splits[0]));

            }
            if (typeoperation == "Сдать")
            {
                var ss = from s in accounts
                         where s.Phone == account.Phone
                         orderby s.Id
                         select s;
                var ss1 = ss.TakeLast(1);
                using (var writer = new StreamWriter(path, true, encoding))

                {
                    foreach (var v in ss1)
                    {
                        var NewRecord = new List<Account>()
                        {
                        new Account { Id = listI.Count , Name = v.Name,Age = v.Age,Country=v.Country,Phone = v.Phone, Email =  v.Email, Order = v.Order, Status_Order = true, Login = v.Login, Password = v.Password}
                        };
                        foreach (var k in NewRecord)
                        {
                            writer.WriteLine(k.ToExcel());
                        }
                    }

                }

            }
            if (typeoperation == "Взять в аренду")
            {

                using (var writer = new StreamWriter(path, true, encoding))

                {
                    var NewRecord = new List<Account>()
                    {
                    new Account { Id = listI.Count , Name = account.Name,Age = account.Age,Country=account.Country,Phone = account.Phone, Email =  account.Email, Order = account.Order, Status_Order = false, Login = account.Login, Password = account.Password}
                    };
                    foreach (var k in NewRecord)
                    {
                        writer.WriteLine(k.ToExcel());
                    }

                }
            }
        }
        public string ToExcel()
        {
            return $"{Id};{Name};{Age};{Country};{Phone};{Email};{Order};{Status_Order};{DateOpen};{DateClose}";
        }

    }
}
