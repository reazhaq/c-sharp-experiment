using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

Console.WriteLine("benchmarking various loop performance");

BenchmarkRunner.Run<LoopBenchmarks>();


[MemoryDiagnoser(false)]
public class LoopBenchmarks
{
    private List<int> intItems;

    [Params(10, 100, 1_000, 10_000, 100_000)]
    public int LoopSize { get; set; }

    public void DoSomething(int someRandomNumber)
    {
        while (someRandomNumber > 0)
        {
            someRandomNumber--;
        }
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        intItems = Enumerable.Range(1, LoopSize).ToList();
    }

    [Benchmark]
    public void ForLoop()
    {
        for (var i = 0; i < intItems.Count; i++)
        {
            var item = intItems[i];
            DoSomething(item);
        }
    }

    [Benchmark]
    public void ForEachLoop()
    {
        foreach (var item in intItems)
        {
            DoSomething(item);
        }
    }

    [Benchmark]
    public void ForEachLinqLoop()
    {
        intItems.ForEach(x => { DoSomething(x); });
    }

    [Benchmark]
    public void ParallelForEachLoop()
    {
        Parallel.ForEach(intItems, x => { DoSomething(x); });
    }

    [Benchmark]
    public void ParallelForEachLinqLoop()
    {
        intItems.AsParallel().ForAll(x => { DoSomething(x); });
    }

    [Benchmark]
    public void ForUsingSpanLoop()
    {
        var spanOfItems = CollectionsMarshal.AsSpan(intItems);
        for (var i = 0; i < spanOfItems.Length; i++)
        {
            DoSomething(spanOfItems[i]);
        }
    }

    /// <summary>
    /// this is used in actual runtime code - to get direct access to an item, and use offset
    /// to iterate over rest of the collections
    /// https://github.com/search?q=repo%3Adotnet%2Fruntime+Unsafe.Add&type=code
    /// </summary>
    [Benchmark]
    public void ForUsingCrazySpanLoop()
    {
        var spanOfItems = CollectionsMarshal.AsSpan(intItems);
        ref var refItem = ref MemoryMarshal.GetReference(spanOfItems);
        for (var i = 0; i < spanOfItems.Length; i++)
        {
            var item = Unsafe.Add(ref refItem, i);
            DoSomething(item);
        }
    }
}