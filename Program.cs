using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplateForCTasks
{
    public class Program
    {
        private List<String> combinations = new List<String>();
        
        public int getCombinations(int sum, int prevNominal, String combination, int[] nomArr)
        {
            int count = 0;
            if (sum == 0)
            {
                count++;
                Console.WriteLine(combination);
                combinations.Add(combination);
            }

            foreach (var curNominal in nomArr)
            {
                if ((prevNominal >= curNominal) && (sum >= curNominal))
                {
                    count += getCombinations(sum - curNominal, curNominal, combination + " " + curNominal + " ",
                        nomArr);
                }
            }

            return count;
        }

    
        public int getSum()
        {
            String str;
            while (true)
            {
                str = Console.ReadLine();
                try
                {
                    return parseToInteger(str,false);
                }

                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

    
        public int[] parseNominal()
        {
            String ss = "";
            List<int> nomArr = new List<int>();
            while (true)
            {
                try
                {
                    ss = Console.ReadLine();
                    if (ss.Equals("0"))
                    {
                        break;
                    }
                    nomArr.Add(parseToInteger(ss, true));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            nomArr.Sort();
            nomArr.Reverse();
            return nomArr.Distinct().ToArray();
        }
        
        public int parseToInteger(String str, bool isNominal)
        {
            int value;
            try
            {
                value = Int32.Parse(str);
                if (value < 0)
                {
                    throw new ArithmeticException("Ошибка! Значение номинала/суммы < 0");
                }

                if (value == 0 && isNominal)
                {
                    return value;
                }
                
                if (value == 0 && !isNominal)
                {
                    throw new ArithmeticException("Ошибка! Значение суммы == 0");
                }
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Ошибка! Значение номинала/суммы не целое число!");
            }

            return value;
        }

        public List<String> getCombinations()
        {
            List<String> answer = new List<string>(combinations);
            combinations.Clear();
            return answer;
        }


    
        public static void Main(String[] args)
        {
            Program ts = new Program();
            Console.WriteLine("Введите сумму:");
            int sum = ts.getSum();
            Console.WriteLine("Вводите каждую купюру c новой строки. Если хотите закончить ввод, введите 0");
            int[] nominal = ts.parseNominal();
            ts.getCombinations(sum, nominal[0], "", nominal);
        }
    }
}