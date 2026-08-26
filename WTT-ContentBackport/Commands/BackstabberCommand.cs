using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Helpers.Commerce;
using SPTarkov.Server.Core.Helpers.Dialogue.Commando.SptCommands;
using SPTarkov.Server.Core.Helpers.Profile;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Dialog;
using SPTarkov.Server.Core.Models.Eft.Profile;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Services.Commerce;

namespace WTTContentBackport.Commands;

[Injectable]
public class BackstabberCommand(
    MailSendService mailSendService,
    RewardHelper rewardHelper,
    ProfileHelper profileHelper) : ISptCommand
{
    public ValueTask<string> PerformAction(UserDialogInfo commandHandler, MongoId sessionId, SendMessageRequest request)
    {
        var profile = profileHelper.GetFullProfile(sessionId);
        mailSendService.SendUserMessageToPlayer(sessionId, commandHandler, $"This REQUIRES a main menu refresh/reload in order to receive the achievement and unlock the Clothing");
        rewardHelper.AddAchievementToProfile(profile, "6948990c05f4f91bdb9a56f3");
        rewardHelper.AddAchievementToProfile(profile, "694c5527d1d40bd7db9a571a");
        return new ValueTask<string>(request.DialogId);
    }

    public string Command => "backstabber";

    public string CommandHelp => "Usage: Unlocks access to all post 1.0 Arena/Battlepass clothing";
}