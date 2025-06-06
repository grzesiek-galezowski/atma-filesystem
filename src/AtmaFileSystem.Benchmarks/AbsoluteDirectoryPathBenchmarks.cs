using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Core.Maybe;

namespace AtmaFileSystem.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AbsoluteDirectoryPathBenchmarks
{
  private const string BasePath = @"C:\TestDirectory";
  private const string ChildPath = @"C:\TestDirectory\SubDirectory";
  private const string DeepPath = @"C:\TestDirectory\SubDirectory\DeepDirectory\VeryDeepDirectory";

  private readonly AbsoluteDirectoryPath _basePath = AbsoluteDirectoryPath.Value(BasePath);
  private readonly AbsoluteDirectoryPath _childPath = AbsoluteDirectoryPath.Value(ChildPath);
  private readonly AbsoluteDirectoryPath _deepPath = AbsoluteDirectoryPath.Value(DeepPath);

  [GlobalSetup]
  public void Setup()
  {
  }

  [Benchmark]
  public AbsoluteDirectoryPath CreatePath()
  {
    return AbsoluteDirectoryPath.Value(BasePath);
  }

  [Benchmark]
  public AbsoluteDirectoryPath AddDirectoryName()
  {
    return _basePath.AddDirectoryName("NewDirectory");
  }

  [Benchmark]
  public AbsoluteFilePath AddFileName()
  {
    return _basePath.AddFileName("test.txt");
  }

  [Benchmark]
  public string GetDirectoryName()
  {
    return _childPath.DirectoryName().ToString();
  }

  [Benchmark]
  public AbsoluteDirectoryPath GetRoot()
  {
    return _deepPath.Root();
  }

  [Benchmark]
  public Maybe<AbsoluteDirectoryPath> GetParentDirectory()
  {
    return _deepPath.ParentDirectory();
  }

  [Benchmark]
  public Maybe<AbsoluteDirectoryPath> GetParentDirectoryAtLevel()
  {
    return _deepPath.ParentDirectory(2);
  }

  [Benchmark]
  public DirectoryInfo GetDirectoryInfo()
  {
    return _deepPath.Info();
  }

  [Benchmark]
  public AnyPath ConvertToAnyPath()
  {
    return _deepPath.AsAnyPath();
  }
}
