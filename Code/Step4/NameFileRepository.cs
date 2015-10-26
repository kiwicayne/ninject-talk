using System;
using System.Collections.Generic;
using System.IO;
using Step4.Interfaces;

namespace Step4
{
  /// <summary>
  ///   This class is ONLY responsible for loading names from a file
  /// </summary>
  public class NameFileRepository: INameRepository
  {
    private readonly string _filename;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="filename"></param>
    /// <exception cref="ArgumentNullException"><paramref name="filename"/> is <see langword="null" />.</exception>
    public NameFileRepository(string filename)
    {
      if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));

      _filename = filename;
    }

    /// <exception cref="FileNotFoundException">The file could not be found</exception>
    /// <exception cref="InvalidDataException">The data file must contain at least one line</exception>
    public IEnumerable<string> Load()
    {      
      if (!File.Exists(_filename))
      {
        throw new FileNotFoundException("The file could not be found", _filename);
      }

      var names = File.ReadAllLines(_filename);
      if (names == null || names.Length < 1) throw new InvalidDataException("The data file must contain at least one line");

      return names;
    }
  }
}