using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGitTest
{
    public static class Tasks
    {
        private static double ConvertTo(CurrencyName currencyName, List<IReadable> list, params object[] exchangeRateRatio)
        {
            try
            {
                if (exchangeRateRatio.Length != 3)
                    throw new Exception();
                Tuple<CurrencyName, double>[] arr = new Tuple<CurrencyName, double>[exchangeRateRatio.Length];
                for (int i = 0; i <exchangeRateRatio.Length; i++)
                {
                    arr[i] = (Tuple<CurrencyName, double>)exchangeRateRatio[i];
                }
                double sum = 0;
                var groupedList = (from a in list.Cast<Currency>()
                                  group a by a.Name into b
                                  select new { b.Key, Sum = (from g in b select g.Amount).Sum() }).ToArray();
                for (int i = 0; i < groupedList.Length; i++)
                {
                    for (int j= 0; j < arr.Length; j++)
                    {
                        if (groupedList[i].Key == currencyName)
                            sum += groupedList[j].Sum;
                        else if (groupedList[i].Key != arr[j].Item1)
                        {
                            continue;
                        }
                        else
                            sum += groupedList[i].Sum * arr[j].Item2;
                    }
                }
                return sum;
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                return 0;
            }
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

            var grnCurrencies = from a in list.Cast<Currency>() where a.Name == CurrencyName.Grivna select a;

            foreach(var grn in grnCurrencies)
            {
                Console.WriteLine(grn);
            }
        }
        public static void UsingLINQGroupBy()
        {
            var list = new Currency().Read(@"D:\робочий стіл\gitTest\MyGitTest\MyGitTest\ReadFromHere.txt");

            var groupedList = from a in list.Cast<Currency>()
                              group a by a.Name into b
                              select new { b.Key, Sum = (from g in b select g.Amount).Sum() };
            foreach(var group in groupedList)
            {
                Console.WriteLine($"Currency: {group.Key};Sum: {group.Sum}");
            }
        }  
        //public static void UsingConvert()
        //{
        //    try
        //    {
        //        double sum = 0;
        //        Console.WriteLine("Input currency of convertion:");
        //        string temp = Console.ReadLine();
        //        List<string> curr = new List<string>() { "grn", "eur", "dol", "fun" };
                
        //        if (!curr.Contains(temp))
        //            throw new Exception("There are only four types of currencies!");
        //        switch (temp)
        //        {
        //            case "grn":
        //                ConvertTo(CurrencyName.Grivna,
        //                    new Currency().Read(@"D:\робочий стіл\gitTest\MyGitTest\MyGitTest\ReadFromHere.txt"),
        //                    new object[] { new Tuple<CurrencyName,double>(CurrencyName.Dollar,0.1),
        //                    new Tuple<CurrencyName,double})
        //                break;
        //            case "eur":
        //                list.Add(new Currency(amount, CurrencyName.Euro));
        //                break;
        //            case "dol":
        //                list.Add(new Currency(amount, CurrencyName.Dollar));
        //                break;
        //            case "fun":
        //                list.Add(new Currency(amount, CurrencyName.Funt));
        //                break;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}
