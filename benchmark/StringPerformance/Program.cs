using BenchmarkDotNet.Running;

namespace StringPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            //var createString = new CreateString();
            //Console.WriteLine($"concat: {createString.ConcatString()}");
            //Console.WriteLine($"format: {createString.FormatString()}");
            //Console.WriteLine($"inter: {createString.InterpolatedString()}");
            var _ = BenchmarkRunner.Run<CreateString>();
        }
    }
}
