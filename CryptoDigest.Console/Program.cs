

namespace CryptoDigest.Console
{
    using System;
    using CryptoDigest.Importers;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var records = ImporterRepository.GetAllCurrencyHistoryItems();

            foreach (var record in records)
            {
                Console.WriteLine($"{record.Currency}\t{record.Date}\t{record.Value:C}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any ket to exit.");
            Console.Read();
        }
    }
}
