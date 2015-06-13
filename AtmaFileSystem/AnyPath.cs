namespace AtmaFileSystem
{
  public class AnyPath //bug behave like value
  {
    private readonly string _path;

    internal AnyPath(string path)
    {
      _path = path;
    }


    public override string ToString()
    {
      return _path;
    }
  }
}