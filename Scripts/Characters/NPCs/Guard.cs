using Godot;

public partial class Guard : NPC
{
    private NPC _corpseToCheck;

    [Signal] public delegate void NotifyCorpseFoundEventHandler(NPC corpse);

    public void EmitNotifyCorpseFoundSignal(NPC corpse) {
        EmitSignal(SignalName.NotifyCorpseFound, corpse);
    }

    public NPC CorpseToCheck { get => _corpseToCheck; set => _corpseToCheck = value; }
}
