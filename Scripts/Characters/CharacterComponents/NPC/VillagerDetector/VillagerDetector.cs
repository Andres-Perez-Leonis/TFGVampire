using Godot;
using System;

public partial class VillagerDetector : Area2D
{
	private Villager _villager;
	private Entity _lastEntityExposed;

	public override void _Ready()
	{
		base._Ready();
		BodyEntered += OnVillagerDetected;
		BodyEntered += OnEntityDetected;
	}

	private void OnVillagerDetected(Node2D node)
	{
		bool isAVillager = node is Villager;
		if (!isAVillager) return;
		_villager = (Villager)node;
	}

	private void OnEntityDetected(Node2D node)
	{
		bool isEntityNotGuard = node is Entity && node is not Guard;
		if (!isEntityNotGuard) return;
		_lastEntityExposed = node as Entity;
	}

	public void ResetDetector()
	{
		_villager = null;
	}


	public Villager Villager
	{
		get => _villager;
	}

	public Entity LastVillagerExposed { get => _lastEntityExposed; }

}
