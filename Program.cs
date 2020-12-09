using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileContent = System.IO.File.ReadAllText(@"C:\Users\haris\Desktop\test.txt");
            WordBreaker wb = new WordBreaker();
            wb.breakWords(fileContent);
            string pattern = @"^$";
            Regex obj = new Regex(pattern);
            string data = "_";
            Console.WriteLine(obj.IsMatch(WordBreaker.lexems[0].VP).ToString());
          //  string s = "/*haris*/";
           // string s = "haris-_`~!@#$%^&*()_+}{:><.,][/?|";
            //Console.WriteLine(wb.lexems.Count);
            //wb.getTokens();
            //Tokens t = new Tokens();
            //t.classify();
            

            // wb.IsDigitsOnly("24324323243241234567890987654321123456789098765432123456789098765432145678909876543234567898765434567899876543234567898765412345678909876543212345678909876543212345678909876543212345678909876543211234567890987654321234567890987654321");
            //int a, b, c;
            //a = 4; b = 5; c = 6;
            //if (a==4 & b == 1)
            //{
            //    Console.WriteLine("p");
            //}
            //{
            //    Console.WriteLine("apapapap");
            //}
            //if (fileContent[3].ToString() == "\r")
            //{
            //    Console.WriteLine("yes");
            //}
            //else
            //{
            //    Console.WriteLine("no");
            //}
            //for (int i = 0; i < fileContent.Length; i++)
            //{
            //    Console.WriteLine(fileContent[i].ToString());
            //}
            //Console.WriteLine(wb.lexems.Count);
            //bool a =wb.isTempEmpty();
            //Console.WriteLine(a);
            //string a = "pakistan";
            //Console.WriteLine(a[0]);
        }
    }
}
