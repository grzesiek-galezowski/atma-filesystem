using Bullseye;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace BuildScript;

class Program
{
    const string Configuration = "Release";
    const string SolutionPath = "../AtmaFileSystem.sln";
    const string TestProject = "../AtmaFileSystemSpecification/AtmaFileSystemSpecification.csproj";
    const string BenchmarkProject = "../AtmaFileSystem.Benchmarks/AtmaFileSystem.Benchmarks.csproj";

    static async Task Main(string[] args)
    {
        // Define targets
        Target("clean", () => Run("dotnet", $"clean {SolutionPath} -c {Configuration}"));

        Target("restore", DependsOn("clean"), () =>
            Run("dotnet", $"restore {SolutionPath}"));

        Target("build", DependsOn("restore"), () =>
            Run("dotnet", $"build {SolutionPath} -c {Configuration} --no-restore"));

        Target("test", DependsOn("build"), () =>
            Run("dotnet", $"test {TestProject} -c {Configuration} --no-build"));

        Target("benchmark", DependsOn("build"), () =>
            Run("dotnet", $"run -c {Configuration} --project {BenchmarkProject} --framework net8.0 -- --short --runtimes net8.0 net9.0"));

        Target("default", DependsOn("test"));

        Target("bench-only", DependsOn("build"), () =>
            Run("dotnet", $"run -c {Configuration} --project {BenchmarkProject} --framework net8.0 -- --short --runtimes net8.0 net9.0"));

        await RunTargetsAndExitAsync(args);
    }
}
