using System.Collections.Generic;
using Flipbop.BOAF;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using static Flipbop.BOAF.CommonDefinitions;

namespace Flipbop.BOAF;

internal class RevDialogue 
{
    public RevDialogue()
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {
            {"Rev_Challenge_First", new(){
                type = NodeType.@event,
                priority = true,
                once = true,
                lookup = [
                    "rev_challenge"
                ],
                dialogue = [
                    new (AmRev, "Ah, you're here at last.", true),
                    new (AmRev, "Perhaps you will be able to match me in combat?", true),
                    new (AmRev, "Play by my rules, and I might just reward you. Unless you just want an old fashioned brawl.", true),

                ]
            }},
            {"Rev_Challenge", new(){
                type = NodeType.@event,
                priority = false,
                lookup = [
                    "rev_challenge"
                ],
                dialogue = [
                    new (AmRev, "You again! Ready for another duel?", true),
                    
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
                dialogue = [
                    new (AmRev, "Both of us will drop our shields. I will give you 3 cards: Block, Shoot, and Reload. These are the only cards you will have.", true),
                    new (AmRev, "You cannot play Shoot unless you have previously loaded you cannon using Reload. You can have up to 6 bullets loaded at once.", true),
                    new (AmRev, "But, keep in mind that I will act first if you decide to shoot.", true),
                    new (AmRev, "I will hide my intentions, but I play by the same rules as you. I cannot shoot you unless I have previously loaded my cannon.", true),
                    new (AmRev, "If you think I intend to shoot you, play Block to not be hit.", true),
                    new (AmRev, "You likely will only be able to afford one card on your turn, so choose wisely!", true),
                    new (AmRev, "The first person to be hit 3 times loses.", true),
                    new (AmRev, "So, want to play?", true),

                ]
            }}
        });
    }
}