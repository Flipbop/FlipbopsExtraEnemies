using Flipbop.EnemyPack2;

namespace Flipbop.BOAF;

/// <summary>
/// For if a dialogue needs to be registered AFTER mods have been loaded
/// </summary>
internal interface IDialogueRegisterable
{
    static abstract void LateRegister();
}

static class CommonDefinitions
{
    internal static ModEntry Instance => ModEntry.Instance;

    internal static string AmFishBreath => Instance.FishBreathCharacter.CharacterType;
    internal static string AmRev => Instance.RevCharacter.CharacterType;
        
    internal const string AmUnknown = "johndoe";
    internal const string AmCat = "comp";
    internal static string AmDizzy => Deck.dizzy.Key();
    internal static string AmPeri => Deck.peri.Key();
    internal static string AmRiggs => Deck.riggs.Key();
    internal static string AmDrake => Deck.eunice.Key();
    internal static string AmIsaac => Deck.goat.Key();
    internal static string AmBooks => Deck.shard.Key();
    internal static string AmMax => Deck.hacker.Key();
    internal const string AmVoid = "void";
}