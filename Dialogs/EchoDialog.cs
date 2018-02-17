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

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(AnswerQuestion);
        }

        public async Task AnswerQuestion(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var a = await argument as Activity;
            var response = "";

            if (a.Text == "faith")
            {
                response = "You must demostrate your faith by your works [James 2:15](https://www.lds.org/scriptures/jst/jst-james/2.15?lang=eng#14).";
            }
            else if (a.Text == "repentance")
            {
                response = "See how slack and repentace are related: [2 peter 3:9](https://www.lds.org/scriptures/nt/2-pet/3.9?lang=eng#8).";
            }
            else if (a.Text == "baptism")
            {
                response = "See what the Book of Momrons says about Baptism: [3 Nephi 30: 1-2](https://www.lds.org/scriptures/bofm/3-ne/30.1,2?lang=eng#1).";
            }
            else
            {
                response = "That is a good question!  I don't have a great answer for that but as Latter Days Saints we believe that all can receive answer from God  [Moroni 10:4-5](https://www.lds.org/scriptures/bofm/moro/10.4,5?lang=eng#2).";
            }

            await context.PostAsync($"{response}");

            LogDatabase.WriteToDatabase
             (
                 conversationid: a.Conversation.Id
                 , username: "ElderBot"
                 , channel: a.ChannelId
                 , message: response
             );

            var checkForUnderstanding = "Does this answer you question?";

            await context.PostAsync($"{checkForUnderstanding}");

            LogDatabase.WriteToDatabase
            (
                conversationid: a.Conversation.Id
                , username: "ElderBot"
                , channel: a.ChannelId
                , message: checkForUnderstanding
            );

            context.Wait(HandleYesNo);
        }

        public async Task Welcome(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var a = await argument as Activity;
            var welcome = "Hi, my name is Elder Bot.  Right now I can teach you about Faith, Repentance and Baptism.  What would you like to know more about?";

            await context.PostAsync($"{welcome}");
            LogDatabase.WriteToDatabase
            (
                conversationid: a.Conversation.Id
                , username: "ElderBot"
                , channel: a.ChannelId
                , message: welcome
            );
  
            context.Wait(AnswerQuestion);
        }

        public async Task HandleYesNo(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var a = await argument as Activity;
            var invite = "After centuries of being lost, the gospel of Jesus Christ has been restored to the earth by our loving Heavenly Father through a living prophet. The Book of Mormon is evidence of this. You can hold it in your hands. You can read it, ponder how the message in the book can improve your life, and pray to know that the message is the word of God.  I would like to offer your own personal copy of this book.Let me get some quick information from you:";
            var apologize = "I'm sorry I didn't answer your question.  I'm still learning.";
            var promptAgain = "Sorry, I didn't catch whether I answered that last question correctly.";


            if (a.Text == "yes")
            {
                await context.PostAsync($"{invite}");

                LogDatabase.WriteToDatabase
                 (
                     conversationid: a.Conversation.Id
                     , username: "ElderBot"
                     , channel: a.ChannelId
                     , message: invite
                 );

                var askAgain = "Glad I could help.  Right now I can teach you about Faith, Repentance and Baptism.  What would you like to know more about?";

                await context.PostAsync($"{askAgain}");
                LogDatabase.WriteToDatabase
                  (
                      conversationid: a.Conversation.Id
                      , username: "ElderBot"
                      , channel: a.ChannelId
                      , message: askAgain
                  );

                context.Wait(AnswerQuestion);

            }
            else if(a.Text =="no")
            {
                await context.PostAsync($"{apologize}");

                LogDatabase.WriteToDatabase
                 (
                     conversationid: a.Conversation.Id
                     , username: "ElderBot"
                     , channel: a.ChannelId
                     , message: apologize
                 );

                var askAgain = "Right now I can teach you about Faith, Repentance and Baptism.  What would you like to know more about?";

                await context.PostAsync($"{askAgain}");
                LogDatabase.WriteToDatabase
                  (
                      conversationid: a.Conversation.Id
                      , username: "ElderBot"
                      , channel: a.ChannelId
                      , message: askAgain
                  );

                context.Wait(AnswerQuestion);
            }
            else
            {
                await context.PostAsync($"{promptAgain}");

                LogDatabase.WriteToDatabase
                 (
                     conversationid: a.Conversation.Id
                     , username: "ElderBot"
                     , channel: a.ChannelId
                     , message: promptAgain
                 );


            }






        }
    }
}