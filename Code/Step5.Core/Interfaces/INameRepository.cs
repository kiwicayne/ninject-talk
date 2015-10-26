using System.Collections.Generic;

namespace Step5.Core.Interfaces
{
  /// <summary>
  /// A repository of names, we don't care where the names come from
  /// </summary>
  public interface INameRepository
  {
    IEnumerable<string> Load();
  }
}