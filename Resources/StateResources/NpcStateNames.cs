using Godot;

/**
	Class for keep State names for NPC

	- To avoid the magic string

*/

public partial class NpcStateNames : Resource
{
	
	public const string Idle = "IdleState";
    public const string Working = "WorkingState";
    public const string Moving = "MovingState";
    public const string MovingScared = "MovingScaredState";
    public const string Attack = "AttackState";
    public const string Death = "DeathState";
    public const string Talking = "TalkingState";
    public const string KeepingObject = "KeepingObjectState";
    public const string ClosingDoor = "ClosingDoorState";
    public const string ClosingDoorFast = "ClosingDoorState";
    public const string CorpseFound = "CorpseFoundState";
    public const string GivingAlarm = "GivingAlarmState";
    public const string GivingAlarmRunning = "GivingAlarmRunningState";


}
