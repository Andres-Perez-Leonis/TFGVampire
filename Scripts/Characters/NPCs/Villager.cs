using Godot;

public partial class Villager : NPC
{

    private bool _availableToTalk = true;
    [Export] private SuspiciousSystem _suspiciousSystem;

    public SuspiciousSystem SuspiciousSystem { get => _suspiciousSystem; set => _suspiciousSystem = value; }
    public bool AvailableToTalk { get => _availableToTalk; set => _availableToTalk = value; }
}
