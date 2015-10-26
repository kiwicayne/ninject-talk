using System;
// using System.IO; -- This is no longer required

namespace Step2
{
  /// <summary>
  ///   Writes a message to the console for each name in a NameFileRepository
  ///   
  ///   This class is ONLY responsible for writing messages to a console.
  /// </summary>
  public class ConsoleMessageSender
  {
    private readonly NameFileRepository _nameRepository;

    /// <summary>
    ///   Constructor
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="nameRepository"/> is <see langword="null" />.</exception>
    public ConsoleMessageSender(NameFileRepository nameRepository)
    {
      if (nameRepository == null) throw new ArgumentNullException(nameof(nameRepository));

      _nameRepository = nameRepository;
    }

    /// <summary>
    ///   Send a message to each person loaded from the name repository
    /// </summary>
    /// <param name="formattedMessage"></param>
    /// <exception cref="System.IO.FileNotFoundException">The file could not be found</exception>
    /// <exception cref="System.IO.InvalidDataException">The data file must contain at least one line</exception>
    public void Send(string formattedMessage)
    {
      foreach (var name in _nameRepository.Load())
      {
        Console.WriteLine(formattedMessage, name);
      }
    }
  }
}