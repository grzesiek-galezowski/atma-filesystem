using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Core.Maybe;

namespace AtmaFileSystem.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class RelativeDirectoryPathBenchmarks
{
  private const string BasePath = @"TestDirectory\";
  private const string ChildPath = @"TestDirectory\SubDirectory\";
  private const string DeepPath = @"TestDirectory\SubDirectory\DeepDirectory\VeryDeepDirectory\";

  private RelativeDirectoryPath _basePath;
  private RelativeDirectoryPath _childPath;
  private RelativeDirectoryPath _deepPath;

  [GlobalSetup]
  public void Setup()
  {
    _basePath = RelativeDirectoryPath.Value(BasePath);
    _childPath = RelativeDirectoryPath.Value(ChildPath);
    _deepPath = RelativeDirectoryPath.Value(DeepPath);
  }

  [Benchmark]
  public RelativeDirectoryPath CreatePath()
  {
    return RelativeDirectoryPath.Value(BasePath);
  }

  [Benchmark]
  public DirectoryName GetDirectoryName()
  {
    return _childPath.DirectoryName();
  }

  [Benchmark]
  public Maybe<RelativeDirectoryPath> GetParentDirectory()
  {
    return _deepPath.ParentDirectory();
  }

  [Benchmark]
  public AnyPath AsAnyPath()
  {
    return _deepPath.AsAnyPath();
  }

  [Benchmark]
  public RelativeFilePath AddFileName()
  {
    return _deepPath.AddFileName("test.txt");
  }

  [Benchmark]
  public RelativeDirectoryPath AddDirectoryName()
  {
    return _deepPath.AddDirectoryName("NewDirectory");
  }
}
