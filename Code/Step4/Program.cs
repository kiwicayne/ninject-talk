using System;
using Ninject;
using Step4.Interfaces;
using Step4.Ninject;

// ReSharper disable ExceptionNotDocumented

namespace Step4
{
  internal class Program
  {    
    private static void Main(string[] args)
    {
    //  RunExample1();
      RunExample2();

      Exit();
    }

    private static void RunExample1()
    {
      // Configure Ninject  
      IKernel kernel = new StandardKernel(new MessageModule());

      // We could do this, use Ninject to resolve and manually inject dependencies
      var nameRespository = kernel.Get<INameRepository>();
      var writer = kernel.Get<IWriter>();
      var emailSender = new MessageSender(nameRespository, writer);

      emailSender.Send("Hello {0}");
    }

    private static void RunExample2()
    {
      // Configure Ninject  
      IKernel kernel = new StandardKernel(new MessageModule());

      // But this is easier, the dependencies are automatically resolved for us
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