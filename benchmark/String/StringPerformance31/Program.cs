using BenchmarkDotNet.Running;

namespace StringPerformance31
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _ = BenchmarkRunner.Run<CreateString>();
        }
    }
}
