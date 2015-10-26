namespace Step3.Interfaces
{
  /// <summary>
  /// Writes a message somewhere, we don't care where
  /// </summary>
  public interface IWriter
  {
    void WriteLine(string message);
  }
}