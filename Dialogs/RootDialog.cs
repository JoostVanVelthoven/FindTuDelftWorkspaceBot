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
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome welcome welcome! What is your name?");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var inputMessage = await result as Activity;


            string name;
            context.UserData.TryGetValue<string>("Name", out name);

            if (string.IsNullOrEmpty(name))
            {
                context.UserData.SetValue<string>("Name", inputMessage.Text);
                name = inputMessage.Text;
                await context.PostAsync("What is your building of choice?");
            }

            string bulding;
            context.UserData.TryGetValue<string>("bulding", out bulding);
            if (string.IsNullOrEmpty(bulding))
            {
                context.UserData.SetValue<string>("bulding", inputMessage.Text);
                bulding = inputMessage.Text;

                await context.PostAsync("How many computers do you need?");

            }

            int requestedComputers;
            context.UserData.TryGetValue<int>("requestedComputers", out requestedComputers);
            if (requestedComputers != 0)
            {

                if (int.TryParse(inputMessage.Text, out int parsedInput))
                {
                    context.UserData.SetValue<int>("requestedComputers", parsedInput);
                    await context.PostAsync("Please wait :)");

                    TuDelftWorkspace.Get()
                       .Take(30)
                       .Where(a => a.Location.Contains(bulding))
                       .Where(a => a.NumberOfAvailableComputers >= parsedInput)
                       .ToList()
                       .ForEach(async place => await context.PostAsync($"{place.Location} -  {place.NumberOfAvailableComputers}"));

                }


            }



        }
    }
}