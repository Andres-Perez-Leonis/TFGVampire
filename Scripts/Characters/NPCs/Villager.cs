using System;
using Godot;

public partial class Villager : NPC
{

    private bool _availableToTalk = true;
    private Personality _personality;
    private bool _onArrest = false;
    [Export] private SuspicionSystem _suspicionSystem;

    [Signal] public delegate void InMadnessEventHandler();


    [Signal] public delegate void IamOnAttackEventHandler();
    [Signal] public delegate void IamOnTargetEventHandler(bool onTarget);
    [Signal] public delegate void GuardComeForMeEventHandler();


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
        //_suspicionSystem.ImSureThatIs += NotifyToGuardState;
    }

    private void NotifyToGuardState()
    {
        GetNode<StateMachine>("StateMachine").ChangeState(NpcStateNames.NotifyingSuspisionState);
    }

    public void StopRightThere()
    {
        EmitSignal(SignalName.GuardComeForMe);
    }


    public void EmitIamOnAttackSignal()
    {
        EmitSignal(SignalName.IamOnAttack);
    }

    public void EmitIamOnTargetSignal(bool onTarget) {
        EmitSignal(SignalName.IamOnTarget, onTarget);
    }


    public Personality Personality { get => _personality; }
    public SuspicionSystem SuspicionSystem { get => _suspicionSystem; set => _suspicionSystem = value; }
    public bool AvailableToTalk { get => _availableToTalk; set => _availableToTalk = value; }
    public bool OnArrest { get => _onArrest; set => _onArrest = value; }
}
