using static Bullseye.Targets;
using static SimpleExec.Command;
using AtmaFileSystem;
using AtmaFileSystem.IO;

namespace BuildScript;

class Program
{
  const string Configuration = "Release";

  static readonly AbsoluteFilePath SolutionPath =
    AbsoluteFilePath.OfThisFile().ParentDirectory().ParentDirectory().Value().AddFileName("AtmaFileSystem.sln");

  static readonly AbsoluteFilePath BenchmarkProject = SolutionPath.ParentDirectory()
    .AddDirectoryName("AtmaFileSystem.Benchmarks").AddFileName("AtmaFileSystem.Benchmarks.csproj");

  static async Task Main(string[] args)
  {
    // Split args into targets and options
    var targets = new List<string>();
    var options = new List<string>();

    for (int i = 0; i < args.Length; i++)
    {
      if (args[i].StartsWith("--"))
      {
        options.Add(args[i]);
        if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
        {
          options.Add(args[i + 1]);
          i++; // Skip the next argument as it's a value
        }
      }
      else
      {
        targets.Add(args[i]);
      }
    }

    var benchmarkArgs = GetBenchmarkArgs(options.ToArray());

    // Define targets
    Target("clean", () => Run("dotnet", $"clean {SolutionPath} -c {Configuration}"));

    Target("restore", dependsOn: ["clean"], () =>
      Run("dotnet", $"restore {SolutionPath}"));

    Target("build", dependsOn: ["restore"], () =>
      Run("dotnet", $"build {SolutionPath} -c {Configuration} --no-restore"));

    Target("test", dependsOn: ["build"], () =>
      Run("dotnet", $"test {SolutionPath} -c {Configuration} --no-build"));

    Target("default", dependsOn: ["test"]);

    Target("bench-only", dependsOn: ["build"], async () =>
    {
      BenchmarkProject.ParentDirectory().SetAsCurrentDirectory();
      await RunAsync("dotnet", $"run {BenchmarkProject} -c {Configuration} --no-build --framework net8.0 -- --short --runtimes net8.0 net9.0 {benchmarkArgs}");
    });

    await RunTargetsAndExitAsync(targets.ToArray());
  }

  private static string GetBenchmarkArgs(string[] args)
  {
    return string.Join(" ", args);
  }
}