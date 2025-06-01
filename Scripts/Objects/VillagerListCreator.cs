using System.Collections.Generic;
using Godot;


public partial class VillagerListCreator : Node
{
    private List<Villager> villagers = new();

    private int _timesListCopied = 0;

    private int _maxTimesListCopied;

    public override void _Ready()
    {
        base._Ready();
        _maxTimesListCopied = GetTree().GetNodeCountInGroup(NameGroups.Villagers);
        foreach (Node node in GetTree().GetNodesInGroup(NameGroups.Villagers))
        {
            villagers.Add((Villager)node);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (_timesListCopied == _maxTimesListCopied) QueueFree();
    }

    public List<Villager> ListOfVillagers
    {
        get {
            _timesListCopied++;
            return villagers;
        }
    }
}