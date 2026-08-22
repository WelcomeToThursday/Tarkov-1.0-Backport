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
public class EndingsCommand(
    MailSendService mailSendService,
    RewardHelper rewardHelper,
    ProfileHelper profileHelper) : ISptCommand
{
    public ValueTask<string> PerformAction(UserDialogInfo commandHandler, MongoId sessionId, SendMessageRequest request)
    {
        IEnumerable<MongoId> endingbackgrounds = new MongoId[]
        {
            "68fb63be323974a4ae011df8",
            "68fb638ebe7aff394b016818",
            "68fb63afc09c82a74d07d21f",
            "68fb63c9be7aff394b01681a",
        };
        var profile = profileHelper.GetFullProfile(sessionId);
        profile.AddCustomisations(endingbackgrounds, "environment", CustomisationSource.DEFAULT);
        rewardHelper.AddAchievementToProfile(profile, "694c60b50cb1e6ad639a5723");
        rewardHelper.AddAchievementToProfile(profile, "6948990c05f4f91bdb9a56f3");
        if (!profileHelper.PlayerHasReceivedMaxNumberOfGift(sessionId, "ibeatthegameiswear", 1))
        {
            profileHelper.FlagGiftReceivedInProfile(sessionId, "ibeatthegameiswear", 1);
            mailSendService.SendSystemMessageToPlayer(sessionId, $"Endings rewards sent! Enjoy you filthy cheater!",
                new List<Item>
                {
                    new()
                    {
                        Id = "69d546e56cd0336ce513f932",
                        Template = "68d41f017cb8c03da8079004",
                        Upd = new Upd()
                        {
                            SpawnedInSession = true,
                            StackObjectsCount = 1
                        }
                    },
                    new()
                    {
                        Id = "69d546e8a9d603390d13f933",
                        Template = "68d41e65a706eba9a204ed27",
                        Upd = new Upd()
                        {
                            SpawnedInSession = true,
                            StackObjectsCount = 1
                        }
                    },
                    new()
                    {
                        Id = "69d546e9a2073bc5b313f934",
                        Template = "68d41fb0a1c275dbaa0798bf",
                        Upd = new Upd()
                        {
                            SpawnedInSession = true,
                            StackObjectsCount = 1
                        }
                    },
                    new()
                    {
                        Id = "69d546eb08df0b9c4e13f935",
                        Template = "68d41fde2937f3f17c04864d",
                        Upd = new Upd()
                        {
                            SpawnedInSession = true,
                            StackObjectsCount = 1
                        }
                    }
                });
        }

        return new ValueTask<string>(request.DialogId);
    }

    public string Command => "ibeatthegameiswear";

    public string CommandHelp => "Usage: Receive all four endings customizations (dogtags, hideout, main menu environments, armbands, etc)";
}