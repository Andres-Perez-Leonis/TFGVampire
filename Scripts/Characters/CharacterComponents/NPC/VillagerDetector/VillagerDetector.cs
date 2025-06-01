using Godot;
using System;

public partial class VillagerDetector : Area2D
{
	private Villager _villager;

	public override void _Ready()
	{
		base._Ready();
		BodyEntered += OnVillagerDetected;
		BodyExited += OnVillagerExitDetected;
	}

	private void OnVillagerDetected(Node2D node)
	{
		bool isAVillager = node is Villager;
		if (!isAVillager) return;
		_villager = (Villager)node;
	}

	private void OnVillagerExitDetected(Node2D node)
	{
		_villager = null;
	}


	public Villager Villager
	{
		get => _villager;
	}

}
