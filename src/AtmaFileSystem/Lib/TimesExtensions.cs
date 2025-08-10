using System;

namespace AtmaFileSystem.Lib;

internal static class TimesExtensions
{
  public static void Times(this int times, Action<int> action)
  {
    for (var i = 0; i < times; ++i)
    {
      action(i);
    }
  }

  public static void Times(this int times, Action action)
  {
    for (var i = 0; i < times; ++i)
    {
      action();
    }
  }

  public static void Times(this uint times, Action<int> action)
  {
    for (var i = 0; i < times; ++i)
    {
      action(i);
    }
  }

  public static void Times(this uint times, Action action)
  {
    for (var i = 0; i < times; ++i)
    {
      action();
    }
  }
}