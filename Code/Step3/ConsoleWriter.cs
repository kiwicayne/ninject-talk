using System;
using Step3.Interfaces;

namespace Step3
{
  public class ConsoleWriter : IWriter
  {
    public void WriteLine(string message)
    {
      Console.WriteLine(message);
    }
  }
}