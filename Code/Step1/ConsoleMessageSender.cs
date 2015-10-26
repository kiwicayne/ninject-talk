using System;
using System.IO;

namespace Step1
{
  /// <summary>
  ///   Writes a message to the console for each name in a file
  /// 
  ///   This class has multiple responsibilities.  
  ///   Loading of names from a file and writing messages to a console. 
  /// </summary>
  public class ConsoleMessageSender
  {
    private readonly string _filename;

    /// <summary>
    /// Constructor which loads names from a file
    /// </summary>
    /// <param name="filename"></param>
    /// <exception cref="ArgumentNullException"><paramref name="filename"/> is <see langword="null" />.</exception>
    public ConsoleMessageSender(string filename)
    {      
      if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));

      _filename = filename;
    }

    /// <summary>
    ///   Send a message to each person loaded from the name file
    /// </summary>
    /// <param name="formattedMessage"></param>
    /// <exception cref="InvalidDataException">The data file must contain at least one line</exception>
    /// <exception cref="FileNotFoundException">The file could not be found</exception>
    public void Send(string formattedMessage)
    {
      if (!File.Exists(_filename))
      {
        throw new FileNotFoundException("The file could not be found", _filename);
      }

      var names = File.ReadAllLines(_filename); // Exceptional plugin makes the squiggle
      if (names == null || names.Length < 1) throw new InvalidDataException("The data file must contain at least one line");

      foreach (var name in names)
      {
        Console.WriteLine(formattedMessage, name);
      }
    }
  }
}