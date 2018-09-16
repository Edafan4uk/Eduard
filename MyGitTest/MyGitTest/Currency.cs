using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MyGitTest
{
    internal enum CurrencyName
    {
        Grivna,
        Dollar,
        Euro,
        Funt
    }
    class Currency:IReadable
    {
        public CurrencyName Name { get; private set; }
        public Currency()
        {

        }
        private Currency(int am,CurrencyName name)
        {
            Name = name;
            Amount = am;
        }
        //public string CurrencyName { get; private set; }
        public int Amount { get; private set; }
        public List<IReadable> Read(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("File not found!");
                List<IReadable> list = new List<IReadable>();
                using (StreamReader sr = File.OpenText(path))
                {
                    
                    string temp = string.Empty;
                    string[] tempArr;
                    while(!sr.EndOfStream)
                    {
                        temp = sr.ReadLine();
                        List<string> curr = new List<string>() { "grn", "eur", "dol", "fun" };
                        tempArr = temp.Split(' ', ',', ';');
                        if (!int.TryParse(tempArr[0], out int amount)||tempArr[1]==string.Empty)
                            throw new ArgumentNullException();
                        if (!curr.Contains(tempArr[1]))
                            throw new FileNotFoundException("There are only four types of currencies!");
                        //list.Add(new Currency(amount, tempArr[1]));
                        string temp1 = tempArr[1];
                        switch(temp1)
                        {
                            case "grn":                                
                                list.Add(new Currency(amount, CurrencyName.Grivna));
                                break;
                            case "eur":
                                list.Add(new Currency(amount, CurrencyName.Euro));
                                break;
                            case "dol":
                                list.Add(new Currency(amount, CurrencyName.Dollar));
                                break;
                            case "fun":
                                list.Add(new Currency(amount, CurrencyName.Funt));
                                break;
                        }

                    }
                    return list;
                }
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public override string ToString()
        {
            return string.Format($"Amount:{Amount} Currency : {Name.ToString()}");
        }
    }
}
