using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using WTTServerCommonLib.Helpers;

namespace WTTContentBackport.Helpers
{
    [Injectable]
    public class BackportQuestHelper(DatabaseService  databaseService, ISptLogger<BackportQuestHelper> logger, QuestHelper questHelper)
    {

        // Define weapon IDs
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo

        // Knives

        private const string KABAR_FIGHTER = "6a3935772db31ca1c800fe26";
        private const string JAGD = "6a39358c658f5889ba050ef3";
        private const string MILLER_M2_BROWN = "68d16e9f2711340d6b0d4786";
        private const string FINKA = "68f244ae22c8979ee308f4d8";

        // Assault Carbine
        private const string ASVAL_MOD4 = "6871284e9a353bb50606f3ed";
        
        // Assault Rifle
        private const string RADIAN_MODEL1 = "6895bb82c4519957df062f82";
        private const string M16A1 = "68a639748e1fe612970728e9";
        private const string NL545 = "68c2940aecc41cc5490bd40e";
        private const string NL545_DI = "68c16b9ab6b75a8a480520a6";
        private const string AK308 = "689166b6c2d6fa42e7044756";
        private const string M16A2 = "68a6399922b1e0bd360afe56";
        private const string M110 = "6932abeb5403890d0c09c926";
        private const string HK416A5 = "6a15ae2ae5267ba21c07f98f";
        private const string HOWATYPE20 = "6a3bffbebc377285900b85cf";
        private const string QBZ191 = "69f9ebbcaae020b0db02f65d";

        // Marksman Rifle
        private const string TKPD = "68aee763130c00663d08aea8";
        private const string SR25_TAUPE = "6932abeb5403890d0c09c926";
        
        // Sniper Rifle
        private const string MXLR = "67c6de3ce39861860909e8e5";

        // Shotguns
        private const string saiga_12g_redline = "6981d72ed009ad83920da43a";

        // Armor (and Armored Rigs)
        private const string armor_6B45 = "68948a95d8f2b85fb705e2a6";
        private const string armor_6B45_armoredrig = "68948ad72c87773b9f06d73f";
        private const string armor_6B45_armoredrigAssault = "68948b118c57a8a52301d7ae";
        private const string armor_6B45_armoredrigMedic = "68948aebd8f2b85fb705e2b0";
        private const string armor_OTV_woodland = "68a89942431252e29a02dbf6";
        private const string armor_THOR_CRL = "68a89146212dbbeead0d5636";
        private const string armor_FCPC_BD = "689479cb47e5acd1e10be986";
        private const string armor_Siege_R_BD = "68947a4be4bf255d1b0ca746";
        private const string armor_LV119_BD = "689479eb30cc5ba7be00f5ff";
        private const string armor_LV119_BD1 = "689479a4a733b1602007e2eb";
        private const string armor_SPPCV2 = "68a99207aa809946e507c2f6";
        private const string armor_KlASS_Kamysh = "68a97093431252e29a02dc06";
        private const string armor_Strandhogg_ABUPAT = "68a85ab8ef22d08bf401fa68";
        private const string armor_ana_m2 = "69412e5573dcf473e50be464";
        private const string armor_tak_kek_jaypc_od = "693fd13aa490096a05028cc8";
        private const string armor_tak_kek_jaypc_b = "693fd1200ec97e98040bd3f9";
        private const string armor_crye_jpc = "693fd0e9deee848f70054999";
        private const string armor_gladiator_lightweight_multicam = "69d28cdc274c032dd804afe0";
        private const string armor_gladiator_deathisinevitable = "69d26ff4b855150a70092b8c";
        private const string armor_strandhogg_multicam_black = "69d36347705756116e0a901c";
        private const string armor_gladiator_lightweight_multicam_2 = "69d27ebeb855150a70092ba3";
        private const string armor_stich_profi_atacs_fg = "69cfff5920eac4535609a008";
        private const string armor_wartech_tv115_black = "69b10ebfde4dda4a140bddb8";
        private const string armor_stich_profi_coyote = "69cfff99cb69530af90a8279";
        private const string armor_fort_defender_flecktarn = "69b11935f3783ec37c03a105";
        private const string armor_fort_redut_m_black = "69cfef0d6242b966d40803e7";
        private const string armor_fort_gladiator_gray = "69d3708c8d8009073d0a9df4";
        private const string armor_fort_redut_m_prisoner = "69cf9696b96c8e8d3e002925";
        private const string armor_spritus_lv119_multicam_black_wedge = "69e2441a18cb3157560855ec";



