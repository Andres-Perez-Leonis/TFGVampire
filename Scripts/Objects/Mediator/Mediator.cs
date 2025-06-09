using Godot;
using System.Collections.Generic;

public partial class Mediator : Node
{

	private VillagerTalkingState _senderState;
	private VillagerTalkingState _recipiestState;
	[Signal] public delegate void CloseConversationEventHandler(Mediator mediator);

	private int _timesSended = 0;

	public void Start()
	{
		_senderState.Mediator = _recipiestState.Mediator = this;
		_senderState.InitConversation(_recipiestState.Villager);
		_recipiestState.InitConversation(_senderState.Villager);
	}

	public void SendInfo(VillagerTalkingState whoSend, List<EntitySuspechData> list)
	{
		if (whoSend == _senderState) _recipiestState.HeardSuspisions(list, _senderState.Villager);
		else _senderState.HeardSuspisions(list, _recipiestState.Villager);
		_timesSended++;
		if (_timesSended < 2) return;
		_timesSended = 0;
		FinishConversation();
	}

	private void FinishConversation()
	{
		_senderState.Mediator = _recipiestState.Mediator = null;
		_senderState.FinishConversation();
		_recipiestState.FinishConversation();
		EmitSignal(SignalName.CloseConversation, this);
	}


	public VillagerTalkingState SenderState { get => _senderState; set => _senderState = value; }
	public VillagerTalkingState RecipiestState { get => _recipiestState; set => _recipiestState = value; }
}
