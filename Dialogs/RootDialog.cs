using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using TuDelft.Api;
using System.Linq;
namespace FindTuDelftWorkspaceBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            bool first;
            context.UserData.TryGetValue<bool>("first", out first);
            if (!first)
            {
                context.UserData.SetValue<bool>("first", true);
                await context.PostAsync($"What is your name?");
                context.Wait(this.MessageReceivedAsync);
                return;
            }



            var inputMessage = await result as Activity;


            string name;
            context.UserData.TryGetValue<string>("Name", out name);
          
            if (string.IsNullOrEmpty(name))
            {
                context.UserData.SetValue<string>("Name", inputMessage.Text);
                name = inputMessage.Text;


                await context.PostAsync($"Hi {name}");

             
                return;
            }


            //TODO add more questions


            await context.PostAsync($"No action");

        }
    }
}