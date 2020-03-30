using System.IO;
using System.Runtime.CompilerServices;

namespace AtmaFileSystem.Internals
{
  internal static class PathInternal
  {
    /// <summary>
    /// Returns true if the given character is a valid drive letter
    /// </summary>
    internal static bool IsValidDriveChar(char value)
    {
      return value >= 'A' && value <= 'Z' || value >= 'a' && value <= 'z';
    }

    /// <summary>
    /// Returns true if the path specified is relative to the current drive or working directory.
    /// Returns false if the path is fixed to a specific drive or UNC path.  This method does no
    /// validation of the path (URIs will be returned as relative as a result).
    /// </summary>
    /// <remarks>
    /// Handles paths that use the alternate directory separator.  It is a frequent mistake to
    /// assume that rooted paths (Path.IsPathRooted) are not relative.  This isn't the case.
    /// "C:a" is drive relative- meaning that it will be resolved against the current directory
    /// for C: (rooted, but relative). "C:\a" is rooted and not relative (the current directory
    /// will not be used to modify the path).
    /// </remarks>
    internal static bool IsPartiallyQualified(string path)
    {
      if (path.Length < 2)
      {
        // It isn't fixed, it must be relative.  There is no way to specify a fixed
        // path with one character (or less).
        return true;
      }

      if (IsDirectorySeparator(path[0]))
      {
        // There is no valid way to specify a relative path with two initial slashes or
        // \? as ? isn't valid for drive relative paths and \??\ is equivalent to \\?\
        return !(path[1] == '?' || IsDirectorySeparator(path[1]));
      }

      // The only way to specify a fixed path that doesn't begin with two slashes
      // is the drive, colon, slash format- i.e. C:\
      return !(path.Length >= 3
               && path[1] == Path.VolumeSeparatorChar
               && IsDirectorySeparator(path[2])
               // To match old behavior we'll check the drive character for validity as the path is technically
               // not qualified if you don't have a valid drive. "=:\" is the "=" file's default data stream.
               && IsValidDriveChar(path[0]));
    }

    /// <summary>
    /// True if the given character is a directory separator.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsDirectorySeparator(char c)
    {
      return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
    }
  }
}