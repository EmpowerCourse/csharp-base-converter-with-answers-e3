using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Xml;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {

            // note: capitaliztion matters (ie 'B' will not be interpreted as the same value as 'b').
            // In Hexadecimal (base 16), 'A' represents 10, 'B' represents 11 ... 'F' represents 15
            Console.WriteLine(ConvertFromBaseXToBase10("101", 2));
            Console.WriteLine(ConvertFromBase10ToBaseX(5, 2));

            Console.WriteLine(ConvertFromBaseXToBase10("FFF", 16));
            Console.WriteLine(ConvertFromBase10ToBaseX(4095, 16));

            Console.WriteLine(ConvertFromBaseXToBase10("AAF1000B98", 16));
            Console.WriteLine(ConvertFromBase10ToBaseX(734187752344, 16));


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(); // Wait for the user to press any key before exiting
        }

        // unsigned (ie no negative numbers)
        private static long ConvertFromBaseXToBase10(string toConvert, int baseX)
        {
            long result = 0;
            int index = 0;
            for(int i = toConvert.Length - 1; i >= 0; i--)
            {
                // check to make sure an invalid # was not entered (ie if base 2, highest number is 1. if base 16, highest number is 15 (represented by F))
                if (GetValue(toConvert[i]) >= baseX)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uh oh! You passed in an invalid value for toConvert");
                    Console.ResetColor();

                    return -1;
                }

                result += (long) Math.Pow(baseX, index) * GetValue(toConvert[i]); // remember formula is the summation of (base^^index * value)
                index++;
            }
            return result;
        }

        // unsigned (ie no negative numbers)
        private static string ConvertFromBase10ToBaseX(long toConvert, int baseX)
        {
            string result = "";

            while (toConvert > 0)
            {
                // toConvert % baseX calculates the remainder when divided by base (gives you the rightmost digit)
                result = GetChar(toConvert % baseX) + result;
                toConvert = toConvert / baseX;  // can also be written as: toConvert /= baseX; This effectively removes the rightmost digit
            }

            return result;
        }


        /*
         * GetValue converts characters representing digits (0-9) and hexadecimal digits (A-F) to its numerical value.
         * You can use this function blindly to return the number value of a character
         * ie if you pass in '2', you will get back 2. If you pass in 'B' you will get 11
         */
        private static int GetValue(char currentCharacter)
        {
            // This uses ASCII value ('0'-'9' is values 48-57) 
            // '0' is 48. So if you pass in '2' (50 ASCII) and subtract '0' (48 ASCII) you get 2
            // important: (int) currentCharacter converts the character to its ASCII value 
            if (currentCharacter >= '0' && currentCharacter <= '9')
            {
                return (int)currentCharacter - '0';
            }
            return (int)currentCharacter - 'A' + 10;    // adding 10 here because the first digit ('A') will represent 10 (ie A == 10)


            // sidenote: in C# there is type string and type char (char is denoted by single quotes while string is denoted by double quotes)
        }

        /*
         * Used to convert a number value to its corresponding character representation
         * Ie 2 will be converted to '2', 10 will be converted to 'A', 15 will be converted to 'F'
         * You can use this function blindly to return the character representation of a number
         */
        private static char GetChar(long currentNumber)
        {
            if (currentNumber >= 0 && currentNumber <= 9)
            {
                return (char)(currentNumber + 48);      // char converts the number to its ASCII value; why add 48? because '0' is represented by 48 in ASCII
            }
            return (char)(currentNumber - 10 + 65);     // why add 65? 'A' is represented by 65 and that is the first digit. why subtract 10? The value in our heads of what 'A' represents is 10
        }




        // Question: what would happen in you use ints instead of longs and try to do:
        // Console.WriteLine(ConvertFromBaseXToBase10("AAF1000B98", 16));
        // What happens and why?


    }
}