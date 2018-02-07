using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Web.Http.Description;
using System.Net.Http;
using SimpleEchoBot.Dialogs;
using System;
using System.Linq;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public int msgcount = 0;
        //LogDatabase l = new LogDatabase();
        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and send replies
        /// </summary>
        /// <param name="activity"></param>
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            // check if activity is of type message
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {



                LogDatabase.WriteToDatabase
                (
                    conversationid: activity.Conversation.Id
                    , username: activity.From.Name
                    , channel: activity.ChannelId
                    , message: activity.Text
                );

                await Conversation.SendAsync(activity, () => new EchoDialog());

            }
            else
            {
                await HandleSystemMessageAsync(activity);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }

        private async Task<Activity> HandleSystemMessageAsync(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

                if (message.MembersAdded.Any(o => o.Id == message.Recipient.Id))
                {
                    var welcome = "Hi, my name is Elder Bot.  How can I help you?";
                    var reply = message.CreateReply(welcome);
              
                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                    if (msgcount == 0)
                    { 
                        LogDatabase.WriteToDatabase
                        (
                            conversationid: message.Conversation.Id
                            , username: "ElderBot"
                            , channel: message.ChannelId
                            , message: welcome
                        );
                        msgcount++;
                    }

                    await connector.Conversations.ReplyToActivityAsync(reply);
                }




            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}