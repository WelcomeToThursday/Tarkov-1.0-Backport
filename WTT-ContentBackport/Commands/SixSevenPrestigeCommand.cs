using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Helpers.Commerce;
using SPTarkov.Server.Core.Helpers.Dialogue.Commando.SptCommands;
using SPTarkov.Server.Core.Helpers.Profile;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Eft.Dialog;
using SPTarkov.Server.Core.Models.Eft.Profile;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Services.Commerce;
using WTTServerCommonLib.Helpers;

namespace WTTContentBackport.Commands;

[Injectable]
public class SixSevenPrestigeCommand(
    MailSendService mailSendService,
    RewardHelper rewardHelper,
    ProfileHelper profileHelper) : ISptCommand
{
    public ValueTask<string> PerformAction(UserDialogInfo commandHandler, MongoId sessionId, SendMessageRequest request)
    {
        var profile = profileHelper.GetFullProfile(sessionId);
        profile.AddCustomisation("68f12827372e7bb1de0d7361", "environment", CustomisationSource.DEFAULT);
        profile.AddCustomisation("683dc6aa57f38ea79c0073f1", "wall", CustomisationSource.DEFAULT);
        profile.AddCustomisation("683dc67e5e8c96c28e0ce6f8", "ceiling", CustomisationSource.DEFAULT);

        if (!profileHelper.PlayerHasReceivedMaxNumberOfGift(sessionId, "67prestigeicon", 1))
        {
            profileHelper.FlagGiftReceivedInProfile(sessionId, "67prestigeicon", 1);
            mailSendService.SendUserMessageToPlayer(sessionId, commandHandler, $"This REQUIRES a full game restart in order to see the new Head options");
            mailSendService.SendSystemMessageToPlayer(sessionId, $"Prestige 5 & 6 rewards sent! Enjoy you filthy cheater!", new List<Item>
            {
                new()
                {
                    Id = "694d59798a66b0d06931c5ea",
                    Template = "68f117b8121d878a2303eee0",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d60dff38773492631c604",
                    Template = "68d65766916a108d7a023c98",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                }
            });
            IEnumerable<MongoId> prestigeHeads = new MongoId[]
            {
                "68fb8872fb3842532002cbc1",
                "68fb88b9d4b0e9617502c1c4",
            };
            profile.AddCustomisations(prestigeHeads, "head", CustomisationSource.DEFAULT);
            rewardHelper.AddAchievementToProfile(profile, "6948990c05f4f91bdb9a56f3");
            rewardHelper.AddAchievementToProfile(profile, "694c6575af08f6f1d59a5737");
        }
        
        return new ValueTask<string>(request.DialogId);
    }

    public string Command => "67prestige";

    public string CommandHelp => "Usage: Receive Prestige 5 and 6 content";
}