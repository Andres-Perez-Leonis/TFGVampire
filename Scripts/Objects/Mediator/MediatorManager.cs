using Godot;
using System;
using System.Collections.Generic;

public partial class MediatorManager : Node
{
	private Queue<Mediator> _mediatorsInPool = new();
	//private Queue<Mediator> _mediatorsWorking = new();
	[Export] private int _initMediators = 5;
	[Export] private PackedScene _mediatorScene;

	public override void _Ready()
	{
		base._Ready();
		for (int i = 0; i < _initMediators; i++)
			_mediatorsInPool.Enqueue(_mediatorScene.Instantiate() as Mediator);
	}
	public void TalkRequest(VillagerTalkingState stateSender, VillagerTalkingState stateRecipiest)
	{

		Villager senderVillager = stateSender.Villager;
		Villager recipiestVillager = stateRecipiest.Villager;


		if (!senderVillager.AvailableToTalk) return;
		if (senderVillager.SuspiciousSystem.iSuspechOf(recipiestVillager)) return;


		if (!recipiestVillager.AvailableToTalk || recipiestVillager.SuspiciousSystem.iSuspechOf(senderVillager))
		{
			stateSender.RejectConversation(recipiestVillager);
			return;
		}


		Mediator mediator = (_mediatorsInPool.Count > 0) ? _mediatorsInPool.Dequeue() : new();

		if (!IsConnected(Mediator.SignalName.FinishConversation, new Callable(this, "CloseConversation"))) mediator.FinishConversation += CloseConversation;

		//_mediatorsWorking.Enqueue(mediator);
		AddChild(mediator);

		stateSender.Mediator = stateRecipiest.Mediator = mediator;

		mediator.StateSender = stateSender;
		mediator.StateRecipiest = stateRecipiest;

		stateSender.InitConversation(recipiestVillager);
		stateRecipiest.InitConversation(senderVillager);
	}

	public void CloseConversation(Mediator mediator)
	{
		RemoveChild(mediator);
		mediator.StateSender = mediator.StateRecipiest = null;
		_mediatorsInPool.Enqueue(mediator);
	}
}
