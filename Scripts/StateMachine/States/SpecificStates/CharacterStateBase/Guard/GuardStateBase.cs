using Godot;
using System;

public partial class GuardStateBase : NpcStateBase
{
    protected Guard _guard;
    public override void _Ready()
    {
        base._Ready();
        _guard = (Guard)_npc;
        
    }

}
