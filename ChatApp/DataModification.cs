using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    public class DataModification
    {
        public static Byte[] StringEncoding(Message message)
        {
            var encodedMessage = Encoding.ASCII.GetBytes(message.text);
            return encodedMessage;
        }

        public static Message ByteDecoding( Byte[] encodedMessage, int messageByteLength)
        {
            Message message = new Message
            {
                text = Encoding.ASCII.GetString(encodedMessage, 0, messageByteLength)
            };
            return message;
        }
    }
}
