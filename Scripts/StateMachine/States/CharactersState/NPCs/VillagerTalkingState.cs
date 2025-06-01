using Godot;
using System;

public partial class VillagerTalkingState : VillagerStateBase
{

	//[Signal] public delegate void TalkRequestEventHandler(Villager sender, Villager recipient);

	private static Mediator _mediator;
	private Villager _villagerToTalk;
	[Export] private VillagerDetector _detector;

	public override void _Ready()
	{
		base._Ready();
		_mediator = GetTree().Root.GetNode<Mediator>("Mediator");
	}

	public override void Start()
	{
		base.Start();
		
		_villagerToTalk = _detector.Villager;
		VillagerTalkingState villagerTalkingState = _villagerToTalk.GetNode	<VillagerTalkingState>("StateMachine/TalkingState");
		_mediator.TalkRequest(this, villagerTalkingState);
	}




	/*
		public void EmitTalkRequest(Villager recipient)
		{
			EmitSignal(SignalName.TalkRequest, this, recipient);
		}
	*/
	public void InitConversation(Villager villager)
	{
		_villager.Destination = villager.PathFollow.LastPassMarker;
	}

	public void RejectConversation(Villager villager)
	{
		StateMachine.ChangeState(NpcStateNames.Idle);
	}

	public Mediator Mediator { get => _mediator; set => _mediator = value; }
	public Villager Villager { get => _villager; }
}
