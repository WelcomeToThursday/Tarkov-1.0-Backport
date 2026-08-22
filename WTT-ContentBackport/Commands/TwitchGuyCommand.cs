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
public class TwitchGuyCommand(
    MailSendService mailSendService,
    RewardHelper rewardHelper,
    ProfileHelper profileHelper) : ISptCommand
{
    public ValueTask<string> PerformAction(UserDialogInfo commandHandler, MongoId sessionId, SendMessageRequest request)
    {
        var profile = profileHelper.GetFullProfile(sessionId);

        if (!profileHelper.PlayerHasReceivedMaxNumberOfGift(sessionId, "twitchguytwitchguytwitchguy", 1))
        {
            profileHelper.FlagGiftReceivedInProfile(sessionId, "twitchguytwitchguytwitchguy", 1);
            mailSendService.SendSystemMessageToPlayer(sessionId, $"Twitch rewards sent! Enjoy you filthy cheater!", new List<Item>
            {
                new()
                {
                    Id = "694d5b11eb5884255831c5ec",
                    Template = "6937ecc3dbdccab44605fcf0",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d61014625f4192131c606",
                    Template = "68f25b772605ccea240dd684",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d6112ebcde84a5b31c607",
                    Template = "688b2e574172ca83e70cf868",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d5ba6ba1951091431c5ef",
                    Template = "6937eccbfd921faceb0dfecd",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d5c912184e8896831c5f1",
                    Template = "68f117b8121d878a2303eee0",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d5cd4b5789bf0ae31c5f3",
                    Template = "68d55968ca9935b3f10607a9",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                }
            });
            rewardHelper.AddAchievementToProfile(profile, "6948990c05f4f91bdb9a56f3");
            rewardHelper.AddAchievementToProfile(profile, "694d58833cff7ff7be31c5e8");
        }
        
        
        return new ValueTask<string>(request.DialogId);

    }

    public string Command => "twitchguytwitchguytwitchguy";

    public string CommandHelp => "Usage: Receive all post 1.0 Twitch drop content";
}