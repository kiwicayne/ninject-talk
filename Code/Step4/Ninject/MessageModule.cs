using System.IO;
using Ninject.Modules;
using Step4.Interfaces;

namespace Step4.Ninject
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

      // Just for fun, InSingletonScope means we will only ever get one instance of this.  
      // No more singleton factories required!
      // Scoping is flexible, you can create your own scope.  
      // I find Asp.Net InRequestScope very useful, particulalry for IDisposable's.
      Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();

      // You don't need to use an interface, can bind a class to itself.  
      // Note that the MessageSender constructor requires an INameRepository and and IWriter, 
      // and these will be automatically injected based on above mappings when a MessageSender is 
      // requested
      Bind<MessageSender>().To<MessageSender>(); 
    }
  }
}