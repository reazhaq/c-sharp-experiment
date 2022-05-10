using System;
using System.Globalization;

namespace StringPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            var createString = new CreateString();
            Console.WriteLine($"concat: {createString.ConcatString()}");
            Console.WriteLine($"format: {createString.FormatString()}");
            Console.WriteLine($"inter: {createString.InterpolatedString()}");

            var todayIs = DateTime.Now;
            var usS = string.Create(new CultureInfo("en-US"), $"en-US: today is: {todayIs}");
            Console.WriteLine(usS);
            var frS = string.Create(new CultureInfo("fr-FR"), $"fr-FR: today is: {todayIs}");
            Console.WriteLine(frS);
        }
    }
}
