using Nickel;
using System.Collections.Generic;

/*namespace Flipbop.EnemyPack2;

internal sealed class CombatDialogue : BaseDialogue
{
	public CombatDialogue() : base(locale => ModEntry.Instance.Package.PackageRoot.GetRelativeFile($"i18n/dialogue-combat-{locale}.json").OpenRead())
	{
		var cleoDeck = ModEntry.Instance.CleoDeck.Deck;
		var cleoType = ModEntry.Instance.CleoCharacter.CharacterType;
		var newNodes = new Dictionary<IReadOnlyList<string>, StoryNode>();
		var saySwitchNodes = new Dictionary<IReadOnlyList<string>, Say>();

		ModEntry.Instance.Helper.Events.OnModLoadPhaseFinished += (_, phase) =>
		{
			if (phase != ModLoadPhase.AfterDbInit)
				return;
			InjectStory(newNodes, [], saySwitchNodes, NodeType.combat);
		};
		ModEntry.Instance.Helper.Events.OnLoadStringsForLocale += (_, e) => InjectLocalizations(newNodes, [], saySwitchNodes, e);

		#region TookDamage
		for (var i = 0; i < 3; i++)
			newNodes[["TookDamage", "Basic", i.ToString()]] = new()
			{
				enemyShotJustHit = true,
				minDamageDealtToPlayerThisTurn = 1,
				lines = [
					new Say { who = cleoType, loopTag = "squint" },
				],
			};

		newNodes[["TookDamage", "Dizzy"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [cleoType, Deck.dizzy.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "squint" },
				new Say { who = Deck.dizzy.Key(), loopTag = "intense" },
			],
		};
		newNodes[["TookDamage", "Riggs"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [cleoType, Deck.riggs.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "squint" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Isaac"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [cleoType, Deck.goat.Key()],
			lines = [
				new Say { who = Deck.goat.Key(), loopTag = "squint" },
				new Say { who = cleoType, loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Drake"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [cleoType, Deck.eunice.Key()],
			lines = [
				new Say { who = Deck.eunice.Key(), loopTag = "squint" },
				new Say { who = cleoType, loopTag = "squint" },
			],
		};
		newNodes[["TookDamage", "Max"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [cleoType, Deck.hacker.Key()],
			lines = [
				new Say { who = Deck.hacker.Key(), loopTag = "mad" },
				new Say { who = cleoType, loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Books"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [cleoType, Deck.shard.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "squint" },
				new Say { who = Deck.shard.Key(), loopTag = "intense" },
			],
		};
		newNodes[["TookDamage", "CAT"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [cleoType, "comp"],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
				new Say { who = "comp", loopTag = "grumpy" },
			],
		};
		#endregion

		for (var i = 0; i < 1; i++)
			newNodes[["TookNonHullDamage", "Basic", i.ToString()]] = new StoryNode()
			{
				enemyShotJustHit = true,
				maxDamageDealtToPlayerThisTurn = 0,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		#region DealtDamage
		for (var i = 0; i < 4; i++)
			newNodes[["DealtDamage", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageDealtToEnemyThisTurn = 1,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		newNodes[["DealtDamage", "Dizzy"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = cleoDeck,
			allPresent = [cleoType, Deck.dizzy.Key()],
			lines = [
				new Say { who = Deck.dizzy.Key(), loopTag = "neutral" },
				new Say { who = cleoType, loopTag = "explain" },
			],
		};
		newNodes[["DealtDamage", "Riggs"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			allPresent = [cleoType, Deck.riggs.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["DealtDamage", "Peri"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = cleoDeck,
			allPresent = [cleoType, Deck.peri.Key()],
			lines = [
				new Say { who = Deck.peri.Key(), loopTag = "neutral" },
				new Say { who = cleoType, loopTag = "explain" },
			],
		};
		newNodes[["DealtDamage", "Isaac"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = cleoDeck,
			allPresent = [cleoType, Deck.goat.Key()],
			lines = [
				new Say { who = Deck.goat.Key(), loopTag = "neutral" },
				new Say { who = cleoType, loopTag = "neutral" },
			],
		};
		newNodes[["DealtDamage", "Drake"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			allPresent = [cleoType, Deck.eunice.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "explain" },
				new Say { who = Deck.eunice.Key(), loopTag = "sly" },
			],
		};
		newNodes[["DealtDamage", "Max"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			allPresent = [cleoType, Deck.hacker.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
				new Say { who = Deck.hacker.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["DealtDamage", "Books"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			allPresent = [cleoType, Deck.shard.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
				new Say { who = Deck.shard.Key(), loopTag = "blush" },
			],
		};
		newNodes[["DealtDamage", "CAT"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			allPresent = [cleoType, "comp"],
			lines = [
				new Say { who = "comp", loopTag = "smug" },
				new Say { who = cleoType, loopTag = "neutral" },
			],
		};
		#endregion

		for (var i = 0; i < 3; i++)
			newNodes[["DealtBigDamage", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageDealtToEnemyThisTurn = 6,
				whoDidThat = cleoDeck,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["ShieldedDamage", "Basic", i.ToString()]] = new StoryNode()
			{
				enemyShotJustHit = true,
				maxDamageDealtToPlayerThisTurn = 0,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			}.SetMinShieldLostThisTurn(1);

		newNodes[["Missed", "Basic", "0"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [cleoType],
			lines = [
				new Say { who = cleoType, loopTag = "squint" },
			],
		};
		newNodes[["Missed", "Basic", "1"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [cleoType],
			lines = [
				new Say { who = cleoType, loopTag = "squint" },
			],
		};
		newNodes[["Missed", "Basic", "2"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [cleoType],
			lines = [
				new Say { who = cleoType, loopTag = "squint" },
			],
		};

		#region AboutToDie
		newNodes[["AboutToDie", "Basic", "0"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType],
			lines = [
				new Say { who = cleoType, loopTag = "nervous" },
			],
		};
		newNodes[["AboutToDie", "Basic", "1"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType],
			lines = [
				new Say { who = cleoType, loopTag = "nervous" },
			],
		};
		newNodes[["AboutToDie", "Basic", "2"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
			],
		};

		newNodes[["AboutToDie", "Dizzy"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType, Deck.dizzy.Key()],
			lines = [
				new Say { who = Deck.dizzy.Key(), loopTag = "nervous" },
				new Say { who = cleoType, loopTag = "nervous" },
			],
		};
		newNodes[["AboutToDie", "Riggs"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType, Deck.riggs.Key()],
			lines = [
				new Say { who = Deck.riggs.Key(), loopTag = "nervous" },
				new Say { who = cleoType, loopTag = "neutral" },
			],
		};
		newNodes[["AboutToDie", "Peri"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType, Deck.peri.Key()],
			lines = [
				new Say { who = Deck.peri.Key(), loopTag = "mad" },
				new Say { who = cleoType, loopTag = "nervous" },
			],
		};
		newNodes[["AboutToDie", "Isaac"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType, Deck.goat.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "nervous" },
				new Say { who = Deck.goat.Key(), loopTag = "sad" },
			],
		};
		newNodes[["AboutToDie", "Drake"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType, Deck.eunice.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "squint" },
				new Say { who = Deck.eunice.Key(), loopTag = "mad" },
			],
		};
		newNodes[["AboutToDie", "Books"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType, Deck.shard.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "squint" },
				new Say { who = Deck.shard.Key(), loopTag = "squint" },
			],
		};
		newNodes[["AboutToDie", "CAT"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [cleoType, "comp"],
			lines = [
				new Say { who = cleoType, loopTag = "nervous" },
				new Say { who = "comp", loopTag = "mad" },
			],
		};
		#endregion

		for (var i = 0; i < 1; i++)
			newNodes[["HitArmor", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageBlockedByEnemyArmorThisTurn = 1,
				oncePerCombat = true,
				oncePerRun = true,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["ExcessEnergy", "Basic", i.ToString()]] = new()
			{
				handEmpty = true,
				minEnergy = 1,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["EmptyHand", "Basic", i.ToString()]] = new()
			{
				handEmpty = true,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["TrashHand", "Basic", i.ToString()]] = new()
			{
				handFullOfTrash = true,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["StartedBattle", "Basic", i.ToString()]] = new()
			{
				turnStart = true,
				maxTurnsThisCombat = 1,
				oncePerCombat = true,
				oncePerRun = true,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["NoOverlap", "Basic", i.ToString()]] = new()
			{
				priority = true,
				shipsDontOverlapAtAll = true,
				oncePerCombatTags = ["NoOverlapBetweenShips"],
				oncePerRun = true,
				nonePresent = ["crab", "scrap"],
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["NoOverlapButSeeker", "Basic", i.ToString()]] = new()
			{
				priority = true,
				shipsDontOverlapAtAll = true,
				oncePerCombatTags = ["NoOverlapBetweenShipsSeeker"],
				oncePerRun = true,
				anyDronesHostile = ["missile_seeker"],
				nonePresent = ["crab"],
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["LongFight", "Basic", i.ToString()]] = new()
			{
				minTurnsThisCombat = 9,
				oncePerCombatTags = ["manyTurns"],
				oncePerRun = true,
				turnStart = true,
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["ReturningFromMissing", "Basic", i.ToString()]] = new()
			{
				priority = true,
				lookup = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::ReturningFromMissing"],
				oncePerRun = true,
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		#region DealtDamage
		for (var i = 0; i < 2; i++)
			newNodes[["GoingToOverheat", "Basic", i.ToString()]] = new()
			{
				goingToOverheat = true,
				oncePerCombatTags = ["OverheatGeneric"],
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "squint" },
				],
			};

		newNodes[["GoingToOverheat", "Drake"]] = new()
		{
			goingToOverheat = true,
			oncePerCombatTags = ["OverheatGeneric"],
			allPresent = [cleoType, Deck.eunice.Key()],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
				new Say { who = Deck.eunice.Key(), loopTag = "sly" },
			],
		};
		#endregion

		for (var i = 0; i < 1; i++)
			newNodes[["Recalibrator", "Basic", i.ToString()]] = new()
			{
				playerShotJustMissed = true,
				hasArtifacts = ["Recalibrator"],
				allPresent = [cleoType],
				lines = [
					new Say { who = cleoType, loopTag = "neutral" },
				],
			};

		newNodes[["StartedBattleAgainstDuncan"]] = new()
		{
			priority = true,
			turnStart = true,
			maxTurnsThisCombat = 1,
			oncePerCombat = true,
			allPresent = [cleoType, "skunk"],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
				new Say { who = "skunk", loopTag = "neutral" },
			],
		};

		newNodes[["StartedBattleAgainstDahlia"]] = new()
		{
			priority = true,
			turnStart = true,
			maxTurnsThisCombat = 1,
			oncePerCombat = true,
			allPresent = [cleoType, "bandit"],
			lines = [
				new Say { who = "bandit", loopTag = "neutral" },
				new Say { who = cleoType, loopTag = "squint" },
			],
		};

		newNodes[["StartedBattleAgainstBigCrystal"]] = new()
		{
			priority = true,
			turnStart = true,
			oncePerRun = true,
			requiredScenes = ["Crystal_1", "Crystal_1_1"],
			excludedScenes = ["Crystal_2"],
			allPresent = [cleoType, "crystal"],
			lines = [
				new Say { who = cleoType, loopTag = "neutral" },
			],
		};

		saySwitchNodes[["CrabFacts1_Multi_0"]] = new()
		{
			who = cleoType,
			loopTag = "squint"
		};
		saySwitchNodes[["CrabFacts2_Multi_0"]] = new()
		{
			who = cleoType,
			loopTag = "squint"
		};
	}
}*/
