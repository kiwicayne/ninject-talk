using System;
using System.IO;

// ReSharper disable ExceptionNotDocumented
namespace Step3
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      RunExample();

      Exit();
    }

    private static void RunExample()
    {
      var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Data\names.txt");

      var nameRespository = new NameFileRepository(filePath);
      var writer = new ConsoleWriter();

      // Dependencies created above are now being injected into the MessageSender
      // ConsoleMessageSender now called MessageSender
      var emailSender = new MessageSender(nameRespository, writer);
      emailSender.Send("Hello {0}");
    }

    private static void Exit()
    {
      Console.WriteLine();
      Console.WriteLine("goodbye.");
      Console.ReadLine();
    }
  }
}