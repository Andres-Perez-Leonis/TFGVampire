using Godot;

/**
	Class for keep State names for NPC

	- To avoid the magic string

*/ 

public partial class GuardStateNames : Resource
{
	
	public const string Idle = "IdleState";
    public const string Moving = "MovingState";
    public const string Attack = "AttackState";
    public const string Death = "DeathState";
    public const string Drag = "DraggingState";


}
