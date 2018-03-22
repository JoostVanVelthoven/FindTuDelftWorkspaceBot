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

                await context.PostAsync("What is your building of choice?");
                context.Wait(this.MessageReceivedAsync);
                return;
            }


            string bulding;
            context.UserData.TryGetValue<string>("bulding", out bulding);

            if (string.IsNullOrEmpty(bulding))
            {

                context.UserData.SetValue<string>("bulding", inputMessage.Text);
                bulding = inputMessage.Text;

                await context.PostAsync("How many computers do you need?");
                context.Wait(this.MessageReceivedAsync);
                return;

            }

            int requestedComputers;
            context.UserData.TryGetValue<int>("requestedComputers", out requestedComputers);
            if (requestedComputers == 0)
            {
                int parsedInput;
                if (int.TryParse(inputMessage.Text, out parsedInput))
                {
                    context.UserData.SetValue<int>("requestedComputers", parsedInput);
                    await context.PostAsync("Please wait :)");

                    TuDelftWorkspace.Get()
                       .Take(30)
                       .Where(a => a.Location.Contains(bulding))
                       .Where(a => a.NumberOfAvailableComputers >= parsedInput)
                       .ToList()
                       .ForEach(async place => await context.PostAsync($"{place.Location} -  {place.NumberOfAvailableComputers}"));

                    return;

                }


            }


            await context.PostAsync($"No action");

        }
    }
}