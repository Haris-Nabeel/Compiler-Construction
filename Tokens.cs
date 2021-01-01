using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler
{
  public class Tokens
    {
        string []keywords = { "for", "while", "if", "else", "extends", "super","return", "void", "class", "virtual", "override", "new", "this", "final", "static", "public", "private", "protected", "try", "Number", "String", "bool", "true", "false", "break", "continue", "null" };
        public string VP,CP;
        public int lineNo;
       // public List<Tokens> t = new List<Tokens>();
        public Tokens()
        {

        }
        public Tokens(string VP,int lineNo)
        {
            this.VP = VP;
            this.lineNo = lineNo;
        }

        public Tokens(string CP, string VP,int lineNo)
        {
            this.CP = CP;
            this.VP = VP;
            this.lineNo = lineNo;    
        }

        public void classify()
        {
            Tokens word;
            for (int i = 0; i < WordBreaker.lexems.Count; i++)
            {
                word = WordBreaker.lexems[i];
                //Console.WriteLine(word.CP);
                if (word.VP[0].ToString() == "\"")
                {
                    word.CP = isStringConstant(word.VP);
                } else if (word.VP[0] >='0' && word.VP[0] <='9')  // when number
                {
                    word.CP = isNumberConstant(word.VP);
                }
                else if (word.VP[0].ToString() == "." && word.VP.Length==1)  //when .
                {
                    word.CP = "DOT";
                }
                else if (word.VP[0].ToString() == ".")
                {
                    word.CP = isNumberConstant(word.VP);
                }
                else if ((Convert.ToChar( word.VP[0])>='a'&& Convert.ToChar(word.VP[0]) <= 'z')|| ((Convert.ToChar(word.VP[0]) >= 'A' && Convert.ToChar(word.VP[0]) <= 'Z')) ||(Convert.ToChar(word.VP[0]) == '_'))
                {//when alphabet 
                    word.CP = isKW(word.VP);
                }
                else if (word.CP == "")  // when no class part is assigned
                {
                    word.CP = "Invalid";
                }

                if (word.CP == "StringConstant") // means its a correct string.. then save wihtout quotes..
                {

                    string sWithoutcomma = string.Empty;
                    for (int j = 1; j < word.VP.Length-1; j++)
                    {
                        sWithoutcomma = sWithoutcomma + word.VP[j];
                    }
                    word.VP = sWithoutcomma;
                 
                    //string s;
                    //s = string.Empty;
                    //if(vPart[0].ToString()=="\"" && vPart[vPart.Length-1].ToString() == "\"")
                    //{
                    //    for (int i = 1; i < vPart.Length - 1; i++)
                    //    {
                    //        s = s + vPart[i];
                    //    }
                    //    Console.WriteLine("yes");
                    //}
                }


            }

           
        }

        public string isStringConstant(string vPart)
        {
       
            //else
            //{
            //    s = vPart;
            //    Console.WriteLine("no");
            //}
           
        
            string pattern =@"^\""([a-zA-Z0-9]{0,1}([\\][torbn0\\\'\""]){0,1}|[^\\])*\""$";  //@"^[[\(t|n|r)]*[^\]*]*$"   @"^\""[\\(t|r|n|0|b|f|""|a|v|')a-zA-Z0-9,\.\*/;\~!\{\}@#\$%\^:&\(\)\+\[\]<>_\?=\-`]*\""$"
            Regex obj = new Regex(pattern);
            if (obj.IsMatch(vPart))
            {
                return "StringConstant";
            }
            return "INVALID";
        }

        public string isNumberConstant(string vPart)
        {
            string pattern = @"^([0-9]*\.?)[0-9]+$";
            Regex obj = new Regex(pattern);
            if (obj.IsMatch(vPart))
            {

            return "NumberConstant";
            }
            else
            {
                return "INVALID";
            }
        }

        public string isId(string vPart)
        {
            string pattern = @"^[_a-zA-Z]*[_a-zA-Z0-9]*[a-zA-Z0-9]+$";
            Regex obj = new Regex(pattern);
            if (obj.IsMatch(vPart))
            {
                
                return "ID";
            }
            return "INVALID";
        }

        public string isKW(string vPart)
        {
            foreach(string v in keywords)
            {
                if (vPart == v)
                {
                  
                    return v;
                }
            }
            return isId(vPart);
        }

        

       




    }

}
