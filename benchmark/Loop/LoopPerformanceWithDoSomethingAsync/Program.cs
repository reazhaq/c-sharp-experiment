using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Console.WriteLine("benchmarking various loop performance");

BenchmarkRunner.Run<LoopBenchmarks>();


[MemoryDiagnoser(false)]
public class LoopBenchmarks
{
    private List<int> intItems;

    [Params(10, 100, 1_000)]
    public int LoopSize { get; set; }

    private Random random = new Random(100);

    public Task DoSomethingAsync(int _)
    {
        var someRandomNumber = random.Next(1, 25);
        return Task.Delay(someRandomNumber);
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        intItems = Enumerable.Range(1, LoopSize).ToList();
    }

    [Benchmark]
    public async Task ForLoopAsync()
    {
        for (var i = 0; i < intItems.Count; i++)
        {
            var item = intItems[i];
            await DoSomethingAsync(item).ConfigureAwait(false);
        }
    }

    [Benchmark]
    public async Task ForLoopAwaitAllAsync()
    {
        var tasks = new List<Task>(intItems.Count);
        for (var i = 0; i < intItems.Count; i++)
        {
            var item = intItems[i];
            tasks.Add(DoSomethingAsync(item));
        }
        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    [Benchmark]
    public async Task ForEachLoopAsync()
    {
        foreach (var item in intItems)
        {
            await DoSomethingAsync(item).ConfigureAwait(false);
        }
    }

    [Benchmark]
    public async Task ForEachLoopAwaitAllAsync()
    {
        var tasks = new List<Task>(intItems.Count);
        foreach (var item in intItems)
        {
            tasks.Add(DoSomethingAsync(item));
        }
        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    [Benchmark]
    public void ForEachLinqLoopAsync()
    {
        intItems.ForEach(async x => { await DoSomethingAsync(x).ConfigureAwait(false); });
    }

    [Benchmark]
    public async Task ForEachLinqLoopAwaitAllAsync()
    {
        var tasks = new List<Task>(intItems.Count);
        intItems.ForEach(x => { tasks.Add(DoSomethingAsync(x)); });
        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    [Benchmark]
    public void ParallelForEachLoopAsync()
    {
        Parallel.ForEach(intItems, async x => { await DoSomethingAsync(x).ConfigureAwait(false); });
    }

    [Benchmark]
    public async Task ParallelForEachLoopAwaitAllAsync()
    {
        var tasks = new List<Task>(intItems.Count);
        Parallel.ForEach(intItems, x => { tasks.Add(DoSomethingAsync(x)); });
        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    [Benchmark]
    public void AsParallelLinqLoopAsync()
    {
        intItems.AsParallel().ForAll(async x => { await DoSomethingAsync(x).ConfigureAwait(false); });
    }

    [Benchmark]
    public async Task AsParallelLinqLoopAwaitAllAsync()
    {
        var tasks = new List<Task>(intItems.Count);
        intItems.AsParallel().ForAll(x => { tasks.Add(DoSomethingAsync(x)); });
        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    //////[Benchmark]
    //////public async Task ForUsingSpanLoopAsync()
    //////{
    //////    foreach (var item in GetItems())
    //////    {
    //////        await DoSomethingAsync(item).ConfigureAwait(false);
    //////    }
    //////}

    //////private IEnumerable<int> GetItems()
    //////{
    //////    var spanOfItems = CollectionsMarshal.AsSpan(intItems);
    //////    for (var i = 0; i < spanOfItems.Length; i++)
    //////    {
    //////        yield return spanOfItems[i];
    //////    }
    //////}

    //////[Benchmark]
    //////public async Task ForUsingSpanLoopAwaitAllAsync()
    //////{
    //////    var tasks = new List<Task>(intItems.Count);
    //////    foreach (var item in GetItems())
    //////    {
    //////        tasks.Add(DoSomethingAsync(item));
    //////    }
    //////    await Task.WhenAll(tasks).ConfigureAwait(false);
    //////}

    ////[Benchmark]
    ////public async Task ForUsingSpanLoopAwaitAllAsync()
    ////{
    ////    var spanOfItems = CollectionsMarshal.AsSpan(intItems);
    ////    for (var i = 0; i < spanOfItems.Length; i++)
    ////    {
    ////        DoSomethingAsync(spanOfItems[i]);
    ////    }
    ////}

    /////// <summary>
    /////// this is used in actual runtime code - to get direct access to an item, and use offset
    /////// to iterate over rest of the collections
    /////// https://github.com/search?q=repo%3Adotnet%2Fruntime+Unsafe.Add&type=code
    /////// </summary>
    ////[Benchmark]
    ////public void ForUsingCrazySpanLoop()
    ////{
    ////    var spanOfItems = CollectionsMarshal.AsSpan(intItems);
    ////    ref var refItem = ref MemoryMarshal.GetReference(spanOfItems);
    ////    for (var i = 0; i < spanOfItems.Length; i++)
    ////    {
    ////        var item = Unsafe.Add(ref refItem, i);
    ////        DoSomethingAsync(item);
    ////    }
    ////}
}