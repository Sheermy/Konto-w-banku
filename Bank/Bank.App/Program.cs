using System;
using Bank;
using static System.Net.Mime.MediaTypeNames;

namespace Bank.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Konto k1 = new Konto("Jan Kowalski", 500);
            KontoPlus kp = new KontoPlus("Anna Nowak", 200, 100);
            KontoLimit kl = new KontoLimit("Piotr Wiśniewski", 0, 150);

            Console.WriteLine("--- TEST KONTO ---");
            Console.WriteLine($"{k1.Nazwa}: {k1.Bilans}");
            k1.Wplata(100);
            k1.Wyplata(50);
            Console.WriteLine($"Po operacjach: {k1.Bilans}");

            Console.WriteLine("\n--- TEST KONTO PLUS ---");
            Console.WriteLine($"{kp.Nazwa}: {kp.Bilans}");
            kp.Wyplata(250);
            Console.WriteLine($"Po debecie (zablokowane: {kp.Zablokowane}): {kp.Bilans}");
            kp.Wplata(100);
            Console.WriteLine($"Po spłacie (zablokowane: {kp.Zablokowane}): {kp.Bilans}");

            Console.WriteLine("\n--- TEST KONTO LIMIT ---");
            Console.WriteLine($"{kl.Nazwa}: {kl.Bilans}");
            kl.Wyplata(120);
            Console.WriteLine($"Po debecie (zablokowane: {kl.Zablokowane}): {kl.Bilans}");

            try
            {
                kl.Wyplata(50);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Próba wypłaty z zablokowanego konta zakończona wyjątkiem.");
            }

            kl.Wplata(150);
            Console.WriteLine($"Po odblokowaniu: {kl.Bilans}");

            Console.ReadKey();
        }
    }
}
