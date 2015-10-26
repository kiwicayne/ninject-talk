using System;
using System.IO;
using NUnit.Framework;

// ReSharper disable ExceptionNotDocumented
namespace Step2.Tests
{
  /// <summary>
  /// Tests for the <code>ConsoleMessageSender</code>.
  /// 
  /// This is a little better, but it still isn't easy to test.  We still need to reference 
  /// files to test the message sender, even though it no longer deals with loading 
  /// names from files directly.  Because ConsoleMessageSender still has a dependency 
  /// on NameRespository, this is an integration test that is testing the both 
  /// classes instead of just the ConsoleMessageSender (a single unit).
  /// </summary>
  [TestFixture]
  public class ConsoleMessageSenderTests
  {
    private const string EmptyFile = @"Data\empty.txt";
    private const string ValidFile = @"Data\names.txt";
    private const string ValidMessage = "Hello {0}";

    #region Constructor

    [Test]
    [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "nameRepository", MatchType = MessageMatch.Contains)]
    public void Constructor_Should_ThrowException_When_NameRepositoryIsNull()
    {
      new ConsoleMessageSender(null);
    }

    // Integration test
    [Test]
    public void Constructor_Should_NotThrowException_When_FileIsValid()
    {
      new ConsoleMessageSender(new NameFileRepository(ValidFile));
    }

    #endregion  

    [Test]
    [ExpectedException(typeof(FileNotFoundException), ExpectedMessage = "The file could not be found", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_FilenameIsInvalid()
    {
      var sender = new ConsoleMessageSender(new NameFileRepository("!@#$%^$%&("));
      sender.Send(ValidMessage);
    }

    [Test]
    [ExpectedException(typeof(InvalidDataException), ExpectedMessage = "The data file must contain at least one line", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_FileIsEmpty()
    {
      var sender = new ConsoleMessageSender(new NameFileRepository(EmptyFile));
      sender.Send(ValidMessage);
    }

    [Test]
    [ExpectedException(typeof(FormatException), ExpectedMessage = "Index (zero based) must be greater than or equal to zero and less than the size of the argument list.", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_MultipleParameterSpecified()
    {
      var sender = new ConsoleMessageSender(new NameFileRepository(ValidFile));
      sender.Send("Hello {0} {1}");
    }

    /// <summary>
    /// We can test calling Send, and see it doesn't throw and exception, but how to we test that a
    /// message was sent to each person in the file and that the message was correct?    
    /// </summary>
    [Test]
    public void Send_Should_SendAMessage_When_OneParameterSpecified()
    {
      var sender = new ConsoleMessageSender(new NameFileRepository(ValidFile));
      sender.Send("Hello {0}");
    }

    [Test]
    public void Send_Should_NotThrowException_When_NoParameterSpecified()
    {
      var sender = new ConsoleMessageSender(new NameFileRepository(ValidFile));
      sender.Send("Hello");
    }
  }
}