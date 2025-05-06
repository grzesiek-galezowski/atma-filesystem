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

  private readonly AbsoluteFilePath _basePath = AbsoluteFilePath.Value(BasePath);
  private readonly AbsoluteFilePath _childPath = AbsoluteFilePath.Value(ChildPath);
  private readonly AbsoluteFilePath _deepPath = AbsoluteFilePath.Value(DeepPath);

  [GlobalSetup]
  public void Setup()
  {
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
