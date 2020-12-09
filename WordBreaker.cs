using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Compiler
{
    class WordBreaker
    {
        string[,] operators = { { "+", "PM" }, { "-", "PM" } ,{"*","MDM" }, { "/", "MDM" }, { "%", "MDM" }, { "&&", "AND" }, { "||", "OR" }, { "=", "EQUAL" }, { "<", "COMP" }, { ">", "COMP" }, { "<=", "COMP" }, { ">=", "COMP" }, { "!=", "COMP" }, { "==", "COMP" }, { "!", "NOT" }, { "+=", "CAO" }, { "-=", "CAO" }, { "*=", "CAO" }, { "/=", "CAO" }, { "++", "INCDEC" }, { "--", "INCDEC" } };
        string[] breakers = { " ", "\n", "\t","\r", "\"", "+", "-", "*", "/", "=", "<", ">", "!","%", "&", "|", ";",  "(",")","{","}",",","[","]","."};
        string temp = string.Empty;
        int lineNo = 1;
        int quoteCount = 0;
        bool isCompound = false;
        string CP;
       public static List<Tokens> lexems = new List<Tokens>();
        //public void breakWords(string srcCode)
        //{

        //    for (int i = 0; i < srcCode.Length; i++)
        //    {

        //        if (isBreaker(srcCode[i].ToString())) // if character is a breaker
        //        {
        //            if (!isTempEmpty())
        //            {
        //                lexems.Add(new Tokens(temp,lineNo));
        //                temp = string.Empty;
        //            }

        //            if (srcCode[i].ToString() == " "|| srcCode[i].ToString() == "\r" || srcCode[i].ToString() == "\"" || srcCode[i].ToString() == "\n" || srcCode[i].ToString() == "\t" || srcCode[i].ToString() == ";")
        //            {
        //                if (srcCode[i].ToString() == "\r")
        //                {
        //                    lineNo++;
        //                }
        //            }
        //            else
        //            {
        //                lexems.Add(new Tokens(srcCode[i].ToString(),lineNo));

        //            }
        //        }

        //        else // if char is not breaker
        //        {
        //            temp = temp + srcCode[i].ToString();

        //        }

        //    }
        //    writeTokens();

        //}



        public void breakWords(string srcCode)
        {
            int i = 0;
            while (i < srcCode.Length)
            {
                 int a = isBreaker(srcCode[i].ToString());
                if (a != 6) // when its a breaker....
                {
                    if (a == 0)  //when whitespace
                    {
                        if (!isTempEmpty())
                        {
                            makeTokenWhenTempFull();
                        }
                        if(srcCode[i].ToString()=="\n" )  //if there is new line
                        {
                            lineNo++;
                        }
                    }
                    else if (a == 2)  // when punctutators
                    {
                        makeTokenWhenTempFull();
                        
                       lexems.Add(new Tokens(srcCode[i].ToString(),srcCode[i].ToString(),lineNo));
                       
                    }
                    else if (a==4) // when double quote "
                    {

                        makeTokenWhenTempFull();
                        quoteCount++;
                        temp = temp + srcCode[i].ToString();
                        while (quoteCount == 1)
                        {
                            if (i + 1 < srcCode.Length)
                            {
                                
                                if (srcCode[i + 1].ToString() != "\"" && srcCode[i + 1].ToString() != "\r" && srcCode[i + 1].ToString() != "\n")
                                {
                                    
                                    temp = temp + srcCode[i + 1].ToString();
                                }
                                else
                                {
                                    if (srcCode[i + 1].ToString() == "\"" && srcCode[i].ToString()=="\\")
                                    {
                                        temp = temp + srcCode[i + 1].ToString();
                                        
                                    }
                                    else if (srcCode[i + 1].ToString() == "\"")
                                    {
                                        temp = temp + srcCode[i+1].ToString();
                                        lexems.Add(new Tokens(temp, lineNo));
                                        temp = string.Empty;
                                        quoteCount++;
                                    }
                                    else
                                    {

                                       // temp = temp.ToString();
                                        lexems.Add(new Tokens(temp, lineNo));
                                        temp = string.Empty;
                                        quoteCount++;
                                        
                                    }
                                }
                               
                            }
                            else
                            {
                                temp = temp + srcCode[i].ToString();
                                quoteCount++;
                                lexems.Add(new Tokens(temp, lineNo));
                                temp = string.Empty;
                            
                            }
                            i++;
                        }
                        quoteCount = 0;
                       
                    }
                    else if (a==1)  // when compound
                    {
                        makeTokenWhenTempFull();
                        
                        if (srcCode[i + 1].ToString() == "=" && (srcCode[i].ToString()=="<" || srcCode[i].ToString() == ">" || srcCode[i].ToString() == "!" || srcCode[i].ToString() == "="))
                        {
                            temp = srcCode[i].ToString() + srcCode[i+1].ToString();
                            CP = "RO";
                            makeTokenWhenTempFull();
                            CP = string.Empty;

                            i++;
                        }
                        else if(srcCode[i + 1].ToString() == "=")
                        {
                            temp = srcCode[i].ToString() + srcCode[i + 1].ToString();
                            CP = "CAO";
                            makeTokenWhenTempFull();
                            CP = string.Empty;

                            i++;
                        }
                        else if (srcCode[i].ToString()=="&" && srcCode[i+1].ToString()=="&")
                        {
                            temp = srcCode[i].ToString() + srcCode[i + 1].ToString();
                            CP = "AND";
                            makeTokenWhenTempFull();
                            CP = string.Empty;
                            i++;
                        }
                        else if(srcCode[i].ToString() == "|" && srcCode[i + 1].ToString() == "|")
                        {
                            temp = srcCode[i].ToString() + srcCode[i + 1].ToString();
                            CP = "||";
                            makeTokenWhenTempFull();
                            CP = string.Empty;
                            i++;
                        }
                        else if (srcCode[i].ToString()=="+" && srcCode[i+1].ToString()=="+")
                        {
                            temp = srcCode[i].ToString() + srcCode[i + 1].ToString();
                            CP = "INCDEC";
                            makeTokenWhenTempFull();
                            CP = string.Empty;
                            i++;
                        }
                        else if (srcCode[i].ToString() == "-" && srcCode[i + 1].ToString() == "-")
                        {
                            temp = srcCode[i].ToString() + srcCode[i + 1].ToString();
                            CP = "INCDEC";
                            makeTokenWhenTempFull();
                            i++;
                        }
                        else if(srcCode[i].ToString()=="/" && srcCode[i + 1].ToString() == "/") // when single line comment
                        {
                            temp = string.Empty;
                            while(i+1< srcCode.Length)
                            {
                                if (srcCode[i].ToString() == "\r")
                                {
                                    break;
                                }
                                i++;
                            }
                        }
                        else if(srcCode[i].ToString()=="/" && srcCode[i + 1].ToString() == "*")  // when multiline comment
                        {
                            i = i + 2;
                            temp = string.Empty;
                            while (i + 1 < srcCode.Length)
                            {
                                if (srcCode[i].ToString() == "*" && srcCode[i+1].ToString()=="/")
                                {
                                    i++;
                                    break;
                                }
                                i++;
                            }
                        }
                        else
                        {
                            if (srcCode[i].ToString() == "=")
                            {
                                CP = "EQUAL";
                            }
                            else if (srcCode[i].ToString() == "+" || srcCode[i].ToString() == "-")
                            {
                                CP = "PM";
                            }
                            else if (srcCode[i].ToString() == "*" || srcCode[i].ToString() == "/" || srcCode[i].ToString() == "%")
                            {
                                CP = "MDM";
                            }
                            else if (srcCode[i].ToString() == "<" || srcCode[i].ToString() == ">")
                            {
                                CP = "RO";
                            }
                            else CP = "INVALID";
                            
                            temp=srcCode[i].ToString();
                            makeTokenWhenTempFull();
                            CP = string.Empty;
                        }
                    }
                    else if (a == 3)  //when . period
                    {
                        if (IsDigitsOnly())
                        {
                            
                        }   
                    }
                    
                   
                }
                
                else      // when its not a breaker
                {
                    temp = temp + srcCode[i].ToString();
                }
                i++;
                
            }
            makeTokenWhenTempFull();
            writeTokens();
        }



        public int isBreaker(string s)
        {
            if ((Array.IndexOf(breakers, s) >= 0) && (Array.IndexOf(breakers, s) < 4))  // if white spaces
            {
                return 0;
            }
            else if ((Array.IndexOf(breakers, s) > 4) && (Array.IndexOf(breakers, s) < 16)) // if Combined operators
            {
                return 1;
            }
            else if ((Array.IndexOf(breakers, s) >= 16) && (Array.IndexOf(breakers, s) < 24)) // when punctuators
            {
                return 2;
            }
            else if (Array.IndexOf(breakers, s) == 24) // when period (.)
            {
                return 3;
            }
            else if ((Array.IndexOf(breakers, s) == 4))
            { // when double quotes "
                return 4;
            }
            else return 6;
            
        }

        public Boolean isTempEmpty()     // checking temp
        {
            if (temp == "")
            {
                return true;
            }
            else return false;
        }

        public void writeTokens()   // writing tokens in file..
        {
           // System.IO.File.WriteAllText(@"C:\Users\haris\Desktop\tokens.txt",tokens.ToString());
            using (StreamWriter sw = File.AppendText(@"C:\Users\haris\Desktop\tokens.txt"))
            {
                for (int i = 0; i < lexems.Count; i++)
                {
                    //sw.WriteLine(lexems[i].ToString());
                    Tokens t = (Tokens)lexems[i];
                    Console.WriteLine("("+ t.CP + ","+ t.VP+","+ t.lineNo + ")" );
                    sw.WriteLine("("+ t.CP+","+t.VP+","+ t.lineNo + ")" );
                }
            }
        }

        public void makeTokenWhenTempFull()
        {
            if (isTempEmpty() == false)
            {
                lexems.Add(new Tokens(CP,temp.ToString(), lineNo));
                temp = string.Empty;
            }
        }

       public bool IsDigitsOnly()
        {
            foreach (char c in temp)
            {
                if (c < '0' || c > '9')
                    return false;    
            }
            return true;
        }

        

    }
}
