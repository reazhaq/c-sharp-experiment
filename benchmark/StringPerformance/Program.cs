using BenchmarkDotNet.Running;

namespace StringPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            //var createString = new CreateString();
            //System.Console.WriteLine($"concat: {createString.ConcatString()}");
            //System.Console.WriteLine($"format: {createString.FormatString()}");
            //System.Console.WriteLine($"inter: {createString.InterpolatedString()}");
            var _ = BenchmarkRunner.Run<CreateString>();
        }
    }
}
