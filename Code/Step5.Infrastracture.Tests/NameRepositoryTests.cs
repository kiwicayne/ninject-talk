using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Step5.Infrastructure.Tests
{
  /// <summary>
  ///   Tests for the <code>NameRepository</code>.
  /// 
  ///   Note: No more duplication with integration tests in ConsoleMessageSenderTests.
  /// </summary>
  [TestFixture]
  public class NameRepositoryTests
  {
    private const string EmptyFile = @"Data\empty.txt";
    private const string ValidFile = @"Data\names.txt";

    #region Constructor

    [Test]
    [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "filename", MatchType = MessageMatch.Contains)]
    public void Constructor_Should_ThrowException_When_FilenameIsNull()
    {
      new NameFileRepository(null);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "filename", MatchType = MessageMatch.Contains)]
    public void Constructor_Should_ThrowException_When_FilenameIsEmpty()
    {
      new NameFileRepository("");
    }

    [Test]
    public void Constructor_Should_NotThrowException_When_FileIsValid()
    {
      new NameFileRepository(ValidFile);
    }

    #endregion

    // Now we are able to test the that the names were loaded correctly

    [Test]
    [ExpectedException(typeof(FileNotFoundException), ExpectedMessage = "The file could not be found", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_FilenameIsInvalid()
    {
      var repo = new NameFileRepository("!@#$%^$%&(");
      repo.Load();
    }

    [Test]
    [ExpectedException(typeof(InvalidDataException), ExpectedMessage = "The data file must contain at least one line", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_FileIsEmpty()
    {
      var repo = new NameFileRepository(EmptyFile);
      repo.Load();
    }

    /// <summary>
    /// Starting to look like real unit testing, we can now assert something interesting
    /// </summary>
    [Test]
    public void Send_Should_ThrowException_When_MultipleParameterSpecified()
    {
      // Arrange
      var repo = new NameFileRepository(ValidFile);

      // Act
      var names = repo.Load();
      
      // Assert
      Assert.That(names, Is.Not.Null);

      var namesList = names.ToList();
      Assert.That(namesList.Count, Is.EqualTo(4));
      Assert.That(namesList[0], Is.EqualTo("bob"));
      Assert.That(namesList[1], Is.EqualTo("sally"));
      Assert.That(namesList[2], Is.EqualTo("fred"));
      Assert.That(namesList[3], Is.EqualTo("tina"));
    }    

    // Other tests could be added, e.g. trying to load a file that user doesn't have permission to read
  }
}