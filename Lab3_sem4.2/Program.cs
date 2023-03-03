using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
namespace Lab3_sem4._2
{
    class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            for (int i = 1; i <= 5; i++)
            {
                List<Product> products = new List<Product>();

                for (int j = 1; j <= 10; j++)
                {
                    Product product = new Product
                    {
                        Name = $"Product {j}",
                        Category = $"Category {random.Next(1, 4)}",
                        Price = random.NextDouble() * 100
                    };

                    products.Add(product);
                }

                string json = JsonSerializer.Serialize(products);
                string filename = $"jsons\\{i}.json";

                using (StreamWriter sw = File.CreateText(filename))
                {
                    sw.Write(json);
                }
            }

            string directoryPath = @"jsons\";
            string category = "Category 3";
            double minPrice = 6.0;
            double maxPrice = 66.0;

            Predicate<Product> filter = p => p.Category == category && p.Price >= minPrice && p.Price <= maxPrice;
            Action<Product> display = p => Console.WriteLine($"Назва: {p.Name}, Категорія: {p.Category}, Ціна: {p.Price}");

            for (int i = 1; i <= 5; i++)
            {
                string filePath = Path.Combine(directoryPath, $"{i}.json");
                ReadProducts(filePath, filter, display);
            }
        }

        static void ReadProducts(string filePath, Predicate<Product> filter, Action<Product> display)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                Product[] products = JsonSerializer.Deserialize <Product[]>(json);
                products.Where(new Func<Product, bool>(filter)).ToList().ForEach(display);
            }
        }
    }
}