using System;
using System.Collections.Generic;
using Nanoray.PluginManager;
using Nickel;

namespace Flipbop.EnemyPack2;

internal interface IRegisterable
{
	static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}
internal interface IRegisterableEnemy
{
	static ModSettings ModSettings => ModEntry.Instance.ModSettings;
	static List<IModSettingsApi.IModSetting> SettingsEntries => ModEntry.Instance.SettingsEntries;
	abstract static void Register(IModHelper helper);

	public static void MakeSetting(IModHelper helper, IEnemyEntry entry) {
		if (helper.ModRegistry.GetApi<IModSettingsApi>("Nickel.ModSettings") is { } settingsApi) {
			SettingsEntries.Add(settingsApi.MakeCheckbox(
				() => entry.Configuration.Name?.Invoke(DB.currentLocale.locale) ?? "???",
				() => !ModSettings.enemiesDisabled.GetValueOrDefault(entry.Configuration.EnemyType.ToString()),
				(_, _, to) => ModSettings.enemiesDisabled[entry.Configuration.EnemyType.ToString()] = !to));
		}
	}

	public static BattleType? IfEnabled(Type type, BattleType? battleType) {
		return !ModSettings.enemiesDisabled.GetValueOrDefault(type.ToString()) ? battleType : null;
	}
}

class ModSettings {
	public readonly Dictionary<string, bool> enemiesDisabled = [];
}