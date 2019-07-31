using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    public class ChatApp
    {
        public static void Start()
        {
            User user = new User();
            user.SetDetails();

            Conversation.Begin(user);
            
        }
    }
}
