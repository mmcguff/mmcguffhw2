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
            var a = await argument as Activity;
            
                var response = "Thou hast said: " + a.Text;

                await context.PostAsync($"{response}");

                LogDatabase.WriteToDatabase
                 (
                     conversationid: a.Conversation.Id
                     , username: "ElderBot"
                     , channel: a.ChannelId
                     , message: response
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