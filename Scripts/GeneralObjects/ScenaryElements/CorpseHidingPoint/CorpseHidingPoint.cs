using Godot;

public partial class CorpseHidingPoint : HidingPoint
{
    private NPC _deathNPC;

    public override void _Ready()
    {
        base._Ready();
        _deathNPC = (NPC) HidenNode;
    }


    internal void HideCorpse(NPC corpseNPC)
    {
        if(corpseNPC == null) {
            GD.Print("Se intento esconder un cadaver pero no se encuentra el cadaver");
            return;
        }
        _deathNPC = corpseNPC;
        _deathNPC.Fading(true, 1);
    }

    public override int Interact()
    {
        int corpseFounded = 0;
        if(_deathNPC == null) return corpseFounded;

        float foundedCorpseDice = GD.Randf();

        if(foundedCorpseDice <= this._currentFindHiddenNodeProbability) corpseFounded = 1;

        return corpseFounded;
        
    }


    public NPC CorpseNPC {
        get => _deathNPC;
    }
}
