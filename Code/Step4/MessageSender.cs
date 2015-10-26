using System;
using Step4.Interfaces;

namespace Step4
{
  /// <summary>
  ///   Writes a message to an IWriter for each name in an INameRespository.
  ///   
  ///   This class is ONLY responsible for writing messages to a writer (no longer console or file specific).
  /// 
  ///   NOTE: The name was changed from ConsoleMessageSender to just MessageSender to reflect that the dependencies are 
  ///   removed and it no has a dependency on the console.
  /// </summary>
  public class MessageSender
  {
    private readonly INameRepository _nameRepository;
    private readonly IWriter _writer;

    /// <summary>
    ///   Constructor which uses dependency injection.  
    /// </summary>
    /// <exception cref="ArgumentNullException"><paramref name="nameRepository"/> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="writer"/> is <see langword="null" />.</exception>
    public MessageSender(INameRepository nameRepository, IWriter writer)
    {
      if (nameRepository == null) throw new ArgumentNullException(nameof(nameRepository));
      if (writer == null) throw new ArgumentNullException(nameof(writer));

      _nameRepository = nameRepository;
      _writer = writer;
    }

    /// <summary>
    ///   Send a message to each person loaded from the name repository
    /// </summary>
    /// <param name="formattedMessage"></param>    
    public void Send(string formattedMessage)
    {
      var names = _nameRepository.Load();
      if (names == null) return;

      foreach (var name in names)
      {
        _writer.WriteLine(string.Format(formattedMessage, name));
      }
    }
  }
}