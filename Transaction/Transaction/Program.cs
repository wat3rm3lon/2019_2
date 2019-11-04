using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace TransactionConsole
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

    }
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options)
            : base(options)
        {

        }
        public DbSet<Transaction> Transactions { get; set;  }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: "TransactionsDB")
                .Options;
            
            using (var context = new TransactionContext(options))
            {
                /*
                context.Transactions.Add(new Transaction { Id = 1, TransactionDate = DateTime.Now, Amount = 13.44M });
                context.Transactions.Add(new Transaction { Id = 2, TransactionDate = DateTime.Now, Amount = 12.44M });
                context.Transactions.Add(new Transaction { Id = 3, TransactionDate = DateTime.Now, Amount = 23.44M });
                context.SaveChanges();
                */
                
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Введите команду add/get/exit");
                        string str = Console.ReadLine().Trim().ToLower();
                        if (str == "exit")
                        {
                            break;
                        }
                        else if (str == "add")
                        {
                            Console.Write("Введите Id:");

                            int id = int.Parse(Console.ReadLine().Trim());
                            if(id < 1)
                                throw new ArgumentException("Id не может быть меньше 0");

                            Console.Write("Введите дату:");
                            //22.12.2019
                            var date = DateTime.ParseExact(Console.ReadLine().Trim(), "dd.MM.yyyy", null);

                            Console.Write("Введите сумму:");
                            decimal amount = Decimal.Parse(Console.ReadLine().Trim());

                            var trxn = context.Transactions.SingleOrDefault(e => e.Id == id);
                            if (trxn != null)
                            {
                                trxn.TransactionDate = date;
                                trxn.Amount = amount;
                            }
                            else
                            {
                                context.Add(new Transaction { Id = id, TransactionDate = date, Amount = amount });
                            }
                            context.SaveChanges();
                            
                            Console.WriteLine("ОК");
                        }
                        else if (str == "get")
                        {
                            Console.Write("Введите Id:");
                            int id = int.Parse(Console.ReadLine().Trim());
                            if (id < 1)
                                throw new ArgumentException("Id не может быть меньше 0");

                            var transaction = context.Transactions.Find(id);
                            if (transaction == null)
                                throw new ArgumentException(string.Format("Не найдена транзакция с id = '{0}'", id));
                            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(transaction));
                            Console.WriteLine("ОК");
                        }
                        else
                        {
                            throw new ArgumentException(string.Format("Недопустимая команда '{0}'", str));
                        }
                        /*
                        foreach (var trxn in context.Transactions)
                        {
                            Console.WriteLine(trxn.Id + " " + trxn.TransactionDate + " " + trxn.Amount);
                            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(trxn));
                        }
                        */
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }
            }
        }
    }
}
