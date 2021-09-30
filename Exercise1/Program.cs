using System;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Programming in C # - Exercise 1";

            Console.WriteLine("System: Enter your full name: ");

            string fullName = Console.ReadLine();
            
            /*
             * Never assume a value for name has been entered
             * https://docs.microsoft.com/en-us/dotnet/api/system.string.isnullorwhitespace?view=net-5.0
             */
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                /*
                 * Note original code concatenated name variable with text, you can also use string interpolation
                 * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
                 */
                Console.WriteLine($"System: Welcome {fullName}!");
            }
            else
            {
                Console.WriteLine("System: Welcome no name given!");
            }


            Console.WriteLine("System: How old are you?");
            string ageInput = Console.ReadLine();

            /*
             * int.TryParse, better than Convert.ToInt32 which is recommended in the remark section
             * for documentation for Convert.ToInt32.
             *
             * https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-5.0
             *
             */
            if (int.TryParse(ageInput, out var age))
            {
                int ageInDays = age * 365;
                /*
                 * Note original code concatenated name variable with text, you can also use string interpolation
                 * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
                 */
                Console.WriteLine($"System: {age}?! That means you are {ageInDays} days old!");
            }
            else
            {
                Console.WriteLine("No age provided or the value was not a valid integer");
            }

            Console.WriteLine("Press any key to close this program");
            Console.ReadLine();

        }
    }
}
