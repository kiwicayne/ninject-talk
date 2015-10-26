using System;
using Step5.Core.Interfaces;

namespace Step5.Infrastructure
{
  internal class ConsoleWriter : IWriter
  {
    public void WriteLine(string message)
    {
      Console.WriteLine(message);
    }
  }
}