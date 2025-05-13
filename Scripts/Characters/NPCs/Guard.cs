using Godot;

public partial class Guard : NPC
{
    private NPC _corpseToCheck;

    [Signal] public delegate void NotifyCorpseFoundEventHandler();

    public void EmitNotifyCorpseFoundSignal(NPC corpse) {
        if(corpse == null) GD.Print("En la llamada al guardía se pierde el corpse");  // IS NOT NULL
        _corpseToCheck = corpse;
        EmitSignal(SignalName.NotifyCorpseFound);
    }

    public NPC CorpseToCheck { get => _corpseToCheck; set => _corpseToCheck = value; }
}
