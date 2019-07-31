using System;
using Xunit;
using ChatApp;
using System.Text;

namespace ChatApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void DataModification_StringEncodingTest()
        {
            Message message = new Message()
            {
                text = "Hello"
            };
            Byte[] actualEncodedMessage = new Byte[1024];
            Byte[] expectedEncodedMessage = new Byte[1024];

            actualEncodedMessage = DataModification.StringEncoding(message);
            expectedEncodedMessage = Encoding.ASCII.GetBytes("Hello");
            Assert.Equal(expectedEncodedMessage, actualEncodedMessage);
        }

        [Fact]
        public void DataModification_ByteDecodingTest()
        {
            Message actualMessage = new Message();
            Message expectedMessage = new Message()
            {
                text = "Hello"
            };

            Byte[] inputByteMessage = new Byte[1024];
           
            inputByteMessage = Encoding.ASCII.GetBytes("Hello");
            actualMessage = DataModification.ByteDecoding(inputByteMessage, 5);
            Assert.Equal(expectedMessage.text, actualMessage.text);
        }
    }
}
