using System.IO;
using Ninject.Modules;
using Step5.Core;
using Step5.Core.Interfaces;
using Step5.Infrastructure;

namespace Step5.Bootstrapper.Ninject
{
  /// <summary>
  /// Wire up dependencies using Ninject
  /// </summary>
  internal class MessageModule : NinjectModule
  {
    public override void Load()
    {
      var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Data\names.txt");

      // Example showing a constructor argument
      Bind<INameRepository>().To<NameFileRepository>().WithConstructorArgument(filePath);
     
      Bind<IWriter>().To<ConsoleWriter>();
      
      // You don't need to use an interface, can bind a class to itself.  
      // Note that the MessageSender constructor requires an INameRepository and and IWriter, 
      // and these will be automatically injected based on above mappings when a MessageSender is 
      // requested      
      Bind<MessageSender>().To<MessageSender>(); 
    }
  }
}