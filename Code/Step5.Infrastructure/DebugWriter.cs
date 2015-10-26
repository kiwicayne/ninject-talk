using System.Diagnostics;
using Step5.Core.Interfaces;

namespace Step5.Infrastructure
{
  internal class DebugWriter : IWriter
  {
    public void WriteLine(string message)
    {
      Debug.WriteLine(message);
    }
  }
}