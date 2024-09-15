using System;
using System.Linq;
using ClientAppDb;
using Microsoft.EntityFrameworkCore;

namespace TrainStationApp
{
    namespace TrainStationApp
    {
        class Program
        {
            static void Main()
            {
                using (var context = new ApplicationContext())
                {
                    
                    //context.Database.EnsureDeleted();
                    //context.Database.EnsureCreated();

                    //    // Создание хранимой процедуры
                    //    context.Database.ExecuteSqlRaw(@"
                    //    IF OBJECT_ID('GetRecentClients', 'P') IS NOT NULL
                    //        DROP PROCEDURE GetRecentClients;");

                    //    context.Database.ExecuteSqlRaw(@"
                    //    CREATE PROCEDURE GetRecentClients
                    //    AS
                    //    BEGIN
                    //        SELECT 
                    //            c.ClientId,
                    //            c.Name,
                    //            c.Email,
                    //            c.PhoneNumber,
                    //            COUNT(p.PurchaseId) AS NumberOfPurchases,
                    //            SUM(p.Amount) AS TotalSpent
                    //        FROM 
                    //            Clients c
                    //        INNER JOIN 
                    //            Purchases p ON c.ClientId = p.ClientId
                    //        WHERE 
                    //            p.PurchaseDate >= DATEADD(DAY, -30, GETDATE())
                    //        GROUP BY 
                    //            c.ClientId, c.Name, c.Email, c.PhoneNumber
                    //        ORDER BY 
                    //            TotalSpent DESC;
                    //    END;");

                    //// Вставка данных
                    //context.Database.ExecuteSqlRaw(@"
                    //    INSERT INTO Clients (Name, Email, PhoneNumber) VALUES 
                    //    ('Dana Scully', 'scully.dana@gmail.com', '0955289873'),
                    //    ('Fox Mulder', 'mulder.fox@gmail.com', '0987654321');

                    //    INSERT INTO Purchases (ClientId, PurchaseDate, Amount) VALUES 
                    //    (1, GETDATE(), 100.00),
                    //    (1, DATEADD(DAY, -10, GETDATE()), 150.00),
                    //    (2, DATEADD(DAY, -20, GETDATE()), 200.00);");

                    // Вызов хранимой процедуры
                    var recentClients = context.Clients
                        .FromSqlRaw("EXEC GetRecentClients")
                        .ToList();

                    Console.WriteLine("Клиенты, совершившие покупки за последние 30 дней:");
                    foreach (var client in recentClients)
                    {
                        Console.WriteLine($"Имя: {client.Name}, Электронная почта: {client.Email}, Телефон: {client.PhoneNumber}");
                    }
                }
            }
        }
    }
}