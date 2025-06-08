using System;
using Godot;

public partial class Villager : NPC
{

    private bool _availableToTalk = true;
    private Personality _personality;
    [Export] private SuspiciousSystem _suspiciousSystem;

    [Signal] public delegate void InMadnessEventHandler();

    public override void _Ready()
    {
        base._Ready();
        Random random = new Random(GetHashCode());
        _personality = new Personality(
            easyInfluenced: random.Next(0, 2) == 1,
            believeInMisticysm: random.Next(0, 2) == 1,
            gossipy: random.Next(0, 2) == 1,
            brave: random.Next(0, 2) == 1,
            prudent: random.Next(0, 2) == 1
        );
    }
    
    public Personality Personality { get => _personality; }
    public SuspiciousSystem SuspiciousSystem { get => _suspiciousSystem; set => _suspiciousSystem = value; }
    public bool AvailableToTalk { get => _availableToTalk; set => _availableToTalk = value; }
}
