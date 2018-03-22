# Azure chatbot with TU Delft API

1. [Create a Azure account](https://azure.microsoft.com/en-us/free/students/) ([More info](Docs/Manual_azure_student_account.pdf))


2. Go to [https://portal.azure.com](https://portal.azure.com)
3. Create a bot in the portal

![create the bot](/Docs/CreateBot.png "Create a bot")

4. Choise a name for the bot and create the bot. (Auto generate keys is highly recommend) 
5. Go to your bot 'blade' by  Bot Services and select your bot.
6. Go to build and open online editor

![Botblade](/Docs/BotBlade.png "Bot blade")

7. Delete all files
8. Go to the Git-page and clone

```
https://github.com/JoostVanVelthoven/FindTuDelftWorkspaceBot  
```
9. Go to the explorer-tab and change /dialog/RootDialog.cs
10. Ask the user his name, his disered building and the number of required workplaces.
11. Open the console tab and build the app by running the command:
```
build.cmd
```
12. 