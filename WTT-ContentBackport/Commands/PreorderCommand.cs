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
public class PreorderCommand(
    MailSendService mailSendService,
    RewardHelper rewardHelper,
    ProfileHelper profileHelper) : ISptCommand
{
    public ValueTask<string> PerformAction(UserDialogInfo commandHandler, MongoId sessionId, SendMessageRequest request)
    {
        var profile = profileHelper.GetFullProfile(sessionId);
        IEnumerable<MongoId> preorderposes = new MongoId[]
        {
            "68fb7e94d4b0e9617502c1ae",
            "68fb7ec2fb3842532002cba7",
            "68fb7eb310742e456a0fa857",
        };
        profile.AddCustomisations(preorderposes, "mannequinPose", CustomisationSource.DEFAULT);

        if (!profileHelper.PlayerHasReceivedMaxNumberOfGift(sessionId, "shutupandtakemyroubles", 1))
        {
            profileHelper.FlagGiftReceivedInProfile(sessionId, "shutupandtakemyroubles", 1);
            mailSendService.SendSystemMessageToPlayer(sessionId, $"Preorder rewards sent! Enjoy you filthy cheater!", new List<Item>
            {
                new()
                {
                    Id = "694d60308c8be1e26431c5ff",
                    Template = "68f8e04eae031982b00e7aaf",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d602a946b0452c231c5fd",
                    Template = "68f8ccbc4c78615ba6079b20",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d602121c3bf281131c5fb",
                    Template = "68f25be683ec644ebf046787",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d601bd1f081e1e531c5f9",
                    Template = "68f8ccb11cc53ada67036e27",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d601624fcecdddc31c5f7",
                    Template = "68f8ccf06a6869fe97026c3b",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                },
                new()
                {
                    Id = "694d60ac37f204c81a31c601",
                    Template = "68fb9480e6ad35a767066feb",
                    Upd = new Upd()
                    {
                        SpawnedInSession = true,
                        StackObjectsCount = 1
                    }
                }
            });
            rewardHelper.AddAchievementToProfile(profile, "6948990c05f4f91bdb9a56f3");
            rewardHelper.AddAchievementToProfile(profile, "6948996f192a4d4fc09a56f7");
        }
        return new ValueTask<string>(request.DialogId);
    }

    public string Command => "shutupandtakemyroubles";

    public string CommandHelp => "Usage: Receive all preorder/beta content";
}