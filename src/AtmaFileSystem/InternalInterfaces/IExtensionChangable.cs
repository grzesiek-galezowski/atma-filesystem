namespace AtmaFileSystem.InternalInterfaces
{
  internal interface IExtensionChangable<T> where T : IExtensionChangable<T>
  {
    T ChangeExtensionTo(FileExtension value);
  }
}