using Godot;
using System;

public abstract partial class VillagerStateBase : NpcStateBase
{
    protected Villager _villager;

    public override void _Ready()
    {
        base._Ready();
        _villager = (Villager)_npc;
    }
}
