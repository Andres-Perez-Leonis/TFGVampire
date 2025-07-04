using Godot;

public partial class Guard : NPC
{
    private NPC _corpseToCheck;

    private Entity _entityToArrest;

    private bool _hasBeenAlerted  = false;

    [Signal] public delegate void NotifyCorpseFoundEventHandler();
    [Signal] public delegate void NotifySuspisionEventHandler();
    [Signal] public delegate void VampireDetectedEventHandler();

    public void ReportVampireDetected()
    {
        _hasBeenAlerted = true;
        EmitSignal(SignalName.VampireDetected);
    }

    public void ReportSuspicion(Entity entity)
    {
        if (entity == null)
        {
            GD.Print("[Guard] Error: El villager sospechoso es nulo");
            return;
        }
        _hasBeenAlerted = true;
        _entityToArrest = entity;
        if (entity is Villager v)
        {
            v.OnArrest = true;
        }
        EmitSignal(SignalName.NotifySuspision);
    }


    public void ReportCorpseFound(NPC corpse)
    {
        if (corpse == null)
        {
            GD.PrintErr("[Guard] Error: El NPC cadáver es nulo  ");
            return;
        }
        _hasBeenAlerted = true;
        _corpseToCheck = corpse;
        EmitSignal(SignalName.NotifyCorpseFound);
    }

    public NPC CorpseToCheck { get => _corpseToCheck; set => _corpseToCheck = value; }
    public Entity EntityInSuspech { get => _entityToArrest; set => _entityToArrest = value; }
    public bool HasBeenAlerted { get => _hasBeenAlerted; set => _hasBeenAlerted = value; }
}
