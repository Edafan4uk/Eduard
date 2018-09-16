using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGitTest
{
    public static class Tasks
    { 
        private static void ConvertTo()
        {

        }
        public static void ReadFromFile()
        {
            Currency currency = new Currency();

            List<IReadable> currencies = currency.Read(@"D:\робочий стіл\gitTest\MyGitTest\MyGitTest\ReadFromHere.txt");

            foreach(Currency currency1 in currencies)
            {
                Console.WriteLine(currency1.ToString());
            }

        }
        public static void PrintOnlyGrn()
        {
            var list = new Currency().Read(@"D:\робочий стіл\gitTest\MyGitTest\MyGitTest\ReadFromHere.txt");

            var grnCurrencies = from a in list.Cast<Currency>() where a.CurrencyName == "grn" select a;

            foreach(var grn in grnCurrencies)
            {
                Console.WriteLine(grn);
            }
        }
        public static void UsingLINQGroupBy()
        {
            var list = new Currency().Read(@"D:\робочий стіл\gitTest\MyGitTest\MyGitTest\ReadFromHere.txt");

            var groupedList = from a in list.Cast<Currency>()
                              group a by a.CurrencyName into b
                              select new { b.Key, Sum = (from g in b select g.Amount).Sum() };
            foreach(var group in groupedList)
            {
                Console.WriteLine($"Currency: {group.Key};Sum: {group.Sum}");
            }
        }  
    }
}
