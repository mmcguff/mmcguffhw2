using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Dialogs
{
    public class LogDatabase
    {
        //In the future we might want to pass a string array with this data
        public static void WriteToDatabase(string conversationid, string username, string channel, string message)
        {
            // Instantiate the BotData dbContext
            Models.BotData DB = new Models.BotData();

            // Create a new UserLog object
            Models.UserLog NewUserLog = new Models.UserLog
            {
                // Set the properties on the UserLog object
                ConversationId = conversationid,
                UserName = username,
                Channel = channel,
                Message = message.Truncate(500),
                Created = DateTime.UtcNow
            };

            // Add the UserLog object to UserLogs
            DB.UserLogs.Add(NewUserLog);

            // Save the changes to the database
            DB.SaveChanges();

            // return our reply to the user
        }
    }
}