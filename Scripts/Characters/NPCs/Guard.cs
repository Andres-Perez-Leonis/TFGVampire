using Godot;

public partial class Guard : NPC
{
    private NPC _corpseToCheck;

    private Entity _entityInSuspech;

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
        _entityInSuspech = entity;
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
    public Entity EntityInSuspech { get => _entityInSuspech; set => _entityInSuspech = value; }
    public bool HasBeenAlerted { get => _hasBeenAlerted; set => _hasBeenAlerted = value; }
}
