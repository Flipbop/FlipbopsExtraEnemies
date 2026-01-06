using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Newtonsoft.Json;
using Nickel;

namespace Flipbop.EnemyPack2;

internal sealed class RevEnemy : AI, IRegisterableEnemy
{
	[JsonProperty]
	private int aiCounter;

	[JsonProperty] 
	public bool isChallanegActive;
	
	public static void Register(IModHelper helper)
	{
		Type thisType = MethodBase.GetCurrentMethod()!.DeclaringType!;
		IRegisterableEnemy.MakeSetting(helper, helper.Content.Enemies.RegisterEnemy(new() {
			EnemyType = thisType,
			Name = ModEntry.Instance.AnyLocalizations.Bind(["ship","rev"]).Localize,
			ShouldAppearOnMap = (_, map) => IRegisterableEnemy.IfEnabled(thisType, map is MapLawless ? BattleType.Normal : null)
		}));
		
	}

	public override void OnCombatStart(State s, Combat c)
	{
		c.bg = new BGDesert();

		c.Queue(new AMidCombatDialogue()
		{
			script = ".Rev_challenge",
		});
	}

	public override Ship BuildShipForSelf(State s)
	{
		character = new Character
		{
			type = ModEntry.Instance.RevCharacter.CharacterType
		};
		List<Part> parts = [
			new Part {
				key = "wing.left",
				type = PType.wing,
				skin = "wing_apollo",
			},
			new Part {
				key = "missile",
				type = PType.missiles,
				skin = "missiles_apollo",
			},
			new Part {
				key = "cannon",
				type = PType.cannon,
				skin = "cannon_apollo",
			},
			new Part {
				key = "cockpit",
				type = PType.cockpit,
				skin = "cockpit_apollo",
			},
			new Part {
				key = "wing.left",
				type = PType.wing,
				skin = "wing_apollo",
				flip = true
			},
		];
		return new Ship {
			x = 6,
			hull = 5,
			hullMax = 5,
			shieldMaxBase = 10,
			ai = this,
			chassisUnder = "chassis_cicada",
			parts = parts
		};
	}

	public override EnemyDecision PickNextIntent(State s, Combat c, Ship ownShip)
	{
		if (isChallanegActive)
		{
			
			return MoveSet(new Rand(), () => new EnemyDecision
			{
				actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon"),
				intents =
				[
					new IntentAttack()
					{
						key = "cannon",
						damage = 1,
					},
					new IntentStatus()
					{
						key = "cockpit",
						amount = -1,
						status = ModEntry.Instance.ReloadStatus.Status,
						targetSelf = true
					}
				]

			}, () => new EnemyDecision
			{
				actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon"),
				intents =
				[
					new IntentStatus()
					{
						key = "cockpit",
						amount = -1,
						status = ModEntry.Instance.ReloadStatus.Status,
						targetSelf = true
					}
				]
			}, () => new EnemyDecision
			{
				actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon"),
				intents =
				[
					new IntentStatus()
					{
						key = "cockpit",
						amount = 1,
						status = Status.perfectShield,
						targetSelf = true
					}
				]
			});
		}
		return MoveSet(aiCounter++, () => new EnemyDecision
		{
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon"),
			intents =
			[
				new IntentAttack()
				{
					key = "cannon",
					damage = 4,
				},
				new IntentMissile()
				{
					missileType = MissileType.heavy,
					key = "missile"
				},
			]

		}, () => new EnemyDecision
		{
			actions = AIHelpers.MoveToAimAt(s, ownShip, s.ship, "cannon"),
			intents =
			[
				new IntentAttack()
				{
					key = "cannon",
					damage = 2,
				},
				new IntentStatus()
				{
					key = "cockpit",
					amount = 3,
					status = Status.shield,
					targetSelf = true
				}
			]
		}, () => new EnemyDecision
		{
			intents =
			[
				new IntentStatus()
				{
					key = "cockpit",
					amount = 3,
					status = Status.tempShield,
					targetSelf = true
				},
				new IntentStatus()
				{
					key = "wing.left",
					amount = 1,
					status = Status.autododgeRight,
					targetSelf = true
				}
			]
		});
	}
}
