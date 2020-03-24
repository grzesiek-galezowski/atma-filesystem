using System;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace AtmaFileSystem.InternalInterfaces
{
  internal class DirectoryPathAlgorithms<TPath> where TPath : IDirectoryPath<TPath>
  {
    public static Maybe<TPath> FindCommonDirectoryPathWith(
      IDirectoryPath<TPath> path1, 
      IDirectoryPath<TPath> path2,
      Func<IDirectoryPath<TPath>, TPath> convToTPath,
      Func<TPath, IDirectoryPath<TPath>> convToIDirectoryPath
    )
    {
      var currentPath = path1.Just();
      while (currentPath.HasValue && !StartsWith(path2, currentPath.Value, convToIDirectoryPath))
      {
        currentPath = currentPath.Value.ParentDirectory().Select(convToIDirectoryPath);
      }

      return currentPath.Select(convToTPath);
    }

    public static bool StartsWith(
      IDirectoryPath<TPath> path, 
      IDirectoryPath<TPath> path2,
      Func<TPath, IDirectoryPath<TPath>> convToIDirectoryPath
    )
    {
      if (!path.ToString().StartsWith(path2.ToString()))
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


  }
}