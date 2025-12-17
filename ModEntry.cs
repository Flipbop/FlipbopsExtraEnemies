using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using Nickel.Common;
using Shockah.Kokoro;
using System;
using System.Collections.Generic;
using System.Linq;

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


	public IModHelper helper { get; }
	

	internal static IReadOnlyList<Type> CommonCardTypes { get; } = [
		
	];

	internal static IReadOnlyList<Type> UncommonCardTypes { get; } = [
		
	];

	internal static IReadOnlyList<Type> RareCardTypes { get; } = [
		
	];

	internal static IReadOnlyList<Type> SpecialCardTypes { get; } = [

	];

	internal static IEnumerable<Type> AllCardTypes { get; }
		= [..CommonCardTypes, ..UncommonCardTypes, ..RareCardTypes, ..SpecialCardTypes];

	internal static IReadOnlyList<Type> CommonArtifacts { get; } = [
		
	];

	internal static IReadOnlyList<Type> BossArtifacts { get; } = [
		
	];
	
	internal static IReadOnlyList<Type> EnemyTypes { get; } =
	[
		//typeof(),
		
	];
	
	internal static IEnumerable<Type> AllArtifactTypes
		=> [..CommonArtifacts, ..BossArtifacts];

	internal static readonly IEnumerable<Type> RegisterableTypes
		= [..AllCardTypes, ..AllArtifactTypes];
	

	public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
	{
		this.helper = helper;
		

		Instance = this;
		Harmony = helper.Utilities.Harmony;
		KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!.V2;

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
		
	}

	public override object? GetApi(IModManifest requestingMod)
		=> new ApiImplementation();

	internal static Rarity GetCardRarity(Type type)
	{
		if (RareCardTypes.Contains(type))
			return Rarity.rare;
		if (UncommonCardTypes.Contains(type))
			return Rarity.uncommon;
		return Rarity.common;
	}

	internal static ArtifactPool[] GetArtifactPools(Type type)
	{
		if (BossArtifacts.Contains(type))
			return [ArtifactPool.Boss];
		if (CommonArtifacts.Contains(type))
			return [ArtifactPool.Common];
		return [];
	}
}
