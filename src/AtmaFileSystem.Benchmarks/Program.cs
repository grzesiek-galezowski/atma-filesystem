using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using System;
using System.Linq;
using System.IO;

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
        var config = args.Contains("--short")
            ? BenchmarkConfigs.ShortConfig 
            : DefaultConfig.Instance;

        // Ensure artifacts are saved in a consistent location relative to the solution root
        var artifactPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "BenchmarkDotNet.Artifacts");
        config = config.WithArtifactsPath(artifactPath);

        BenchmarkRunner.Run<AbsoluteDirectoryPathBenchmarks>(config);
        BenchmarkRunner.Run<AbsoluteFilePathBenchmarks>(config);
        BenchmarkRunner.Run<AnyDirectoryPathBenchmarks>(config);
    }
}
