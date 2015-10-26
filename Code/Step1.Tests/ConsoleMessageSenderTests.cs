using System;
using System.IO;
using NUnit.Framework;

// ReSharper disable ExceptionNotDocumented
namespace Step1.Tests
{
  /// <summary>
  /// Tests for the <code>ConsoleMessageSender</code>.
  /// 
  /// What sort of tests can we write?  
  /// </summary>
  [TestFixture]
  public class ConsoleMessageSenderTests
  {
    private const string EmptyFile = @"Data\empty.txt";
    private const string ValidFile = @"Data\names.txt";
    private const string ValidMessage = "Hello {0}";

    #region Constructor
    
    [Test]
    [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "filename", MatchType = MessageMatch.Contains)]
    public void Constructor_Should_ThrowException_When_FilenameIsNull()
    {
      new ConsoleMessageSender(null);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "filename", MatchType = MessageMatch.Contains)]
    public void Constructor_Should_ThrowException_When_FilenameIsEmpty()
    {
      new ConsoleMessageSender("");
    }
    
    [Test]
    public void Constructor_Should_NotThrowException_When_FileIsValid()
    {
      new ConsoleMessageSender(ValidFile);
    }

    #endregion

    // Here we test the exceptional cases, but hard to test that the names were 
    // loaded as expected or that messages were sent out.

    [Test]
    [ExpectedException(typeof(FileNotFoundException), ExpectedMessage = "The file could not be found", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_FilenameIsInvalid()
    {
      var sender = new ConsoleMessageSender("!@#$%^$%&(");
      sender.Send(ValidMessage);
    }

    [Test]
    [ExpectedException(typeof(InvalidDataException), ExpectedMessage = "The data file must contain at least one line", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_FileIsEmpty()
    {
      var sender = new ConsoleMessageSender(EmptyFile);
      sender.Send(ValidMessage);
    }

    [Test]
    [ExpectedException(typeof(FormatException), ExpectedMessage = "Index (zero based) must be greater than or equal to zero and less than the size of the argument list.", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_MultipleParameterSpecified()
    {
      var sender = new ConsoleMessageSender(ValidFile);
      sender.Send("Hello {0} {1}");
    }

    /// <summary>
    /// We can test calling Send, and see it doesn't throw and exception, but how to we test that a
    /// message was sent to each person in the file and that the message was correct?  
    /// 
    /// Was was the output?  
    /// </summary>
    [Test]
    public void Send_Should_SendAMessage_When_OneParameterSpecified()
    {
      var sender = new ConsoleMessageSender(ValidFile);
      sender.Send("Hello {0}");
    }

    [Test]
    public void Send_Should_NotThrowException_When_NoParameterSpecified()
    {
      var sender = new ConsoleMessageSender(ValidFile);
      sender.Send("Hello");
    }  
  }
}