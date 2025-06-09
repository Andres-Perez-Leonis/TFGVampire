using Godot;
using System;
using System.Collections.Generic;

public partial class MediatorManager : Node
{
	private Queue<Mediator> _mediatorsInPool = new();
	//private Queue<Mediator> _mediatorsWorking = new();
	[Export] private int _initMediators = 5;
	[Export] private PackedScene _mediatorScene;

	private Dictionary<Villager, Villager> _communicationDataBuffer = new();

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

		// Si quien pide la comunicacion no esta dentro del Buffer
		// Significa que es el primero en pedir la comunicacion
		// El otro es posible que no haya entrado aun al estado Talking
		// Por lo que le espera
		if (!_communicationDataBuffer.ContainsKey(senderVillager) && !_communicationDataBuffer.ContainsValue(senderVillager))
		{
			_communicationDataBuffer.Add(senderVillager, recipiestVillager);
			return;
		}

		// Si esta dentro del diccionario significa que ambos estan en el estado Talking y estan listos para hablar
		// Por lo que eliminamos al primero del diccionario
		// Para el segundo, el que recibe es el primero
		_communicationDataBuffer.Remove(recipiestVillager);

		bool senderWantsTalk = senderVillager.AvailableToTalk && !senderVillager.SuspicionSystem.iSuspechOf(recipiestVillager);
		bool recipiestWantsTalk = recipiestVillager.AvailableToTalk && !recipiestVillager.SuspicionSystem.iSuspechOf(senderVillager);



		if (!senderWantsTalk || !recipiestWantsTalk)
		{
			stateSender.RejectConversation(recipiestVillager);
			stateRecipiest.RejectConversation(recipiestVillager);
			return;
		}


		Mediator mediator = (_mediatorsInPool.Count > 0) ? _mediatorsInPool.Dequeue() : _mediatorScene.Instantiate<Mediator>();

		mediator.CloseConversation += CloseConversation;

		//_mediatorsWorking.Enqueue(mediator);
		AddChild(mediator);

		mediator.SenderState = stateSender;
		mediator.RecipiestState = stateRecipiest;
		mediator.Start();
	}

	public void CloseConversation(Mediator mediator)
	{
		RemoveChild(mediator);
		mediator.SenderState = mediator.RecipiestState = null;
		mediator.CloseConversation -= CloseConversation;
		_mediatorsInPool.Enqueue(mediator);
	}
}
