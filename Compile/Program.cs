using System;
using System.IO;
using System.Text;

namespace Compile
{
    public enum KeyWords
    {
        BEGIN = 257, END = 258, IF = 259, DO = 260, WHILE = 261, NUMB = 262, IDEN = 263
    }

    class Program
    {
        private static char nch = '\n';
        private static int lex;
        private static int lval;
        private static string inputFile = "D:\\projects\\SYSPROG\\lab\\Compile\\Compile\\iofiles\\input.txt";
        private static string outputFile = "D:\\projects\\SYSPROG\\lab\\Compile\\Compile\\iofiles\\output.txt";
        private static StreamReader sr = new StreamReader(inputFile, Encoding.UTF8);
        private static StreamWriter sw = new StreamWriter(outputFile);
        private static int ptn;
        private static String[] TNM = new String[400];

        static void Main(string[] args)
        {
            Get();
            sw.Close();
            
            Console.ReadKey();
        }

        public static char GetC()
        {
            return (char) sr.Read();
        }

        public static void Get()
        {
            while (sr.Peek() >= 0)
            {
                while (nch == ' ' || Char.IsControl(nch))
                {
                    nch = GetC();
                }

                if (Char.IsLetter(nch))
                {
                    Word();
                }
                else if (Char.IsDigit(nch))
                {
                    Number();
                }
                else if (nch == '(' || nch == ')' || nch == ',' || nch == '=' || nch == ';' ||
                            nch == '+' || nch == '-' || nch == '*' || nch == '/' || nch == '%' || nch == '<' || nch == '>')
                {
                    lex = (int)nch;
                    Console.WriteLine("lexeme = " + lex + " \'" + (char)lex + "\'");
                    sw.WriteLine("lexeme = " + lex + " \'" + (char)lex + "\'");
                    nch = GetC();
                }
            }
            
        }

        static void  Word()
        {
            string tx = " ";
            string[] serv = { "begin", "end", "if", "do", "while" };
            int[] cdl = { (int)KeyWords.BEGIN, (int)KeyWords.END, (int)KeyWords.IF, (int)KeyWords.DO, (int)KeyWords.WHILE };


            while (Char.IsLetterOrDigit(nch))
            { 
                tx += nch;
                nch = GetC();
            }
            tx += " ";
            sw.WriteLine(tx);
            Console.WriteLine(tx);

            tx = tx.Trim();
            for (int i = 0; i < cdl.Length; i++)
            {
                if (tx.Equals(serv[i]))
                {
                    lex = cdl[i];
                    sw.WriteLine("KeyWord = " + serv[i]);
                    Console.WriteLine("KeyWord = " + serv[i]);
                    return;
                }
            }
            
            lex = (int)KeyWords.IDEN;
            lval = Add(tx);
            sw.WriteLine(" for ident tx = " + tx + " lval = " + lval);
            Console.WriteLine(" for ident tx = " + tx + " lval = " + lval);
            Console.WriteLine("lexeme = " + lex);
            sw.WriteLine("lexeme = " + lex);
            
        }

        static void Number()
        { 
            lval = 0;

            while (Char.IsDigit(nch))
            {
                Console.WriteLine("nch = " + nch);
                lval = lval * 10 + nch - '0';
                nch = GetC();
            }
            Console.WriteLine("lval = " + lval);
            lex = (int)KeyWords.NUMB;
            Console.WriteLine("lexeme = " + lex);
            sw.WriteLine("lexeme = " + lex);

        }

        public static int Add(string nm)
        { 
            for (int i = 0; i < ptn; i++)
            {
                if (TNM[i] == nm)
                {
                    return i;
                }
            }

            try
            {
                TNM[ptn] = nm;
                return ptn++;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Переповнення таблиці TNM");
            }
            return 1;
        }
    }
}
