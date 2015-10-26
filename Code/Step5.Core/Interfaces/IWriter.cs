namespace Step5.Core.Interfaces
{
  /// <summary>
  /// Writes a message somewhere, we don't care where
  /// </summary>
  public interface IWriter
  {
    void WriteLine(string message);
  }
}