using System;
using Ninject;
using Step5.Bootstrapper.Ninject;
using Step5.Core;

// ReSharper disable ExceptionNotDocumented
namespace Step5
{
  internal class Program
  {   
    private static void Main(string[] args)
    {    
      //RunExample1();
      RunExample2();

      Exit();
    }

    /// <summary>
    /// This is the same as the previous steps Example 2.
    /// </summary>
    private static void RunExample1()
    {
      IKernel kernel = new StandardKernel(new MessageModule());

      var emailSender = kernel.Get<MessageSender>();
      emailSender.Send("Hello {0}");      
    }

    /// <summary>
    /// Here we introduce a Notifier, which uses a different implementation of the IWriter
    /// than the MessageSender.  All plumbing for wiring this up is in the Bootstrapper keeping
    /// this code very simple.
    /// 
    /// How can we make the notifier use the ConsoleWriter as well?
    /// </summary>
    private static void RunExample2()
    {
      IKernel kernel = new StandardKernel(new MessageModule(), new BonusModule());

      var notifier = kernel.Get<Notifier>();
      notifier.SendNotification(">>>>   You have been notified!!!!!!   <<<<");

      var emailSender = kernel.Get<MessageSender>();
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