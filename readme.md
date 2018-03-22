# Azure chatbot with TU Delft API

## Goal
Create a multi channel chatbot. The bot ask your name, your building and the number of requested places. To answer the user the bot uses the Tu-Delft web REST API. 


## Steps   
1. [Create a Azure account](https://azure.microsoft.com/en-us/free/students/) ([More info](Docs/Manual_azure_student_account.pdf))


2. Go to [https://portal.azure.com](https://portal.azure.com)
3. Create a bot in the portal

![create the bot](/Docs/CreateBot.png "Create a bot")

4. Choice a name for the bot and create the bot. (Auto generate keys is highly recommend) 
5. Go to your bot 'blade' by  Bot Services and select your bot.
6. Go to build and open online editor

![Botblade](/Docs/BotBlade.png "Bot blade")

7. Delete all files
8. Go to the Git-page and clone

```
https://github.com/JoostVanVelthoven/FindTuDelftWorkspaceBot  
```
9. Go to the explorer-tab and change /dialog/RootDialog.cs
10. Ask the user his name, his desired building and the number of required workplaces. Repost the users input. 
11. Open the console tab and build the app by running the command:
```
build.cmd
```
12. Go to back to the portal. Test your bot in the 'Test in Web Chat'-blade. To reset a session use: `/deleteprofile`

13. In the portal go to Channels. Register Skype as a channel.
14. Open Skype and test the channel.

![Skype](/Docs/Skype.png "Skype")

15. Now it's time to use the TU-Delft API! Because debugging Microsoft bots is time consuming we use a test console application.  Download the source from this repo.  (Because of time limitations, the REST API is already implemented in the solution). Try to filter the data. 
17. With this experience, it is time to implement the API in the bot. Migrate all the code from the console to the bot. (Off course using the online editor) 
18. Done!
 
