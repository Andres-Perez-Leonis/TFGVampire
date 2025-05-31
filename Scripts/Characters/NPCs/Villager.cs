using Godot;

public partial class Villager : NPC
{

    private bool _availableToTalk = true;
    [Signal] public delegate void TalkRequestEventHandler(NPC sender, NPC recipient);

    private Mediator _mediator;

    public void EmitTalkRequest(NPC recipient)
    {
        EmitSignal(SignalName.TalkRequest, this, recipient);
    }
    public Mediator Mediator { get => _mediator; set => _mediator = value; }
    public bool AvailableToTalk { get => _availableToTalk; set => _availableToTalk = value; }
}
