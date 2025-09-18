using Godot;
using System;
using System.Collections.Generic;

public partial class VillagerTalkingState : VillagerStateBase
{

	//[Signal] public delegate void TalkRequestEventHandler(Villager sender, Villager recipient);

	private static MediatorManager MediatorManager;
	private Villager _villagerToTalk;
	[Export] private VillagerDetector _detector;

	private Mediator _mediator;

	public override void _Ready()
	{
		base._Ready();
		MediatorManager = GetTree().Root.GetNode<MediatorManager>("MainScene/MediatorManager");
	}

	public override void Start()
	{
		base.Start();

		_villagerToTalk = _detector.Villager;
		VillagerTalkingState villagerTalkingState = _villagerToTalk.GetNode<VillagerTalkingState>("StateMachine/TalkingState");
		MediatorManager.TalkRequest(this, villagerTalkingState);
	}

	public void InitConversation(Villager villager)
	{
		List<Entity> villagersInSuspech = _villager.SuspicionSystem.EntitiesInSuspech(3, _villager);
		_mediator.SendInfo(this, villagersInSuspech);
	}

	public void HeardSuspisions(List<Entity> villagersInSuspech, Villager whoTellMe)
	{
		_villager.SuspicionSystem.AnalizeSupisions(villagersInSuspech, whoTellMe);
	}

	public void FinishConversation()
	{
		_detector.ResetDetector();
		StateMachine.ChangeState(NpcStateNames.Idle);
	}

	public void RejectConversation(Villager villager)
	{
		StateMachine.ChangeState(NpcStateNames.Idle);
	}

	public Mediator Mediator { get => _mediator; set => _mediator = value; }
	public Villager Villager { get => _villager; }
}
