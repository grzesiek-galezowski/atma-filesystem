using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Core.Maybe;

namespace AtmaFileSystem.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class RelativeFilePathBenchmarks
{
  private const string BasePath = @"TestDirectory\test.txt";
  private const string ChildPath = @"TestDirectory\SubDirectory\test.txt";
  private const string DeepPath = @"TestDirectory\SubDirectory\DeepDirectory\VeryDeepDirectory\test.txt";

  private RelativeFilePath _childPath;
  private RelativeFilePath _deepPath;

  [GlobalSetup]
  public void Setup()
  {
    _childPath = RelativeFilePath.Value(ChildPath);
    _deepPath = RelativeFilePath.Value(DeepPath);
  }

  [Benchmark]
  public RelativeFilePath CreatePath()
  {
    return RelativeFilePath.Value(BasePath);
  }

  [Benchmark]
  public FileName GetFileName()
  {
    return _childPath.FileName();
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
  public bool HasExtension()
  {
    return _deepPath.Has(FileExtension.Value(".txt"));
  }

  [Benchmark]
  public FileInfo GetFileInfo()
  {
    return _deepPath.Info();
  }
}
