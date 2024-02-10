using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Frozen;

Console.WriteLine("starting benchmark runner");

BenchmarkRunner.Run<LookupBenchmarkRunner>();

[MemoryDiagnoser(false)]
public class LookupBenchmarkRunner
{
    private int[] intArrays;
    private List<int> justList;
    private FrozenSet<int> frozenList;
    private HashSet<int> hashSet;

    [Params(100, 1_000, 10_000, 100_000)]
    public int IterationCount;

    [GlobalSetup]
    public void GlobalSetup()
    {
        intArrays = Enumerable.Range(0, IterationCount).ToArray();
        justList = Enumerable.Range(0, IterationCount).ToList();
        frozenList = Enumerable.Range(0, IterationCount).ToFrozenSet();
        hashSet = Enumerable.Range(0, IterationCount).ToHashSet();
    }

    [Benchmark(Baseline = true)]
    public void LookupArray()
    {
        for (int i = 0; i < IterationCount; i++)
        {
            _ = intArrays.Contains(i);
        }
    }

    [Benchmark]
    public void LookupList()
    {
        for (int i = 0; i < IterationCount; i++)
            _ = justList.Contains(i);
    }

    [Benchmark]
    public void LookupFrozen()
    {
        for (int i = 0; i < IterationCount; i++)
            _ = frozenList.Contains(i);
    }

    [Benchmark]
    public void LookupHash()
    {
        for (int i = 0; i < IterationCount; i++)
            _ = hashSet.Contains(i);
    }
}