        // Helmets
        private const string helmet_Vulkan5_FLAME = "68a98762609a5cb2120ebd26";
        private const string helmet_Vulkan5_CAMO = "68a987a1609a5cb2120ebd2f";
        private const string helmet_Champion = "68bee28f79c8186398098e6f";
        private const string helmet_neosteel_aces = "68a9beecbba00932ed0bc256";
        private const string helmet_ulach_sand = "68bee2ccd6da72c13f03db95";
        private const string helmet_galvion_mutualist = "68a9b5a5863d2a71fa0494a6";
        private const string helmet_Caiman_MultiCam = "68a9b3ca0a9c4f9398032c46";
        private const string helmet_Ronin_Respirator_green = "68a9a92b838d65bcb3050176";
        private const string helmet_Ronin_Respirator_beast = "68a9a85c3e1ee5a70504c12e";
        private const string helmet_NeoSteel_Orange = "68a9be93f260f4e1c2038686";
        private const string helmet_hardhat_white = "68a6d96fddf0111c2f04c9c9";
        private const string helmet_ULACH_greenstripes = "68bee2d9af253218c00ebbb4";
        private const string helmet_hardhat_orange = "68a6d95addf0111c2f04c9c3";
        private const string helmet_ULACH_meshspray = "68bee2e0ede5c8489f08e1b5";
        private const string helmet_LShZ5_Eightball = "68a986ca5c0073fa2d0d8cb8";
        private const string helmet_ULACH_coyotestripe = "68bee2e876e02b9e340ef113";
        private const string helmet_galvion_caiman_multicam_alpine = "693be003582cc8870b090b41";
        private const string helmet_ulach_wintermesh = "693be8f650fafa102607aed4";
        private const string helmet_crye_precision_airframe_od_green = "69cbfe34c293038df7002963";
        private const string helmet_atlant_armour_titan_aramid_multicam = "69ce70df606696c00301a760";
        private const string helmet_atlant_armour_titan_aramid_od_green = "69ce70a321c1fc42cd01affa";
        private const string helmet_wendy_exfil_multicam = "69c26722bf4ff19f50057643";
        private const string helmet_atlant_armour_titan_aramid_rudiarius = "69ce70ff49b447e32200cc1c";
        private const string helmet_crye_precision_airframe_mlok_bull = "69ce47ca49b447e32200cbe9";
        private const string helmet_crye_precision_airframe_oldschool = "69ce47f01bb66daf5b0d62fd";
        private const string helmet_crye_precision_airframe_mesh_green = "69cbfe1f6dbaa9badb0c6b13";
        private const string helmet_crye_precision_airframe_shark = "69cbfe3a2f657c59da06e262";
        private const string helmet_crye_precision_mlok_black = "69cd423343c6b278a2076cca";
        private const string helmet_highcom_striker_achhc_coyote = "69c26fa8add25b3623091e89";
        private const string helmet_crye_precision_airframe_tan = "69cbfe44897389c1870b2337";


        // Facecovers (armored)

        private const string facecover_Atomic_ping = "688b3bfa1ed594eccd0c45ee";
        private const string facecover_deathshadow_gold = "68a9b821cdf661cc5a0626c6";
        private const string facecover_Samurai_menpo = "68bee22e79c8186398098e6d";
        private const string facecover_Samurai_menpo_gold = "68bee238a48c3c320808abc4";
        private const string facecover_Atomic_toxic = "68a9a0b01696fb8c1e0ee9cc";
        private const string facecover_Samurai_menpo_white = "68bee246ede5c8489f08e1b3";
        private const string facecover_deathshadow_gray = "68a9b852cdf661cc5a0626c9";
        private const string facecover_Atomic_crashtested = "68a9a1223e1ee5a70504c126";
        private const string facecover_deathshadow_white = "68a9b873a4b28d56c80a1818";
        private const string facecover_Atomic_LouiPeeton4 = "68d54d0525ac8590a8075ac3";
        private const string facecover_Atomic_LouiPeeton3 = "68a9a04373d52d47830759c7";
        private const string facecover_Atomic_demonic = "68a9a15d73d52d47830759c9";
        private const string facecover_Atomic_blastedice = "6936ff8734029a096c06f95a";
        
