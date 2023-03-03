using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string csvFilePath = "transactions.csv";
        string dateFormat = "dd.MM.yyyy";

        Func<string, DateTime> getDate = (line) => DateTime.ParseExact(line.Split(',')[0], dateFormat, null);
        Func<string, double> getAmount = (line) => double.Parse(line.Split(',')[1]);
        Action<DateTime, double> printTotal = (date, total) => Console.WriteLine($"{date.ToString(dateFormat)}: {total}");

        Dictionary<DateTime, List<double>> transactionsByDate = new Dictionary<DateTime, List<double>>();
        using (StreamReader reader = new StreamReader(csvFilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                DateTime date = getDate(line);
                double amount = getAmount(line);

                if (!transactionsByDate.ContainsKey(date))
                {
                    transactionsByDate[date] = new List<double>();
                }
                transactionsByDate[date].Add(amount);
            }
        }

        foreach (var kvp in transactionsByDate)
        {
            DateTime date = kvp.Key;
            List<double> amounts = kvp.Value;

            double total = amounts.Sum();
            printTotal(date, total);
        }
    }
}