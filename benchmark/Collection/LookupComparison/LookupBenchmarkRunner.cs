using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Frozen;
using System.Collections.Immutable;

Console.WriteLine("counting ....");

// start with bunch of collections
var list = new List<int> { 1, 2, 3 };
var readOnlyList = list.AsReadOnly();
var frozenSet = list.ToFrozenSet();
var immutableList = list.ToImmutableList();

Console.WriteLine($"before - list.Count: {list.Count}");
Console.WriteLine($"before - readOnlyList.Count: {readOnlyList.Count}");
Console.WriteLine($"before - frozenSet.Count: {frozenSet.Count}");
Console.WriteLine($"before - immutableList.Count: {immutableList.Count}{Environment.NewLine}");

// let's add one to the list
list.Add(4);

Console.WriteLine($"after - list.Count: {list.Count}");
Console.WriteLine($"after - readOnlyList.Count: {readOnlyList.Count}");
Console.WriteLine($"after - frozenSet.Count: {frozenSet.Count}");
Console.WriteLine($"after - immutableList.Count: {immutableList.Count}{Environment.NewLine}");


Console.WriteLine($"starting benchmark runner{Environment.NewLine}");

Console.WriteLine($"starting Lookup runner{Environment.NewLine}");
BenchmarkRunner.Run<LookupBenchmarkRunner>();

Console.WriteLine($"starting creation runner{Environment.NewLine}");
BenchmarkRunner.Run<CollectionCreationBenchmarkRunner>();

[MemoryDiagnoser(false)]
public class LookupBenchmarkRunner
{
    private int[] intArrays;
    private List<int> justList;
    private FrozenSet<int> frozenList;
    private HashSet<int> hashSet;

    [Params(10, 100, 1_000, 10_000, 100_000)]
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


[MemoryDiagnoser(false)]
public class CollectionCreationBenchmarkRunner
{
    private IEnumerable<int> _items;

    [Params(10, 100, 1_000, 10_000, 100_000)]
    public int IterationCount;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _items = Enumerable.Range(0, IterationCount);
    }

    [Benchmark(Baseline =true)]
    public int[] CreateArray() => _items.ToArray();

    [Benchmark]
    public List<int> CreateList() => _items.ToList();

    [Benchmark]
    public FrozenSet<int> CreateFrozenSet() => _items.ToFrozenSet();

    [Benchmark]
    public HashSet<int> CreateHaset() => _items.ToHashSet();

    [Benchmark]
    public ImmutableHashSet<int> CreateImmutableHashSet() => _items.ToImmutableHashSet();
}
