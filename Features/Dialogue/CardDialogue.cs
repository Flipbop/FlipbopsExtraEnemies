using Nickel;
using System.Collections.Generic;

/*namespace Flipbop.EnemyPack2;

internal sealed class CardDialogue : BaseDialogue
{
	public CardDialogue() : base(locale => ModEntry.Instance.Package.PackageRoot.GetRelativeFile($"i18n/dialogue-card-{locale}.json").OpenRead())
	{
		var cleoDeck = ModEntry.Instance.CleoDeck.Deck;
		var cleoType = ModEntry.Instance.CleoCharacter.CharacterType;
		var newNodes = new Dictionary<IReadOnlyList<string>, StoryNode>();

		ModEntry.Instance.Helper.Events.OnModLoadPhaseFinished += (_, phase) =>
		{
			if (phase != ModLoadPhase.AfterDbInit)
				return;
			InjectStory(newNodes, [], [], NodeType.combat);
		};
		ModEntry.Instance.Helper.Events.OnLoadStringsForLocale += (_, e) => InjectLocalizations(newNodes, [], [], e);

		newNodes[["Played", "SmallRepairs"]] = new()
		{
			lookup = [$"Played::{new SmallRepairsCard().Key()}"],
			priority = true,
			oncePerRun = true,
			allPresent = [cleoType],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
			],
		};

		for (var i = 0; i < 3; i++)
			newNodes[["Played", "ApologizeNextLoop", i.ToString()]] = new()
			{
				lookup = [$"Played::{new ApologizeNextLoopCard().Key()}"],
				priority = true,
				oncePerRun = true,
				oncePerCombatTags = [$"Played::{new ApologizeNextLoopCard().Key()}"],
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["Played", "SeekerBarrage", i.ToString()]] = new()
			{
				lookup = [$"Played::{new SeekerBarrageCard().Key()}"],
				priority = true,
				oncePerRun = true,
				oncePerCombatTags = [$"Played::{new SeekerBarrageCard().Key()}"],
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};
	}
}*/
