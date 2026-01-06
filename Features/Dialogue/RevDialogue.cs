using System.Collections.Generic;
using Flipbop.BOAF;
using Flipbop.EnemyPack2;
using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using static Flipbop.BOAF.CommonDefinitions;

namespace Flipbop.BOAF;

internal class RevDialogue 
{
    public string RevChallenge = AccessTools.DeclaredMethod(typeof(EventsModded), nameof(EventsModded.RevChallenge)).Name;
    public string RevChallengeNoRules = AccessTools.DeclaredMethod(typeof(EventsModded), nameof(EventsModded.RevChallengeNoRules)).Name;

    public RevDialogue()
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {
            {"Rev_Challenge_First", new(){
                type = NodeType.@event,
                priority = true,
                once = true,
                lookup = [
                    "Rev_challenge"
                ],
                dialogue = [
                    new (AmRev, "Ah, you're here at last.", true),
                    new (new List<DialogueThing>
                    {
                        new (AmDizzy, "squint", "Who are you?"),
                        new (AmRiggs, "We are?"),
                        new (AmPeri, "mad","State your business."),
                        new (AmIsaac, "Do I know you?"),
                        new (AmDrake, "squint", "You don't look like a pirate."),
                        new (AmMax, "squint","This feels familiar..."),
                        new (AmBooks, "Hi!!!"),
                        new (AmCat, "squint", "Have we met?")
                    }),
                    new (AmRev, "I am Rev. Perhaps you will be able to match me in combat?", true),
                    new (AmRev, "Play by my rules, and I might just reward you. Unless you just want an old fashioned brawl.", true, choiceFunc: RevChallenge),
                    
                ]
            }},
            {"Rev_Challenge", new(){
                type = NodeType.@event,
                priority = false,
                lookup = [
                    "Rev_challenge"
                ],
                dialogue = [
                    new (new List<DialogueThing>
                    {
                        new (AmRev, "You again! Ready for <c=keyword>another duel</c>?", true, choiceFunc: RevChallenge),
                        new (AmRev, "<c=keyword>Another duel</c> to pass the time?", true, choiceFunc: RevChallenge),
                        new (AmRev, "Would you like to <c=keyword>duel</c>?", true, choiceFunc: RevChallenge),
                    })
                    
                ]
            }},
            {"Rev_Challenge_Max_0", new(){
                type = NodeType.@event,
                priority = true,
                once = true,
                allPresent = [AmMax],
                lookup = [
                    "Rev_challenge"
                ],
                dialogue = [
                    new (AmMax, "Dude I gotta say, I love your cosplay."),
                    new (AmRev, "squint","Cosplay? I'm not wearing any cosplay.", true),
                    new (AmMax, "squint","So you mean to tell me you just look like this?"),
                    new (AmRev, "squint","Yes?", true),
                    new (AmMax, "Cool."),
                    new (AmRev, "Would you like a duel?", true, choiceFunc: RevChallenge),
                    
                ]
            }},
            {"Rev_Challenge_Max_1", new(){
                type = NodeType.@event,
                priority = true,
                once = true,
                allPresent = [AmMax],
                requiredScenes = ["Rev_Challenge_Max_0"],
                lookup = [
                    "Rev_challenge"
                ],
                dialogue = [
                    new (AmMax, "squint","Wait, so you don't know who you look like?"),
                    new (AmRev, "squint","I can't say I do.", true),
                    new (AmMax, "Oh c'mon! Everyone has heard of him!"),
                    new (AmRev, "Not me apparently.", true),
                    new (AmMax, "Okay, I will come back with a picture of him to prove it to you."),
                    new (AmRev, "Sure, but in the meantime, want another duel?", true, choiceFunc: RevChallenge),
                    
                ]
            }},
            {"Rev_Challenge_Max_2", new(){
                type = NodeType.@event,
                priority = true,
                once = true,
                allPresent = [AmMax],
                requiredScenes = ["Rev_Challenge_Max_1"],
                lookup = [
                    "Rev_challenge"
                ],
                dialogue = [
                    new (AmMax, "Hey! Look, I got a picture of him. You look just like him."),
                    new (AmRev, "squint","It can't be...", true),
                    new (AmMax, "Hah! I knew it! You do look like him!"),
                    new (AmRev, "No, I can't believe you would think I look anything like this fool!", true),
                    new (AmMax, "squint", "Oh come on!"),
                    new (AmRev, "No duel today! You've insulted my pride.", true),
                ]
            }},
            {"Rev_Challenge_Accept", new(){
                type = NodeType.@event,
                never = true,
                dialogue = [
                    new (AmRev, "Excellent! A true duel!", true),
                ]
            }},
            {"Rev_Challenge_Refuse", new(){
                type = NodeType.@event,
                never = true,
                dialogue = [
                    new (AmRev, "No? Just a simple battle? Boring, but fine.", true),
                ]
            }},
            {"Rev_Challenge_Rules", new(){
                type = NodeType.@event,
                never = true,
                lookup = ["Rev_Rules"],
                dialogue = [
                    new (AmRev, "Both of us will drop our shields. I will give you 3 cards: Block, Shoot, and Reload. These are the only cards you will have.", true),
                    new (AmRev, "You cannot play Shoot unless you have previously loaded you cannon using Reload. You can have up to 6 bullets loaded at once.", true),
                    new (AmRev, "But, keep in mind that I will act first if you decide to shoot.", true),
                    new (AmRev, "I will hide my intentions, but I play by the same rules as you. I cannot shoot you unless I have previously loaded my cannon.", true),
                    new (AmRev, "If you think I intend to shoot you, play Block to not be hit.", true),
                    new (AmRev, "You likely will only be able to afford one card on your turn, so choose wisely!", true),
                    new (AmRev, "The first person to be hit 3 times loses.", true),
                    new (AmRev, "So, want to play?", true, choiceFunc: RevChallengeNoRules),

                ]
            }}
        });
    }
}