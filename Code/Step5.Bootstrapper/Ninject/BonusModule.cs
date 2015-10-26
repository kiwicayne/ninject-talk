using Ninject.Modules;
using Step5.Core;
using Step5.Core.Interfaces;
using Step5.Infrastructure;

namespace Step5.Bootstrapper.Ninject
{
  /// <summary>
  ///   Wire up dependencies to show off some bonus features using Ninject
  /// </summary>
  internal class BonusModule : NinjectModule
  {
    public override void Load()
    {
      // There is new Notifier class which also takes an IWriter, but we want to use a DebugWriter for this not ConsoleWriter
      // Note there are lots of overloads of When...
      // Comment this out for the notifier to use the ConsoleWriter
      //Bind<IWriter>().To<DebugWriter>().WhenInjectedInto<Notifier>();

      Bind<Notifier>().To<Notifier>();
    }
  }
}