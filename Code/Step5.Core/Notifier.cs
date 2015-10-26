using System;
using Step5.Core.Interfaces;

namespace Step5.Core
{
  public class Notifier
  {
    private readonly IWriter _writer;

    /// <exception cref="ArgumentNullException"><paramref name="writer" /> is <see langword="null" />.</exception>
    public Notifier(IWriter writer)
    {
      if (writer == null) throw new ArgumentNullException(nameof(writer));
      _writer = writer;
    }

    public void SendNotification(string msg)
    {
      // Doesn't matter if this is console or debug, we have no idea
      _writer.WriteLine(msg);
    }
  }
}