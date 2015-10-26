using System;
using System.IO;

// ReSharper disable ExceptionNotDocumented
namespace Step1
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

      var emailSender = new ConsoleMessageSender(filePath);
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