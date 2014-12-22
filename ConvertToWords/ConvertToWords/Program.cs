using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            double amount = 15246.751;

            double thousands = Math.Floor(amount / 1000);
            string amountInWords = string.Empty;
            if (thousands > 0)
            {
                amountInWords += ConvertToWords(thousands) + " thousand ";
            }

            double hundreds = Math.Floor(amount - thousands * 1000);
            if (hundreds > 0)
            {
                amountInWords += ConvertToWords(hundreds);
            }

            double cents = (amount - Math.Floor(amount)) * 100;
            if (cents > 0)
            {
                amountInWords += " and " + ConvertToWords(cents) + " cents";
            }

            Console.WriteLine(amountInWords);
            Console.ReadLine();
        }

        private static string ConvertToWords(double amount)
        {
            string[] onesInWord = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] teensInWord = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensInWord = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            //hundreds
            int hundreds = (int)Math.Floor(amount / 100.00);
            int tens = (int)Math.Floor((amount - hundreds * 100) / 10.00);
            int ones = (int)Math.Floor((amount - hundreds * 100 - tens * 10));

            string words = "";
            //tens
            if (hundreds > 0)
            {
                words = onesInWord[hundreds] + " hundred ";
            }

            if (tens == 1)
            {
                words = words + teensInWord[ones];
            }
            else if (tens > 1)
            {
                words = words + tensInWord[tens] + " ";
                if (ones > 0)
                {
                    words = words + onesInWord[ones];
                }
            }
            else if (ones > 0)
            {
                words = words + onesInWord[ones];
            }
            return words;
        }
    }
}
