using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Core.Maybe;

namespace AtmaFileSystem.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AnyDirectoryPathBenchmarks
{
    private const string BasePath = @"TestDirectory";
    private const string ChildPath = @"TestDirectory\SubDirectory";
    private const string DeepPath = @"TestDirectory\SubDirectory\DeepDirectory\VeryDeepDirectory";
    private const string AbsolutePath = @"C:\TestDirectory\SubDirectory";
    
    private AnyDirectoryPath _basePath;
    private AnyDirectoryPath _childPath;
    private AnyDirectoryPath _deepPath;
    private AnyDirectoryPath _absolutePath;

    [GlobalSetup]
    public void Setup()
    {
        _basePath = AnyDirectoryPath.Value(BasePath);
        _childPath = AnyDirectoryPath.Value(ChildPath);
        _deepPath = AnyDirectoryPath.Value(DeepPath);
        _absolutePath = AnyDirectoryPath.Value(AbsolutePath);
    }

    [Benchmark]
    public AnyDirectoryPath CreatePath()
    {
        return AnyDirectoryPath.Value(BasePath);
    }

    [Benchmark]
    public AnyFilePath AddFileName()
    {
        return _basePath + FileName.Value("test.txt");
    }

    [Benchmark]
    public DirectoryName GetDirectoryName()
    {
        return _childPath.DirectoryName();
    }

    [Benchmark]
    public Maybe<AnyDirectoryPath> GetParentDirectory()
    {
        return _deepPath.ParentDirectory();
    }

    [Benchmark]
    public Maybe<DirectoryInfo> GetDirectoryInfo()
    {
        return _deepPath.Info();
    }

    [Benchmark]
    public AnyPath AsAnyPath()
    {
        return _deepPath.AsAnyPath();
    }
}
