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
            await context.PostAsync("Welke locatie zoekt u!");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var inputMessage = await result as Activity;
            string location;

            context.UserData.TryGetValue<string>("Name", out location);

            if(string.IsNullOrEmpty(location))
            {
                context.UserData.SetValue<string>("Name", inputMessage.Text);
                location = inputMessage.Text;
            }

            TuDelftWorkspace.Get()
                .Take(10)
                .ToList()
                .ForEach(async place => await context.PostAsync($"{place.Location} -  {place.NumberOfAvailableComputers}"));

        }
    }
}