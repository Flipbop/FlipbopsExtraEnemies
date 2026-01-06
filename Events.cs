using System.Collections.Generic;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nickel;

namespace Flipbop.EnemyPack2;

public class EventsModded {

    public static void Register(IModHelper helper)
    {
        DB.eventChoiceFns.Add(AccessTools.DeclaredMethod(typeof(EventsModded), nameof(EventsModded.RevChallenge)).Name, AccessTools.DeclaredMethod(typeof(EventsModded), nameof(EventsModded.RevChallenge)));
        DB.eventChoiceFns.Add(AccessTools.DeclaredMethod(typeof(EventsModded), nameof(EventsModded.RevChallengeNoRules)).Name, AccessTools.DeclaredMethod(typeof(EventsModded), nameof(EventsModded.RevChallengeNoRules)));

    }
    public static List<Choice> RevChallenge(State s)
    {
        ModEntry.Instance.Logger.LogInformation("YEAH IT'S REGISTERED ALL RIGHT");
        return new List<Choice>
            {
                new ()
                {
                    label = Loc.T("RevChallenge_Yes", "You're on!"),
                    key = "RevChallenge_Yes",
                    actions =
                    {
                        new ARevChallenge
                        {
                            choice = 1,
                        }
                    }
                },
                new ()
                {
                    label = Loc.T("RevChallenge_No", "Pass."),
                    key = "RevChallenge_No",
                    actions =
                    {
                        new ARevChallenge
                        {
                            choice = 0,
                        }
                    }
                },
                new ()
                {
                    label = Loc.T("RevChallenge_Rules", "How do you play?"),
                    key = "RevChallenge_Rules",
                    actions =
                    {
                        new ARevChallenge
                        {
                            choice = 2,
                        },
                        new AMidCombatDialogue()
                        {
                            script = ".Rev_Rules",
                        }
                    }
                },
            };
    }

    public static List<Choice> RevChallengeNoRules(State s)
    {
        return new List<Choice>
        {
            new()
            {
                label = Loc.T("RevChallenge_Yes", "You're on!"),
                key = "RevChallenge_Yes",
                actions =
                {
                    new ARevChallenge
                    {
                        choice = 1,
                    }
                }
            },
            new()
            {
                label = Loc.T("RevChallenge_No", "Pass."),
                key = "RevChallenge_No",
                actions =
                {
                    new ARevChallenge
                    {
                        choice = 0,
                    }
                }
            },
        };
    }
}
