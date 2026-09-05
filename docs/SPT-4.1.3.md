# Fix nested client detection in the Backport prepatcher on SPT 4.1.3

Targets **4.1.x-dev**, not the 4.0.13 main branch. The already-ported 2.0.1 development code is the base; this contribution is only the applied plugin-discovery fix.

## Problem and implementation

The prepatcher inferred a plugins directory from its assembly location. In the tested installation this did not match the actual nested plugin layout, so it skipped EEnvironmentUIType registration and the client emitted five incorrect-enum messages.

Use BepInEx.Paths.PluginPath, verify that directory exists, and search recursively for the exact WTT-ContentBackportClient.dll filename. Do not load the client assembly during prepatching, because doing so can resolve game types before enum modification finishes. Existing enum IDs and the absent-client guard are retained.

## Validation

The applied patcher built against the SPT 4.1.3 / BepInEx installation and was included in the setup used for the completed raid. Installed Backport assets came from c719336a341d133780bcdc009c05a6f07eef4d2d and were checked against their LFS SHA256 values. Assets are not reuploaded by this PR. No claim is made that this promotes the development branch to a stable release.

## Compatibility and evidence

Target: **SPT 4.1.3**, EFT **0.16.9.5.40743**. This is a source contribution for that environment, not a claim of compatibility with future SPT releases or Fika.

The tester successfully loaded Icebreaker, entered a raid and extracted. In subsequent feedback they confirmed the blowtorch, extraction and doors work, and Black Division bots appeared to behave normally after the SAIN fix. These are user-reported functional observations, not automated coverage of every encounter or performance benchmarks. Ten continuous hours, all quests and multiplayer have not been tested.

The migration used installed SPT 4.1.3 assemblies and these guides:
- https://wiki.sp-tushonka.com/en/modding/SPT_41_Modding/Server_413_Changes
- https://wiki.sp-tushonka.com/en/modding/SPT_41_Modding/client/Class_Name_Mappings
- https://wiki.sp-tushonka.com/en/modding/SPT_41_Modding/server/Mod_Web_Pages


## Contribution build checks

The publication working copies were built in Release against the installed SPT 4.1.3 assemblies with deployment disabled. Client/server builds passed for Icebreaker, MoreBotsAPI, BlackDiv and ManimalCSGas; the Backport prepatcher and DynamicMaps client also passed. Existing compiler warnings remain in several ports. Fika was not built as part of this contribution gate. These checks validate compilation, not untested gameplay.

The patcher project also accepts `-p:SPTPath=<installation-root>` for local references. Its existing post-build copy is now gated by `DeployToGame=true`.

The patcher project also accepts `-p:SPTPath=<installation-root>` for local references. Its existing post-build copy is now gated by `DeployToGame=true`.
