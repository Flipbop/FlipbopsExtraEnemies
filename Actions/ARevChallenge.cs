using System;
using FSPRO;
using Nickel;
using System.Collections.Generic;
using Flipbop.BOAF;


namespace Flipbop.EnemyPack2;

public sealed class ARevChallenge : CardAction
{
	public int choice;

	public override void Begin(G g, State s, Combat c)
	{
		if (c.otherShip.ai is RevEnemy rev) 
		{
			if (choice == 0)
			{
				rev.isChallanegActive = false; 
			}
			if (choice == 1)
			{ 
				rev.isChallanegActive = true; 
			}
		}
	}
	
	public override List<Tooltip> GetTooltips(State s)
	{
		return new List<Tooltip>
		{
			new TTText(ModEntry.Instance.Localizations.Localize(["action", "RevChallenge", choice.ToString()]))
		};
	}
}
