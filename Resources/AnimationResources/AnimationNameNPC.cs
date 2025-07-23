using Godot;
/**
	Class for keep Animation names for NPC

	- To avoid the magic string

*/ 

public partial class AnimationNameNPC : Resource
{
	public const string Idle = "Idle";
	public const string Moving = "Walking";
	public const string Talking = "Talking";
	public const string Fleeing = "Fleeing"; //HUIR
	public const string Shout = "Shout";
	public const string Death = "Death";
}
