using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Core.Maybe;

namespace AtmaFileSystem.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AbsoluteFilePathBenchmarks
{
    private const string BasePath = @"C:\TestDirectory\test.txt";
    private const string ChildPath = @"C:\TestDirectory\SubDirectory\test.txt";
    private const string DeepPath = @"C:\TestDirectory\SubDirectory\DeepDirectory\VeryDeepDirectory\test.txt";
    
    private AbsoluteFilePath _basePath;
    private AbsoluteFilePath _childPath;
    private AbsoluteFilePath _deepPath;

    [GlobalSetup]
    public void Setup()
    {
        _basePath = AbsoluteFilePath.Value(BasePath);
        _childPath = AbsoluteFilePath.Value(ChildPath);
        _deepPath = AbsoluteFilePath.Value(DeepPath);
    }

    [Benchmark]
    public AbsoluteFilePath CreatePath()
    {
        return AbsoluteFilePath.Value(BasePath);
    }

    [Benchmark]
    public string GetFileName()
    {
        return _childPath.FileName().ToString();
    }

    [Benchmark]
    public AbsoluteDirectoryPath GetParentDirectory()
    {
        return _deepPath.ParentDirectory();
    }

    [Benchmark]
    public Maybe<AbsoluteDirectoryPath> GetParentDirectoryAtLevel()
    {
        return _deepPath.ParentDirectory(2);
    }

    [Benchmark]
    public FileInfo GetFileInfo()
    {
        return _deepPath.Info();
    }

    [Benchmark]
    public AnyPath AsAnyPath()
    {
        return _deepPath.AsAnyPath();
    }
}
