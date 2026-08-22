using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Callbacks;
using SPTarkov.Server.Core.Controllers;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Helpers.Commerce;
using SPTarkov.Server.Core.Helpers.Dialogue.Commando.SptCommands;
using SPTarkov.Server.Core.Helpers.Profile;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Eft.Dialog;
using SPTarkov.Server.Core.Models.Eft.Profile;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Spt.Dialog;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Services.Commerce;
using WTTServerCommonLib.Helpers;

namespace WTTContentBackport.Commands;

[Injectable]
public class AllTheClothesCommand(
    MailSendService mailSendService,
    RewardHelper rewardHelper,
    ProfileHelper profileHelper) : ISptCommand
{
    public ValueTask<string> PerformAction(UserDialogInfo commandHandler, MongoId sessionId, SendMessageRequest request)
    {
        var profile = profileHelper.GetFullProfile(sessionId);
        rewardHelper.AddAchievementToProfile(profile, "6948990c05f4f91bdb9a56f3");
        rewardHelper.AddAchievementToProfile(profile, "694c5cae0256a6bd999a5721");
        IEnumerable<MongoId> civilianHeads = new MongoId[]
        {
            "68a6fb5ca1eb443ead010e8a",
            "68a6fb6d62b09585f80dd73b",
            "68a6fb7a226a60005506faff",
            "68a6fb8ac8a3ef285c060b7b",
        };
        IEnumerable<MongoId> civilianVoices = new MongoId[]
        {
            "68e7a025dcdb0a85f4046d69",
            "68e7a04c481f58ac9702afb4",
            "68e7a05e2da4d083ce0f2146",
            "68e7a0773f9907cd5508dbcc",
        };
        IEnumerable<MongoId> blackDivisionVoices = new MongoId[]
        {
            "6899fcdea469e729c40cbbde",
            "6899fff4c17f776ecb07fafd",
        };
        IEnumerable<MongoId> ruafVoices = new MongoId[]
        {
            "68a322944dc8ea81b603528f",
            "68a322c44dc8ea81b6035293",
        };
        profile.AddCustomisations(civilianHeads, "head", CustomisationSource.DEFAULT);
        profile.AddCustomisations(civilianVoices, "voice", CustomisationSource.DEFAULT);
        profile.AddCustomisations(blackDivisionVoices, "voice", CustomisationSource.DEFAULT);
        profile.AddCustomisations(ruafVoices, "voice", CustomisationSource.DEFAULT);
        profile.AddCustomisation("69c68f1a8f75eda7610edac4", "voice", CustomisationSource.DEFAULT);
        mailSendService.SendUserMessageToPlayer(sessionId, commandHandler, $"This REQUIRES a main menu refresh/reload in order to receive the achievement and unlock the Clothing");
        mailSendService.SendUserMessageToPlayer(sessionId, commandHandler, $"This REQUIRES a full game restart in order to see the new Head and Voice options.");
        return new ValueTask<string>(request.DialogId);
    }

    public string Command => "givemealltheclothes";

    public string CommandHelp => "Usage: Unlocks all Black Division, RUAF, and Civilian customizations";
}