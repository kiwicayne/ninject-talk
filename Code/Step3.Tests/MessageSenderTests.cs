using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Step3.Interfaces;

// ReSharper disable ExceptionNotDocumented
namespace Step3.Tests
{
  /// <summary>
  /// Tests for the <code>MessageSender</code>.
  /// 
  /// This is much better.  The two dependencies (INameRepository & IWriter) 
  /// can be mocked, and the only unit of code being tested is the MessageSender.
  /// </summary>
  [TestFixture]
  public class MessageSenderTests
  {    
    private const string ValidMessage = "Hello {0}";

    private Mock<INameRepository> _mockNameRepository;
    private Mock<IWriter> _mockWriter;

    [SetUp]
    public void SetUp()
    {
      // Strict means that if an expectiation is not setup but it happens, the test will fail
      _mockNameRepository = new Mock<INameRepository>(MockBehavior.Strict);
      _mockWriter = new Mock<IWriter>(MockBehavior.Strict);
    }

    #region Constructor

    [Test]
    [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "nameRepository", MatchType = MessageMatch.Contains)]
    public void Constructor_Should_ThrowException_When_NameRepositoryIsNull()
    {
      new MessageSender(null, _mockWriter.Object);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "writer", MatchType = MessageMatch.Contains)]
    public void Constructor_Should_ThrowException_When_WriterIsNull()
    {
      new MessageSender(_mockNameRepository.Object, null);
    }

    [Test]
    public void Constructor_Should_NotThrowException_When_ParametersAreNotNull()
    {
      new MessageSender(_mockNameRepository.Object, _mockWriter.Object);
    }


    #endregion
    
    // No more files being loaded, using mocks to control different cases

    [Test]
    [ExpectedException(typeof(Exception), ExpectedMessage = "Test exception to simulate failure loading names", MatchType = MessageMatch.Exact)]
    public void Send_Should_ThrowException_When_NameRepositoryCannotLoadNames()
    {
      // Arrange
      _mockNameRepository.Setup(x => x.Load())
        .Throws(new Exception("Test exception to simulate failure loading names"));

      // Act
      var sender = new MessageSender(_mockNameRepository.Object, _mockWriter.Object);
      sender.Send(ValidMessage);
    }


    // Finally it is easy to test the functionality of the MessageSender! 

    [Test]
    public void Send_Should_WriteMessages_When_NameRespositoryReturnsOneName()
    {
      // Arrange
      _mockNameRepository.Setup(x => x.Load()).Returns(new[] { "Fred" });
      _mockWriter.Setup(x => x.WriteLine("Hello Fred"));

      // Act
      var sender = new MessageSender(_mockNameRepository.Object, _mockWriter.Object);
      sender.Send("Hello {0}");

      // Assert
      _mockNameRepository.VerifyAll();
      _mockWriter.VerifyAll();
    }

    [Test]
    public void Send_Should_WriteMessages_When_NameRespositoryReturnsMultipleNames()
    {
      // Arrange
      _mockNameRepository.Setup(x => x.Load()).Returns(new[] { "Bob", "Sally" });
      _mockWriter.Setup(x => x.WriteLine("Hello Bob"));
      _mockWriter.Setup(x => x.WriteLine("Hello Sally"));

      // Act
      var sender = new MessageSender(_mockNameRepository.Object, _mockWriter.Object);
      sender.Send("Hello {0}");

      // Assert
      _mockNameRepository.VerifyAll();
      _mockWriter.VerifyAll();
    }

    // And we can easily test other cases now that were difficult before

    [Test]
    public void Send_Should_WriteNothing_When_NameRespositoryReturnsNull()
    {
      // Arrange
      _mockNameRepository.Setup(x => x.Load()).Returns((IEnumerable<string>)null);

      // Act
      var sender = new MessageSender(_mockNameRepository.Object, _mockWriter.Object);
      sender.Send(ValidMessage);

      // Assert
      _mockNameRepository.VerifyAll();
      _mockWriter.VerifyAll();
    }

    [Test]
    public void Send_Should_WriteNothing_When_NameRespositoryReturnsEmptyEnumeration()
    {
      // Arrange
      _mockNameRepository.Setup(x => x.Load()).Returns(new string[] {});

      // Act
      var sender = new MessageSender(_mockNameRepository.Object, _mockWriter.Object);
      sender.Send(ValidMessage);

      // Assert
      _mockNameRepository.VerifyAll();
      _mockWriter.VerifyAll();
    }
  }
}