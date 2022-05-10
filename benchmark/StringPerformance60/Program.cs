using BenchmarkDotNet.Running;
using StringPerformance60;

var _ = BenchmarkRunner.Run<CreateString>();