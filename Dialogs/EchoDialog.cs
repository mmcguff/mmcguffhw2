using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using SimpleEchoBot.Dialogs;


namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        //LogDatabase l = new LogDatabase();
        //protected int count = 1;
       

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var invite = "After centuries of being lost, the gospel of Jesus Christ has been restored to the earth by our loving Heavenly Father through a living prophet. The Book of Mormon is evidence of this. You can hold it in your hands. You can read it, ponder how the message in the book can improve your life, and pray to know that the message is the word of God.  I would like to offer your own personal copy of this book.Let me get some quick information from you:";


            var a = await argument as Activity;
            var response = "";// = "Repeating: " + a.Text;

            if (a.Text =="a")
            {
                response = "Response A";
            }
            else if (a.Text == "b")
            {
                response = "Response b";
            }
            else if (a.Text == "c")
            {
                response = "Response c";
            }
            else
            {
                response = "That is a good question!  I don't have a great answer for that but as Latter Days Saints we believe that all can receive answer from God (see <https://www.lds.org/scriptures/bofm/moro/10.4,5?lang=eng#2 | Moroni 10:4-5>)";
            }

            await context.PostAsync($"{response}");

                LogDatabase.WriteToDatabase
                 (
                     conversationid: a.Conversation.Id
                     , username: "ElderBot"
                     , channel: a.ChannelId
                     , message: response
                 );

            await context.PostAsync($"{invite}");

            //We might be able to get out of logging this information as it may make everything verbose
            //It can be assurmed that the bot is going to say what its saying to the user perhaps.
            //However so I can ensure it's doing whats its suppose to the logging stays. 
            LogDatabase.WriteToDatabase
            (
                conversationid: a.Conversation.Id
                , username: "ElderBot"
                , channel: a.ChannelId
                , message: invite
            );

            context.Wait(MessageReceivedAsync);            
        }

        //public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        //{
        //    var confirm = await argument;
        //    if (confirm)
        //    {
        //        this.count = 1;
        //        await context.PostAsync("Reset count.");
        //    }
        //    else
        //    {
        //        await context.PostAsync("Did not reset count.");
        //    }
        //    context.Wait(MessageReceivedAsync);
        //}

    }
}