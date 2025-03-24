using Godot;

/**
	Class for keep State names for Vampire

	- To avoid the magic string

*/

public partial class VampireStateNames : Resource
{
	
	public const string Idle = "IdleState";
    public const string Hidden = "HiddenState";
    public const string Dragging = "DraggingState";
    public const string HidingCorpse = "HidingCorpseState";
    public const string Moving = "MovingState";
    public const string Attack = "AttackState";
    public const string Death = "DeathState";
    public const string Talking = "TalkingState";
}
