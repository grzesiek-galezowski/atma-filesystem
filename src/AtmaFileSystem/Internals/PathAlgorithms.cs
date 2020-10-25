using System;
using System.IO;
using AtmaFileSystem.InternalInterfaces;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace AtmaFileSystem.Internals
{
  internal static class PathAlgorithms
  {
    private static readonly StringComparison _culture = StringComparison.InvariantCulture;

    public static Maybe<TPath> FindCommonDirectoryPathWith<TPath>(
      IDirectoryPath<TPath> path1, 
      IDirectoryPath<TPath> path2,
      Func<IDirectoryPath<TPath>, TPath> convToTPath,
      Func<TPath, IDirectoryPath<TPath>> convToIDirectoryPath
    ) where TPath : IDirectoryPath<TPath>
    {
      var currentPath = path1.Just();
      while (currentPath.HasValue && !StartsWith(path2, currentPath.Value, convToIDirectoryPath))
      {
        currentPath = currentPath.Value.ParentDirectory().Select(convToIDirectoryPath);
      }

      return currentPath.Select(convToTPath);
    }

    public static bool StartsWith<TPath>(
      IDirectoryPath<TPath> path,
      IDirectoryPath<TPath> path2,
      Func<TPath, IDirectoryPath<TPath>> convToIDirectoryPath
    ) where TPath : IDirectoryPath<TPath>
    {
      if (!path.ToString().StartsWith(path2.ToString(), _culture))
      {
        return false;
      }

      var currentPath = path.Just();
      while (currentPath.HasValue)
      {
        if (currentPath.Value.Equals(path2))
        {
          return true;
        }

        currentPath = currentPath.Value.ParentDirectory().Select(convToIDirectoryPath);
      }

      return false;
    }
    
    public static bool StartsWith(
      RelativeFilePath path,
      RelativeDirectoryPath path2
    )
    {
      if (!path.ToString().StartsWith(path2.ToString(), _culture))
      {
        return false;
      }

      var currentPath = path.ParentDirectory();
      while (currentPath.HasValue)
      {
        if (currentPath.Value.Equals(path2))
        {
          return true;
        }

        currentPath = currentPath.Value.ParentDirectory();
      }

      return false;
    }
    
    //bug some duplication with the methods above
    public static bool StartsWith(
      AbsoluteFilePath path,
      AbsoluteDirectoryPath path2
    )
    {
      if (!path.ToString().StartsWith(path2.ToString(), _culture))
      {
        return false;
      }

      var currentPath = path.ParentDirectory().Just();
      while (currentPath.HasValue)
      {
        if (currentPath.Value.Equals(path2))
        {
          return true;
        }

        currentPath = currentPath.Value.ParentDirectory();
      }

      return false;
    }
    
    //bug invariant culture everywhere
    public static Maybe<string> TrimStart(string originalPathString, string startPathString)
    {
      if (originalPathString.Equals(startPathString, _culture))
      {
        return Maybe<string>.Nothing;
      }

      if (originalPathString.StartsWith(startPathString, _culture))
      {
        return originalPathString.Substring(
          startPathString.Length,
          originalPathString.Length - startPathString.Length).Just();
      }
      else
      {
        return Maybe<string>.Nothing;
      }

    }

    public static string NormalizeSeparators(string relativePath)
    {
      return relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
    }

  }
}