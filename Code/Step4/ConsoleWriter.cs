using System;
using Step4.Interfaces;

namespace Step4
{
  public class ConsoleWriter : IWriter
  {
    public void WriteLine(string message)
    {
      Console.WriteLine(message);
    }
  }
}