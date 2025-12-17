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
			Name = ModEntry.Instance.AnyLocalizations.Bind(["ship","fishguy", "name"]).Localize,
			ShouldAppearOnMap = (_, map) => IRegisterableEnemy.IfEnabled(thisType, map is MapThree ? BattleType.Normal : null)
		}));
		
	}
	

	public override Ship BuildShipForSelf(State s)
	{
		character = new Character
		{
			type = ModEntry.Instance.FishBreathCharacter.CharacterType
		};
		List<Part> parts = [
			new Part {
				key = "cannon.left",
				type = PType.cannon,
				skin = "wing_knight"
			},
			new Part {
				key = "wing",
				type = PType.wing,
				skin = "missiles_gemini_off",
			},
			new Part {
				key = "power.left",
				type = PType.cannon,
				skin = "wing_knight"
			},
			new Part {
				key = "cockpit",
				type = PType.cockpit,
				damageModifier = PDamMod.weak,
				stunModifier = PStunMod.stunnable,
				skin = "cockpit_wizard"
			},
			new Part {
				key = "power.right",
				type = PType.cannon,
				skin = "wing_knight",
				flip = true
			},
			new Part {
				key = "wing",
				type = PType.wing,
				skin = "missiles_gemini_off",
				flip = true
			},
			new Part {
				key = "cannon.right",
				type = PType.cannon,
				skin = "wing_knight",
				flip = true
			},
		];
		return new Ship {
			x = 6,
			hull = 15,
			hullMax = 15,
			shieldMaxBase = 0,
			ai = this,
			chassisUnder = "chassis_lawless",
			parts = parts
		};
	}
	
	public override Song? GetSong(State s)
	{
		return Song.Elite;
	}

	public override EnemyDecision PickNextIntent(State s, Combat c, Ship ownShip)
	{
		return MoveSet(aiCounter++, () => new EnemyDecision {
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon.left"),
			intents = [
				new IntentAttack
				{
					key = "power.left",
					damage = 1,
				},
				new IntentAttack
				{
					key = "cannon.left",
					damage = 1,
					multiHit = 5,
				},
			]
			
		}, () => new EnemyDecision
		{
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "power.left"),
			intents = [
				new IntentAttack
				{
					key = "power.right",
					damage = 1,
				},
				new IntentAttack
				{
					key = "power.left",
					damage = 1,
					multiHit = 5,
				},
			]
		}, () => new EnemyDecision
		{
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "power.right"),
			intents = [
				new IntentAttack
				{
					key = "cannon.right",
					damage = 1,
				},
				new IntentAttack
				{
					key = "power.right",
					damage = 1,
					multiHit = 5,
				},
			]
		}, () => new EnemyDecision
		{
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon.right"),
			intents = [
				new IntentAttack
				{
					key = "cannon.left",
					damage = 1,
				},
				new IntentAttack
				{
					key = "cannon.right",
					damage = 1,
					multiHit = 5,
				},
			]
		});
	}
}