        // Gas masks

        private const string facecover_gasmask_avon_m53a1 = "689b880fff8b4adc420f5b56";
        
        public void ModifyQuests()
        {
            var quests = databaseService.GetTemplates().Quests;

            // ReSharper disable CommentTypo
            // ====================== PRAPOR QUESTS ======================
            
            // Our Own Land (6179b5b06e9dd54ac275e409)
            questHelper.AddDogtagsToQuests(quests, "6179b5b06e9dd54ac275e409", BackportJunkDisabler._usecDogtags, "USEC");
            
            // Test Drive Part 1 (5c0bd94186f7747a727f09b2)
            questHelper.AddWeaponsToKillCondition(quests, "5c0bd94186f7747a727f09b2", [SR25_TAUPE]);
            
            // Punisher Part 4 (59ca264786f77445a80ed044)
            questHelper.AddWeaponsToKillCondition(quests, "59ca264786f77445a80ed044", [saiga_12g_redline]);
            
            // Punisher Part 6 (59ca2eb686f77445a80ed049)
            //questHelper.AddWeaponsToKillCondition(quests, "59ca2eb686f77445a80ed049", []);
            questHelper.AddDogtagsToQuests(quests, "59ca2eb686f77445a80ed049", BackportJunkDisabler._bearDogtags, "BEAR");
            questHelper.AddDogtagsToQuests(quests, "59ca2eb686f77445a80ed049", BackportJunkDisabler._usecDogtags, "USEC");

            // Mall Cop (64e7b99017ab941a6f7bf9d7)
            //questHelper.AddWeaponsToKillCondition(quests, "64e7b99017ab941a6f7bf9d7", []);

            // Tickets, Please (64e7b9a4aac4cd0a726562cb)
            //questHelper.AddWeaponsToKillCondition(quests, "64e7b9a4aac4cd0a726562cb", []);

            // District Patrol (64e7b9bffd30422ed03dad38)
            questHelper.AddWeaponsToKillCondition(quests, "64e7b9bffd30422ed03dad38", [
                ASVAL_MOD4, RADIAN_MODEL1, M16A1, NL545, NL545_DI, AK308, M16A2, HK416A5, HOWATYPE20, QBZ191
            ]);

            // ====================== SKIER QUESTS ======================

            // Friend From The West Part 1 (5a27c99a86f7747d2c6bdd8e)
            questHelper.AddDogtagsToQuests(quests, "5a27c99a86f7747d2c6bdd8e", BackportJunkDisabler._usecDogtags, "USEC");
            
            // Stirrup (596b455186f77457cb50eccb)
            //questHelper.AddWeaponsToKillCondition(quests, "596b455186f77457cb50eccb", []);

            // Silent Caliber (5c0bc91486f7746ab41857a2)
            questHelper.AddWeaponsToKillCondition(quests, "5c0bc91486f7746ab41857a2", [saiga_12g_redline]);

            // Setup (5c1234c286f77406fa13baeb)
            //questHelper.AddWeaponsToKillCondition(quests, "5c1234c286f77406fa13baeb", []);

            // Connections Up North (6764174c86addd02bc033d68)
            questHelper.AddWeaponsToKillCondition(quests, "6764174c86addd02bc033d68", [
                MXLR
            ]);
            
            // ====================== FENCE QUESTS ======================
            // Compensation For Damage Trust (61e6e5e0f5b9633f6719ed95)
            questHelper.AddDogtagsToQuests(quests, "61e6e5e0f5b9633f6719ed95", BackportJunkDisabler._bearDogtags, "BEAR");
            questHelper.AddDogtagsToQuests(quests, "61e6e5e0f5b9633f6719ed95", BackportJunkDisabler._usecDogtags, "USEC");

            // ====================== PEACEKEEPER QUESTS ======================
            
            // Counteraction (6179b5eabca27a099552e052)
            questHelper.AddDogtagsToQuests(quests, "6179b5eabca27a099552e052", BackportJunkDisabler._bearDogtags, "BEAR");
            
            // Trophies (60e71ccb5688f6424c7bfec4)
            questHelper.AddDogtagsToQuests(quests, "60e71ccb5688f6424c7bfec4", BackportJunkDisabler._usecDogtags, "USEC");
            questHelper.AddDogtagsToQuests(quests, "60e71ccb5688f6424c7bfec4", BackportJunkDisabler._bearDogtags, "BEAR");
            
            // The Punisher Harvest (655e427b64d09b4122018228)
            questHelper.AddDogtagsToQuests(quests, "655e427b64d09b4122018228", BackportJunkDisabler._usecDogtags, "USEC");
            questHelper.AddDogtagsToQuests(quests, "655e427b64d09b4122018228", BackportJunkDisabler._bearDogtags, "BEAR");

            // Spa Tour Part 1 (5a03153686f77442d90e2171)
            questHelper.AddWeaponsToKillCondition(quests, "5a03153686f77442d90e2171", [saiga_12g_redline]);

            // Worst Job (63a9b229813bba58a50c9ee5)
            questHelper.AddWeaponsToKillCondition(quests, "63a9b229813bba58a50c9ee5", [
                M16A2, M16A1, RADIAN_MODEL1
            ]);

            // ====================== JAEGER QUESTS ======================

            var allArmors = new[]
            {
                armor_ana_m2, armor_tak_kek_jaypc_b, armor_tak_kek_jaypc_od, armor_crye_jpc, armor_6B45, armor_6B45_armoredrig, armor_6B45_armoredrigAssault, armor_6B45_armoredrigMedic, armor_OTV_woodland, armor_THOR_CRL, armor_FCPC_BD, armor_Siege_R_BD, armor_LV119_BD, armor_LV119_BD1, armor_SPPCV2, armor_KlASS_Kamysh, armor_Strandhogg_ABUPAT,     armor_gladiator_lightweight_multicam,
    armor_gladiator_deathisinevitable,
    armor_strandhogg_multicam_black,
    armor_gladiator_lightweight_multicam_2,
    armor_stich_profi_atacs_fg,
    armor_wartech_tv115_black,
    armor_stich_profi_coyote,
    armor_fort_defender_flecktarn,
    armor_fort_redut_m_black,
    armor_fort_gladiator_gray,
    armor_fort_redut_m_prisoner,
    armor_spritus_lv119_multicam_black_wedge
            };

            var allHelmets = new[]
            {
                helmet_galvion_caiman_multicam_alpine, helmet_ulach_wintermesh, helmet_Vulkan5_FLAME, helmet_Vulkan5_CAMO, helmet_Champion, helmet_neosteel_aces, helmet_ulach_sand, helmet_galvion_mutualist, helmet_Caiman_MultiCam, helmet_Ronin_Respirator_beast, helmet_Ronin_Respirator_green, helmet_NeoSteel_Orange, helmet_hardhat_white, helmet_ULACH_greenstripes, helmet_hardhat_orange, helmet_ULACH_meshspray, helmet_LShZ5_Eightball, helmet_ULACH_coyotestripe,     helmet_crye_precision_airframe_od_green,
    helmet_atlant_armour_titan_aramid_multicam,
    helmet_atlant_armour_titan_aramid_od_green,
    helmet_wendy_exfil_multicam,
    helmet_atlant_armour_titan_aramid_rudiarius,
    helmet_crye_precision_airframe_mlok_bull,
    helmet_crye_precision_airframe_oldschool,
    helmet_crye_precision_airframe_mesh_green,
    helmet_crye_precision_airframe_shark,
    helmet_crye_precision_mlok_black,
    helmet_highcom_striker_achhc_coyote,
    helmet_crye_precision_airframe_tan
            };

            var allArmoredFaceCovers = new[]
            {
                facecover_Atomic_blastedice, facecover_Atomic_crashtested, facecover_Atomic_demonic, facecover_Atomic_LouiPeeton3, facecover_Atomic_LouiPeeton4, facecover_Atomic_ping, facecover_Atomic_toxic, facecover_deathshadow_gold, facecover_deathshadow_gray, facecover_deathshadow_white, facecover_Samurai_menpo, facecover_Samurai_menpo_gold, facecover_Samurai_menpo_white
            };

            var allKnives = new[] {
                MILLER_M2_BROWN, FINKA, JAGD, KABAR_FIGHTER
            };

            // Tarkov Shooter Part 1-8
            questHelper.AddWeaponsToKillCondition(quests, "5bc4776586f774512d07cf05", [MXLR]); // Part 1
            questHelper.AddWeaponsToKillCondition(quests, "5bc479e586f7747f376c7da3", [MXLR]); // Part 2
            questHelper.AddWeaponsToKillCondition(quests, "5bc47dbf86f7741ee74e93b9", [MXLR]); // Part 3
            questHelper.AddWeaponsToKillCondition(quests, "5bc480a686f7741af0342e29", [MXLR]); // Part 4
            questHelper.AddWeaponsToKillCondition(quests, "5bc4826c86f774106d22d88b", [MXLR]); // Part 5
            questHelper.AddWeaponsToKillCondition(quests, "5bc4836986f7740c0152911c", [MXLR]); // Part 6
            questHelper.AddWeaponsToKillCondition(quests, "5bc4856986f77454c317bea7", [MXLR]); // Part 7
            questHelper.AddWeaponsToKillCondition(quests, "5bc4893c86f774626f5ebf3e", [MXLR]); // Part 8

            // Claustrophobia (669fa3979b0ce3feae01a130)
            questHelper.AddWeaponsToKillCondition(quests, "669fa3979b0ce3feae01a130", [saiga_12g_redline]);

            // Slaughterhouse (63a9b36cc31b00242d28a99f)
            questHelper.AddWeaponsToKillCondition(quests, "63a9b36cc31b00242d28a99f", allKnives);

            // ====================== MECHANIC QUESTS ======================

            // Psycho Sniper (5c0be13186f7746f016734aa)
            questHelper.AddWeaponsToKillCondition(quests, "5c0be13186f7746f016734aa", [
                MXLR
            ]);

            // Shooter Born in Heaven (5c0bde0986f77479cf22c2f8)
            questHelper.AddWeaponsToKillCondition(quests, "5c0bde0986f77479cf22c2f8", [
                MXLR
            ]);

            // Make Amends Equipment (6261482fa4eb80027c4f2e11)
            //questHelper.AddWeaponsToFindOrHandoverCondition(quests, "6261482fa4eb80027c4f2e11", []);
            
            // Survivalist Path Unprotected but Dangerous (5d25aed386f77442734d25d2)
            questHelper.AddArmorToEquipmentExclusive(quests, "5d25aed386f77442734d25d2", allArmors);
            
            // Swift One (60e729cf5698ee7b05057439)
            questHelper.AddArmorToEquipmentExclusive(quests, "60e729cf5698ee7b05057439", allArmors);
            questHelper.AddArmorToEquipmentExclusive(quests, "60e729cf5698ee7b05057439", allHelmets);
            questHelper.AddArmorToEquipmentExclusive(quests, "60e729cf5698ee7b05057439", allArmoredFaceCovers);
            
            // All Western Items
            var allWesternWeapons = new[]
            {
                AK308, M110
            };
            // Import Control
            questHelper.AddWeaponsToFindOrHandoverCondition(quests, "668bcccc167d507eb01a268b", allWesternWeapons);

            // Gloves Off (67040c5b4ac6d9c18c0ade26)
            questHelper.AddWeaponsToKillCondition(quests, "67040c5b4ac6d9c18c0ade26", [saiga_12g_redline]);


            // ====================== THERAPIST QUESTS ======================

            // Decontamination Services (5c0d1c4cd0928202a02a6f5c)
            questHelper.AddArmorToEquipmentExclusive(quests, "5c0d1c4cd0928202a02a6f5c", [facecover_gasmask_avon_m53a1]);

        }
    }
}
