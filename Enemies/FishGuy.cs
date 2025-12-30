using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Newtonsoft.Json;
using Nickel;

namespace Flipbop.EnemyPack2;

internal sealed class FishGuyEnemy : AI, IRegisterableEnemy
{
	[JsonProperty]
	private int aiCounter;
	
	public static void Register(IModHelper helper)
	{
		Type thisType = MethodBase.GetCurrentMethod()!.DeclaringType!;
		IRegisterableEnemy.MakeSetting(helper, helper.Content.Enemies.RegisterEnemy(new() {
			EnemyType = thisType,
			Name = ModEntry.Instance.AnyLocalizations.Bind(["ship","fishguy"]).Localize,
			ShouldAppearOnMap = (_, map) => IRegisterableEnemy.IfEnabled(thisType, map is MapThree ? BattleType.Normal : null)
		}));
		
	}

	public override void OnCombatStart(State s, Combat c)
	{
		c.bg = new BGUnderwater();
	}

	public override Ship BuildShipForSelf(State s)
	{
		character = new Character
		{
			type = ModEntry.Instance.FishBreathCharacter.CharacterType
		};
		List<Part> parts = [
			new Part {
				key = "wing.left",
				type = PType.wing,
				skin = "wing_ancient",
				damageModifier = PDamMod.weak
			},
			new Part()
			{
				key = "bay.left"	,
				type = PType.missiles,
				skin = "missiles_ancient"
			},
			new Part()
			{
				key	= "centerpiece.left",
				type = PType.wing,
				skin = "wing_ancient"
			},
			new Part()
			{
				key = "cannon",
				type = PType.cannon,
				skin = "cannon_ancient"
			},
			new Part()
			{
				key	= "centerpiece.right",
				type = PType.wing,
				skin = "wing_ancient",
				flip = true
			},
			new Part()
			{
				key = "bay.right"	,
				type = PType.missiles,
				skin = "missiles_ancient",
				flip = true
			},
			new Part()
			{
				key = "wing.right",
				type = PType.cockpit,
				skin = "cockpit_ancient",
				damageModifier = PDamMod.weak,
			}
		];
		return new Ship {
			x = 6,
			hull = 10,
			hullMax = 10,
			shieldMaxBase = 5,
			ai = this,
			chassisUnder = "chassis_ancient",
			parts = parts
		};
	}

	public override EnemyDecision PickNextIntent(State s, Combat c, Ship ownShip)
	{
		return MoveSet(aiCounter++, () => new EnemyDecision
		{
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon"),
			intents =
			[
				new IntentSpawn()
				{
					thing = new FishingHook(){xDir = 1, targetPlayer = true},
					key = "bay.left"
				},
				new IntentAttack()
				{
					key = "cannon",
					damage = 2,
				}
			]

		}, () => new EnemyDecision
		{
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon"),
			intents =
			[
				new IntentSpawn()
				{
					thing = new FishingHook(){xDir = -1, targetPlayer = true},
					key = "bay.right"
				},
				new IntentAttack()
				{
					key = "cannon",
					damage = 5,
				}
			]
		}, () => new EnemyDecision
		{
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon"),
			intents =
			[
				new IntentStatus()
				{
					key = "centerpiece.left",
					amount = 1,
					status = Status.bubbleJuice,
					targetSelf = true
				},
				new IntentStatus()
				{
					key = "centerpiece.right",
					amount = 1,
					status = Status.bubbleJuice,
					targetSelf = true
				},
				new IntentAttack()
				{
					key = "cannon",
					damage = 5,
				}
			]
		});
	}
}
