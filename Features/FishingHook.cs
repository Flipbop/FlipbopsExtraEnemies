using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nanoray.Shrike;
using Nanoray.Shrike.Harmony;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nickel;

namespace Flipbop.EnemyPack2;

internal sealed class FishingHook : Missile, IRegisterable
{
	private static ISpriteEntry HookSprite = null!;
	private static ISpriteEntry HookIcon = null!;
	
	
	[System.Text.Json.Serialization.JsonConverter(typeof(StringEnumConverter))]
	public enum FishingHookType
	{
		Normal
	}

	[JsonProperty]
	public FishingHookType MissileType = FishingHookType.Normal;

	public override double GetWiggleAmount() => 1.0;

	public override double GetWiggleRate() => 1.0;
	
	public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
	{
		HookSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Midrow/FishHook.png"));
		HookIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/FishHook.png"));
	}

	public override void Render(G g, Vec v)
		=> DrawWithHilight(g, HookSprite.Sprite, v + GetOffset(g), flipY: targetPlayer, flipX: xDir < 0);

	public override Spr? GetIcon()
		=> HookIcon.Sprite;

	public override List<Tooltip> GetTooltips()
	{
		List<Tooltip> tooltips = new List<Tooltip>();
		if (this.xDir >= 0)
		{
			tooltips.Add(new GlossaryTooltip($"midrow.{ModEntry.Instance.Package.Manifest.UniqueName}::Hook")
			{
				Icon = HookIcon.Sprite,
				TitleColor = Colors.midrow,
				Title = ModEntry.Instance.Localizations.Localize(["midrow", "FishingHook", "name"]),
				Description = ModEntry.Instance.Localizations.Localize(["midrow", "FishingHook", "description"]),
				vals = ["left", "right"]
			});
		}
		else
		{
			tooltips.Add(new GlossaryTooltip($"midrow.{ModEntry.Instance.Package.Manifest.UniqueName}::Hook")
			{
				Icon = HookIcon.Sprite,
				TitleColor = Colors.midrow,
				Title = ModEntry.Instance.Localizations.Localize(["midrow", "FishingHook", "name"]),
				Description = ModEntry.Instance.Localizations.Localize(["midrow", "FishingHook", "description"]),
				vals = ["right", "left"]
			});
		}

		if (this.bubbleShield)
		{
			tooltips.Add(new TTGlossary("midrow.bubbleShield"));
		}
		return tooltips;
	}

	public override List<CardAction>? GetActions(State s, Combat c)
	{
		return new List<CardAction>
		{
			new AMissileHit()
			{
				worldX = this.x,
				outgoingDamage = 1,
				targetPlayer = this.targetPlayer,
				xPush = (Math.Sign(this.xDir) * -3)
			}
		};
	}
}