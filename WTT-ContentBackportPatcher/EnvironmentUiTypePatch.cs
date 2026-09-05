using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using Mono.Cecil;
using FieldAttributes = Mono.Cecil.FieldAttributes;

namespace ContentBackportPatcher
{
    public static class EnvironmentUiTypePatch
    {
        private const string LogSourceName = "Content Backport PrePatch";

        public static IEnumerable<string> TargetDLLs { get; } = new[]
        {
            "Assembly-CSharp.dll"
        };

        public static void Patch(ref AssemblyDefinition assembly)
        {
            var logger = Logger.CreateLogSource(LogSourceName);

            try
            {
                bool shouldPatch = ShouldPatchAssembly(logger);
                if (!shouldPatch)
                {
                    logger.LogWarning("Plugin missing, not patching assembly. Mod is either not properly installed, or not properly uninstalled.");
                    return;
                }

                var enumType = assembly.MainModule.GetType("EFT.EEnvironmentUIType");
                if (enumType == null)
                {
                    logger.LogError("Failed to find EFT.EEnvironmentUIType");
                    return;
                }

                AddEnumValueIfMissing(enumType, "MiamiEnvironmentUiType", 6, logger);
                AddEnumValueIfMissing(enumType, "EndingAndSurvivedEnvironmentUiType", 7, logger);
                AddEnumValueIfMissing(enumType, "EndingDidntEscapeFromYourselfEnvironmentUiType", 8, logger);
                AddEnumValueIfMissing(enumType, "EndingForHumanityEnvironmentUiType", 9, logger);
                AddEnumValueIfMissing(enumType, "EndingInTheDarknessEnvironmentUiType", 10, logger);
            }
            catch (Exception ex)
            {
                logger.LogError($"EnvironmentUiTypePatch failed: {ex}");
            }
        }

        private static void AddEnumValueIfMissing(TypeDefinition enumType, string name, int value, ManualLogSource logger)
        {
            var existing = enumType.Fields.FirstOrDefault(f => f.Name == name);
            if (existing != null)
            {
                return;
            }

            var field = new FieldDefinition(
                name,
                FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.HasDefault,
                enumType)
            {
                Constant = value
            };

            enumType.Fields.Add(field);
        }

        private static bool ShouldPatchAssembly(ManualLogSource logger)
        {
            // A prepatcher is loaded from BepInEx/patchers, which is not a stable
            // base for resolving a plugin's install directory. BepInEx already
            // supplies the correct plugins root; nested mod folders are valid.
            // Check only the exact client DLL name without loading its assembly:
            // loading it here could resolve game types before enum patching ends.
            return Directory.Exists(BepInEx.Paths.PluginPath)
                && Directory.EnumerateFiles(BepInEx.Paths.PluginPath,
                    "WTT-ContentBackportClient.dll", SearchOption.AllDirectories).Any();
        }
    }
}
