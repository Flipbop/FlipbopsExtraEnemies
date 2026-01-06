using System.Collections.Generic;

namespace Flipbop.EnemyPack2;

public class EventsModded
{
    public static bool isRulesVisible = true;

    public static List<Choice> RevChallenge(State s)
    {
        
        if (isRulesVisible)
        {
            isRulesVisible = false;
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
		
		isRulesVisible = true;
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
        };
		
    }
}