using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using tuwerkplekkenzoeker;

namespace FindTuDelftWorkspaceBot.Dialogs
{

    [Serializable]
    public class SandwichOrder
    {

        public int NumberOfRequieredComputers { get; set; }

        public static IForm<SandwichOrder> BuildForm()
        {
            return new FormBuilder<SandwichOrder>()
                    .Message("Welcome to the simple sandwich order bot!")
                    
                    .OnCompletion(OnComplete)
                    .Build();
        }

        private static async Task<IDialogContext> OnComplete(IDialogContext context, SandwichOrder state)
        {
            await context.PostAsync("Welkom bij QDelft :)");

            var data = Program.Main();

            return context;
        }
    }
}