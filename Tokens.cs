using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
  public class Tokens
    {
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
                if (word.VP[0].ToString() == "\"")
                {

                    word.CP = isStringConstant(word.VP);
                } else if (word.VP[0] >='0' && word.VP[0] <='9')
                {
                    word.CP = isNumberConstant(word.VP);
                }
                else if (word.VP[0].ToString() == "\"")
                {

                }
              

            } 
        }

        public string isStringConstant(string vPart)
        {
            return "INVALID";
        }

        public string isNumberConstant(string vPart)
        {
            return "INVALID";
        }

        public string isId(string vPart)
        {
            return "INVALID";
        }

        public string isKW(string vPart)
        {
            return "INVALID";
        }




    }

}
