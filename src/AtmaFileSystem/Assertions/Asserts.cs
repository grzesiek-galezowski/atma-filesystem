using System;
using System.IO;
using System.Linq;

namespace AtmaFileSystem.Assertions;

internal static class Asserts
{
    public static void FullyQualified(string path)
    {
        if (!Path.IsPathFullyQualified(path))
        {
            throw new ArgumentException("Expected an absolute path, but got " + path);
        }
    }

    public static void NotFullyQualified(string path)
    {
        if (Path.IsPathFullyQualified(path))
        {
            throw new ArgumentException(PathFragment(path, "is fully qualified") + ExceptionMessages.RootedPathsAreIllegalPleasePassARelativePath);
        }
    }

    public static void NotEmpty(string path, string message)
    {
        if (path == string.Empty)
        {
            throw new ArgumentException(PathFragment(path, "is empty") + message, nameof(path));
        }
    }

    public static void NotNull(string path, string paramName)
    {
        if (path == null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static void ValidDirectoryName(string value, string message)
    {
        if (value != string.Empty)
        {
            var directoryName = new DirectoryInfo(value).Name;
            if (directoryName != value)
            {
                throw new ArgumentException(
                  ValueFragment(value, "is not a valid directory name") + message);
            }
        }
    }

    public static void ConsistsSolelyOfExtension(string extensionString, string message)
    {
        var extractedExtension = Path.GetExtension(extensionString);
        if (extractedExtension != extensionString)
        {
            throw new ArgumentException(message + ". Expected extension: " + extractedExtension);
        }
    }

    public static void ConsistsSolelyOfFileName(string path)
    {
        if (Path.GetFileName(path) != path)
        {
            throw new ArgumentException("Expected file name, but got " + path);
        }
    }

    public static void DirectoryPathValid(string path)
    {
        try
        {
            Path.IsPathRooted(path);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(PathFragment(path, "is not a valid directory path"), e);
        }
    }

    public static void DoesNotContainInvalidChars(string path)
    {
        var invalidCharIndex = path.IndexOfAny(Path.GetInvalidPathChars().ToArray());
        if (invalidCharIndex != -1)
        {
            throw new ArgumentException(
                $"Path {path} contains invalid char {path[invalidCharIndex]} at index {invalidCharIndex}");
        }
    }

    // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
    public static void NotAllWhitespace(string path, string message)
    {
        if (path != string.Empty && path.All(char.IsWhiteSpace))
        {
            throw new ArgumentException(ValueFragment(path, "consists only of whitespace") + message);
        }
    }

    private static string PathFragment(string path, string constaintBreak)
    {
      return $"Path {path} {constaintBreak}. ";
    }

    private static string ValueFragment(string value, string constaintBreak)
    {
      return $"Value {value} {constaintBreak}. ";
    }
}