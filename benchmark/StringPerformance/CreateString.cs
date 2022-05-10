using BenchmarkDotNet.Attributes;

namespace StringPerformance
{
    [MemoryDiagnoser]
    public class CreateString
    {
        private string[] data = new[]
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "ten"
        };

        [Benchmark]
        public string ConcatString()
        {
            var result = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                result += i.ToString() + data[i] + ",";
            }
            return result;
        }

        [Benchmark]
        public string FormatString()
        {
            return string.Format("{0}{1},{2}{3},{4}{5},{6}{7},{8}{9},{10}{11},{12}{13},{14}{15},{16}{17},{18}{19},",
                0, data[0], 1, data[1], 2, data[2], 3, data[3], 4, data[4], 5, data[5], 6, data[6], 7, data[7], 8, data[8], 9, data[9] );
        }

        [Benchmark]
        public string InterpolatedString()
        {
            return $"0{data[0]},1{data[1]},2{data[2]},3{data[3]},4{data[4]},5{data[5]},6{data[6]},7{data[7]},8{data[8]},9{data[9]},";
        }
    }
}
