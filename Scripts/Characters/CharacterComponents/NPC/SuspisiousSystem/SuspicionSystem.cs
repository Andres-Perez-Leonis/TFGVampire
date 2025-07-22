using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SuspicionSystem : Node
{

	private List<EntitySuspechData> _suspechData = new();
	private Dictionary<Entity, EntitySuspechData> _dicSuspechData;

	[Signal] public delegate void ImSureThatIsEventHandler(); 

	[Export] private int _thresholdSuspision = 90;
	[Export] private int _thresholdTalkAGuard = 90;

	[Export] private int _reductionSuspisionAmount = 10;
	[Export] private int _increaseSuspisionAmount = 10;

	private Random _rnd;

	private Villager _myVillager;

	public override void _Ready()
	{
		base._Ready();
		_myVillager = Owner as Villager;
		_myVillager.InMadness += InMadness;
		_rnd = new(GetHashCode());
		CallDeferred("CallDeferredReady");
	}


	private void CallDeferredReady()
	{
		VillagerListCreator creator = GetTree().Root.GetNode<VillagerListCreator>("MainScene/VillagerListCreator");
		List<Villager> villagers = creator.ListOfVillagers;

		foreach (Entity entity in villagers)
		{
			if(entity != _myVillager) _suspechData.Add(new(entity, 10));
		}
		_suspechData.Add(new(GetTree().GetFirstNodeInGroup(NameGroups.Vampire) as Entity)); // Vampire
		_dicSuspechData = _suspechData.ToDictionary(e => e.Entity);
		ShowSuspisions();

	}

	public List<EntitySuspechData> EntityInSuspech(int entityAmountToTell)
	{
		return (_myVillager.Personality.Gossipy) ? _suspechData.Where(e => e.AmountOfSuspicion > 0).Take(entityAmountToTell).ToList() : new();
	}

	public List<EntitySuspechData> EntityInSuspech(int entityAmountToTell, Villager whoItelling)
	{
		if (!_myVillager.Personality.Gossipy) return new();
		List<EntitySuspechData> entities =_suspechData.Where(e => e.AmountOfSuspicion > 0).Take(entityAmountToTell).ToList();
		if (_myVillager.Personality.Prudent)
			if(entities.Contains(_dicSuspechData[whoItelling])) entities.Remove(_dicSuspechData[whoItelling]);
		return entities;
	}

	public List<EntitySuspechData> EntityInSuspech()
	{
		return _suspechData.Where(e => e.AmountOfSuspicion > 0).Take(3).ToList();
	}

	public void AnalizeSupisions(List<EntitySuspechData> entities, Entity whoSaidMeThat)
	{
		//Comprobamos las sospechas del otro NPC
		if (!_myVillager.Personality.EasyInfluenced) return;

		bool heThinkItsMe = false;

		//Dictionary<Entity, EntitySuspechData> suspechData = _suspechData.ToDictionary(s => s.Entity);
		foreach (EntitySuspechData entityData in entities)
		{
			heThinkItsMe = (entityData.Entity == _myVillager);
			IncreaseSuspision(entityData.Entity);
		}

		if (heThinkItsMe) IncreaseSuspision(whoSaidMeThat);

		HashSet<Entity> hashEntity = new HashSet<Entity>(EntityInSuspech(entities.Count).Select(s => s.Entity));
		int matches = entities.Count(e => hashEntity.Contains(e.Entity));

		_dicSuspechData[whoSaidMeThat].AmountOfSuspicion -= _reductionSuspisionAmount * matches;

		_suspechData.Sort((a, b) => b.AmountOfSuspicion.CompareTo(a.AmountOfSuspicion));
		ShowSuspisions();
	}

	private void ShowSuspisions()
	{
		GD.Print("Lista de sospechos");
		for (int i = 0; i < _suspechData.Count; i++)
		{
			GD.Print(i + ". " + _suspechData[i].Entity.Name + " : " + _suspechData[i].AmountOfSuspicion);
		}
	}

	public void InMadness()
	{
		List<EntitySuspechData> entities = _suspechData.Where(e => e.AmountOfSuspicion < 100).ToList();
		for (int i = 0; i < entities.Count; i++)
		{
			entities[i].AmountOfSuspicion = 100;
		}
	}

	public void IncreaseSuspision(Entity entity)
	{
		if (entity != _myVillager) return;

		if (!_dicSuspechData.TryGetValue(entity, out var data))
		{
			GD.PrintErr($"[IncreaseSuspicion] Error: No se encontró la entidad '{entity.Name}' en el diccionario.");
			return;
		}

		data.AmountOfSuspicion += _increaseSuspisionAmount;

		if (data.AmountOfSuspicion >= 100)
		{
			TellingToGuard();
		}
		else if (data.AmountOfSuspicion > 90 && _rnd.NextDouble() > 0.5)
		{
			TellingToGuard();
		}
	}

	public Entity ObteinMostSuspision()
	{
		return _suspechData[0].Entity;
	}

	private void TellingToGuard()
	{
		EmitSignal(SignalName.ImSureThatIs);
	}



	public bool iSuspechOf(Entity entity)
	{
		return _thresholdSuspision < _dicSuspechData[entity].AmountOfSuspicion;
	}
	

}
