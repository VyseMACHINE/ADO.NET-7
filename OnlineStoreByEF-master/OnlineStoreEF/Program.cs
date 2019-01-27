using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StoreDbContext db = new StoreDbContext())
            {
                db.Categories.Add(new Category { Name = "Ноутбуки" });
                db.Categories.Add(new Category { Name = "Нетбуки" });
                db.Categories.Add(new Category { Name = "Смартфоны" });
                db.Categories.Add(new Category { Name = "Мобильные телефоны" });
                db.Categories.Add(new Category { Name = "Аксессуары" });

                db.SaveChanges();

                int smrtphoneId = db.Categories.ToList().Find(item => item.Name == "Смартфоны").Id;
                int notebookId = db.Categories.ToList().Find(item => item.Name == "Ноутбуки").Id;
                int accessoryId = db.Categories.ToList().Find(item => item.Name == "Аксессуары").Id;

                db.Products.Add(new Product { Name = "Samsung Galaxy S5", Count = 20, Measure = "шт.",
                    Price = 100000, CategoryId = smrtphoneId });
                db.Products.Add(new Product { Name = "Samsung Galaxy S4", Count = 40, Measure = "шт.",
                    Price = 90000, CategoryId = smrtphoneId });
                db.Products.Add(new Product { Name = "LG G4", Count = 30, Measure = "шт.",
                    Price = 120000, CategoryId = smrtphoneId });
                db.Products.Add(new Product { Name = "ASUS XR242", Count = 10, Measure = "шт.",
                    Price = 250000, CategoryId = notebookId });
                db.Products.Add(new Product { Name = "ACER R3", Count = 40, Measure = "шт.",
                    Price = 300000, CategoryId = notebookId });
                db.Products.Add(new Product { Name = "Наушники", Count = 100, Measure = "шт.",
                    Price = 2000, CategoryId = accessoryId });
                db.Products.Add(new Product { Name = "Светильник", Count = 50, Measure = "шт.",
                    Price = 5000, CategoryId = accessoryId });

                db.Clients.Add(new Client { Fio="Сабитов Ильяс Маратович", BirthDate=new DateTime(1992,2,8),
                    Phone =123456789, Email="1231@mail.ru"});
                db.Clients.Add(new Client { Fio="Кентаев Жанат Еркинович", BirthDate=new DateTime(1991,3,10),
                    Phone =123536236, Email="ыпывып@mail.ru"});
                db.Clients.Add(new Client { Fio="Сабуров Нурлан Канатович", BirthDate=new DateTime(1990,1,9),
                    Phone =152552262, Email="пфпфпфп@mail.ru"});
                db.Clients.Add(new Client { Fio="Исекешев Асет Толегенович", BirthDate=new DateTime(1995,11,18),
                    Phone =457474747, Email="54235аыпыып@mail.ru"});

                db.SaveChanges();

                int firstClientId = db.Clients.ToList().Find(item => item.Fio == "Сабитов Ильяс Маратович").Id;
                int secondClientId = db.Clients.ToList().Find(item => item.Fio == "Сабуров Нурлан Канатович").Id;
                int firstProductId = db.Products.ToList().Find(item => item.Name == "Samsung Galaxy S5").Id;
                int secondProductId = db.Products.ToList().Find(item => item.Name == "ASUS XR242").Id;

                db.Orders.Add(new Order { ClientId = firstClientId, ProductId = firstProductId,
                    Summa = 100126.00M, Date = DateTime.Now });
                db.Orders.Add(new Order { ClientId = secondClientId, ProductId = secondProductId,
                    Summa = 250126.12M, Date = DateTime.Now });

                db.SaveChanges();

                var orders = db.Orders.ToList();
                foreach(var order in orders)
                {
                    db.Entry(order).Reference("Product").Load();
                    db.Entry(order).Reference("Client").Load();
                }

                Console.WriteLine("------ЧЕКИ------");
                foreach(var order in orders)
                {
                    Console.WriteLine("ID: {0}\nНазвание продукта: {1}\nФИО клента: {2}\nСумма заказа: {3}\nДата заказа: {4}", 
                        order.Id, order.Product.Name, order.Client.Fio, order.Summa, order.Date);
                }
            }
        }
    }
}
