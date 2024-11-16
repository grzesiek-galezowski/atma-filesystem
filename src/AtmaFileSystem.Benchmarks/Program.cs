using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using System;

namespace AtmaFileSystem.Benchmarks;

public class BenchmarkConfigs
{
    public static IConfig ShortConfig => ManualConfig.Create(DefaultConfig.Instance)
        .WithOptions(ConfigOptions.DisableOptimizationsValidator)
        .AddJob(Job.ShortRun
            .WithToolchain(InProcessNoEmitToolchain.Instance)
            .WithWarmupCount(1)
            .WithIterationCount(3)
            .WithUnrollFactor(1)  // Set unroll factor to 1 to avoid invocation count restrictions
            .WithInvocationCount(8));
}

public class Program
{
    public static void Main(string[] args)
    {
        var config = args.Length > 0 && args[0] == "--short" 
            ? BenchmarkConfigs.ShortConfig 
            : DefaultConfig.Instance;

        BenchmarkRunner.Run<AbsoluteDirectoryPathBenchmarks>(config);
        BenchmarkRunner.Run<AbsoluteFilePathBenchmarks>(config);
        BenchmarkRunner.Run<AnyDirectoryPathBenchmarks>(config);
    }
}
