using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SuspiciousSystem : Node
{

	private Dictionary<Villager, int> _amountOfSuspision;

	[Export] private int _thresholdSuspision;
	[Export] private int _thresholdTalkAGuard;

	public override void _Ready()
	{
		base._Ready();
		CallDeferred("CallDeferedReady");
	}


	private void CallDeferredReady()
	{
		VillagerListCreator creator = GetTree().Root.GetNode<VillagerListCreator>("./VillagerListCreator");
		List<Villager> villagers = creator.ListOfVillagers;

		foreach (Villager villager in villagers)
		{
			_amountOfSuspision.Add(villager, 0);
		}
	}

	public Villager[] VillagerInSuspech(int villagerAmountToTell)
	{
		Villager[] villagers = new Villager[villagerAmountToTell];
		Dictionary<Villager, int> aux = _amountOfSuspision.OrderByDescending(villager => villager.Value).Take(villagerAmountToTell).ToDictionary(villager => villager.Key, villager => villager.Value);
		int i = 0;
		foreach (Villager villager in aux.Keys)
		{
			villagers[i] = villager;
			i++;
		}

		return villagers;

	}

	public bool iSuspechOf(Villager villager)
	{
		int suspision = _amountOfSuspision[villager];
		return _thresholdSuspision > suspision;
	}
}
