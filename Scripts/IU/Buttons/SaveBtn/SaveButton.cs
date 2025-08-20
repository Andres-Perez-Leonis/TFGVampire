using Godot;
using Godot.Collections;
using System.Linq;

public partial class SaveButton : Button
{
	public override void _Ready()
	{
		base._Ready();
		Pressed += OnPressed;
	}
	public void OnPressed()
	{
		Array<VillagerResourceData> villagerResources = new();

		Array<Villager> villagers = new Array<Villager>(
			GetTree().GetNodesInGroup(NameGroups.Villagers)
				.OfType<Villager>()
				.Where(villager => !villager.IsDeath)
		);


		foreach (var villager in villagers)
		{
			villagerResources.Add(new VillagerResourceData(villager));
		}

		SavingSystem savingSystem = new SavingSystem();
		GameResourceData gameDataesources = new GameResourceData();
		savingSystem.SaveGame(villagerResources, gameDataesources);
	}
}
