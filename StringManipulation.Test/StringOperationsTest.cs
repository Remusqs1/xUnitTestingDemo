
using Microsoft.Extensions.Logging;
using Moq;

namespace StringManipulation.Test
{
    public class StringOperationsTest
    {
        [Fact(Skip = "We are akipping this test for now, Ticket-001")]
        public void ConcatenateStringsSkip()
        {
            //Arrange
            StringOperations strOperation = new();

            //Act
            var result = strOperation.ConcatenateStrings("Hallo", "Welt");

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Hallo Welt", result);
        }

        [Theory]
        [InlineData("Hallo", "Welt", "Hallo Welt")]
        [InlineData("Deutschland", "ist Toll", "Deutschland ist Toll")]
        public void ConcatenateStrings(string input1, string input2, string expected)
        {
            //Arrange
            StringOperations strOperation = new();

            //Act
            var result = strOperation.ConcatenateStrings(input1, input2);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsPalindromeTrue()
        {
            //Arrange
            StringOperations strOperation = new();

            //Act
            var result = strOperation.IsPalindrome("ama");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPalindromeFalse()
        {
            //Arrange
            StringOperations strOperation = new();

            //Act
            var result = strOperation.IsPalindrome("Deutschland");

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void RemoveWhitespace()
        {
            //Arrange
            StringOperations strOperation = new();

            //Act
            var result = strOperation.RemoveWhitespace("Deutschland ist Toll");

            Assert.DoesNotContain(" ", result);
        }

        [Fact]
        public void QuantintyInWords()
        {
            StringOperations strOperation = new();
            var result = strOperation.QuantintyInWords("cat", 10);
            Assert.StartsWith("ten", result);
            Assert.Contains("cat", result);
        }

        [Fact]
        public void GetStringLengthException()
        {
            StringOperations strOperation = new();
            //No Act 'cos of exception
            Assert.ThrowsAny<ArgumentNullException>(() => strOperation.GetStringLength(null));
        }

        [Fact]
        public void TruncateStringException()
        {
            StringOperations strOperation = new();
            //No Act 'cos of exception
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => strOperation.TruncateString("Deutschland ist Toll", 0));
        }

        [Fact]
        public void TruncateString()
        {
            StringOperations strOperation = new();
            var result = strOperation.TruncateString("Deutschland ist Toll", 11);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Deutschland", result);
        }

        [Theory]
        [InlineData("Test", 5)] 
        [InlineData("Hello, World!", 0)]
        [InlineData("Lorem Ipsum", 11)]
        public void TruncateStringTheory(string input, int maxLength)
        {
            StringOperations strOperation = new StringOperations();

            if (maxLength <= 0)
            {
                Assert.ThrowsAny<ArgumentOutOfRangeException>(() => strOperation.TruncateString(input, maxLength));
            }
            else if (maxLength >= input.Length || string.IsNullOrEmpty(input))
            {
                Assert.Equal(input, strOperation.TruncateString(input, maxLength));
            }
            else
            {
                string truncated = strOperation.TruncateString(input, maxLength);
                Assert.Equal(input.Substring(0, maxLength), truncated);
            }
        }


        [Fact]
        public void GetStringLength()
        {
            StringOperations strOperation = new();
            string testString = "Deutschland ist Toll";
            var result = strOperation.GetStringLength(testString);

            Assert.Equal(testString.Length, result);
        }

        [Theory]
        [InlineData("V", 5)]
        [InlineData("III", 3)]
        [InlineData("iii", 3)]
        [InlineData("X", 10)]
        public void FromRomanToNumber(string romanNumber, int expected)
        {
            StringOperations strOperation = new();
            var result = strOperation.FromRomanToNumber(romanNumber);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("P")]
        [InlineData("H")]
        [InlineData(null)]
        public void FromRomanToNumberException(string romanNumber)
        {
            StringOperations strOperation = new();
            Assert.ThrowsAny<ArgumentException>(() => strOperation.FromRomanToNumber(romanNumber));
        }

        //IsPalindrome but with Theory instead of Fact
        [Theory]
        [InlineData("ama", true)]
        [InlineData("Deutschland", false)]
        public void IsPalindrome(string input, bool expected)
        {
            StringOperations strOperation = new();
            var result = strOperation.IsPalindrome(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CountOccurrences()
        {
            var mockLogger = new Mock<ILogger<StringOperations>>();
            StringOperations strOperation = new(mockLogger.Object);
            var result = strOperation.CountOccurrences("Hallo Welt", 'l');
            Assert.Equal(3, result);
        }

        [Fact]
        public void ReadFile()
        {
            StringOperations strOperation = new();
            var mockFileReader = new Mock<IFileReaderConector>();
            //mockFileReader.Setup(s => s.ReadString("file.txt")).Returns("Reading File");
            mockFileReader.Setup(s => s.ReadString(It.IsAny<string>())).Returns("Reading File");
            var result = strOperation.ReadFile(mockFileReader.Object, "file2.txt");
            Assert.Equal("Reading File", result);
        }

        [Theory]
        [InlineData("Reverse", "esreveR")]
        [InlineData("Deutschland", "dnalhcstueD")]
        public void ReverseString(string input, string expected)
        {
            StringOperations strOperation = new();
            var result = strOperation.ReverseString(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("cat", "cats")]
        [InlineData("grocery", "groceries")]
        public void PluralizeSuccess(string input, string expected)
        {
            StringOperations strOperation = new();
            var result = strOperation.Pluralize(input);
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("cat", "caties")]
        [InlineData("grocery", "grocerys")]
        public void PluralizeFail(string input, string expected)
        {
            StringOperations strOperation = new();
            var result = strOperation.Pluralize(input);
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.NotEqual(expected, result);
        }

    }
}
