using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MyGitTest
{
    class Currency:IReadable
    {        
        public Currency(int am,string name)
        {
            CurrencyName = name;
            Amount = am;
        }
        public string CurrencyName { get; private set; }
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
                        tempArr = temp.Split(' ', ',', ';');
                        if (!int.TryParse(tempArr[0], out int amount)||tempArr[1]==string.Empty)
                            throw new ArgumentNullException();
                        list.Add(new Currency(amount, tempArr[1]));

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
            return string.Format($"Amount:{Amount} Currency : {CurrencyName}");
        }
    }
}
