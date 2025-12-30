using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using Nickel.Common;
using Shockah.Kokoro;
using System;
using System.Collections.Generic;
using System.Linq;
using Flipbop.BOAF;

namespace Flipbop.EnemyPack2;

public sealed class ModEntry : SimpleMod
{
	internal static ModEntry Instance { get; private set; } = null!;
	internal readonly IEnemyPack2Api Api = new ApiImplementation();

	internal IHarmony Harmony { get; }
	internal IKokoroApi.IV2 KokoroApi { get; }

	internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
	internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }
	internal List<IModSettingsApi.IModSetting> SettingsEntries = [];

	internal ModSettings ModSettings = new();

	internal INonPlayableCharacterEntryV2 FishBreathCharacter { get; }
	internal INonPlayableCharacterEntryV2 RevCharacter { get; }


	public IModHelper helper { get; }
	

	internal static IReadOnlyList<Type> SpecialCardTypes { get; } = [

	];
	internal static IReadOnlyList<Type> CommonArtifacts { get; } = [
		
	];
	
	internal static IReadOnlyList<Type> MidrowObjects { get; } =
	[
		typeof(FishingHook),
		
	];
	
	internal static IReadOnlyList<Type> EnemyTypes { get; } =
	[
		typeof(FishGuyEnemy),
		
	];

	internal static readonly IEnumerable<Type> RegisterableTypes
		= [..SpecialCardTypes, ..CommonArtifacts, ..MidrowObjects];
	

	public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
	{
		this.helper = helper;
		

		Instance = this;
		Harmony = helper.Utilities.Harmony;
		KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!.V2;
		ModSettings = helper.Storage.LoadJson<ModSettings>(helper.Storage.GetMainStorageFile("json"));

		helper.Events.OnModLoadPhaseFinished += (_, phase) =>
		{
			if (phase != ModLoadPhase.AfterDbInit)
				return;
			foreach (Type type in EnemyTypes) {
				AccessTools.DeclaredMethod(type, nameof(IRegisterableEnemy.Register))?.Invoke(null, [helper]);
			}

		};

		this.AnyLocalizations = new JsonLocalizationProvider(
			tokenExtractor: new SimpleLocalizationTokenExtractor(),
			localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/main-{locale}.json").OpenRead()
		);
		this.Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
			new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(this.AnyLocalizations)
		);
		
		foreach (var registerableType in RegisterableTypes)
			AccessTools.DeclaredMethod(registerableType, nameof(IRegisterable.Register))?.Invoke(null, [package, helper]);
		
		FishBreathCharacter = helper.Content.Characters.V2.RegisterNonPlayableCharacter("FishBreath", new NonPlayableCharacterConfigurationV2()
		{
			CharacterType = "fishguy",
			Name = AnyLocalizations.Bind(["character", "fishguy"]).Localize,
			
		});
		helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
		{
			CharacterType = FishBreathCharacter.CharacterType,
			LoopTag = "neutral",
			Frames = Enumerable.Range(0, 5)
				.Select(i =>
					helper.Content.Sprites
						.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/FishGuy/{i}.png")).Sprite)
				.ToList()
		});
		RevCharacter = helper.Content.Characters.V2.RegisterNonPlayableCharacter("Rev", new NonPlayableCharacterConfigurationV2()
		{
			CharacterType = "rev",
			Name = AnyLocalizations.Bind(["character", "rev"]).Localize,
			
		});
		helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
		{
			CharacterType = RevCharacter.CharacterType,
			LoopTag = "neutral",
			Frames = Enumerable.Range(0, 5)
				.Select(i =>
					helper.Content.Sprites
						.RegisterSprite(package.PackageRoot.GetRelativeFile($"assets/Character/FishGuy/{i}.png")).Sprite)
				.ToList()
		});

		_ = new RevDialogue();
		SetUpModSettings(helper);
	}

	public override object? GetApi(IModManifest requestingMod)
		=> new ApiImplementation();

	internal static Rarity GetCardRarity(Type type)
	{
		return Rarity.common;
	}

	internal static ArtifactPool[] GetArtifactPools(Type type)
	{
		if (CommonArtifacts.Contains(type))
			return [ArtifactPool.Common];
		return [];
	}
	private void SetUpModSettings(IModHelper helper) {
		if (helper.ModRegistry.GetApi<IModSettingsApi>("Nickel.ModSettings") is { } settingsApi) {
			settingsApi.RegisterModSettings(settingsApi.MakeList(SettingsEntries)
				.SubscribeToOnMenuClose(_ => {
					helper.Storage.SaveJson(helper.Storage.GetMainStorageFile("json"), ModSettings);
				}));
		}
	}
}
