public partial class Guard : NPC
{
    private NPC _corpseToCheck;
    public void NotifyOfCorpseFounded(NPC corpse) {
        _corpseToCheck = corpse;
        _destination = _corpseToCheck.PathFollow.LastPassMarker;
        GetNode<StateMachine>(".").ChangeState(NpcStateNames.Moving);
    }

    public NPC CorpseToCheck { get => _corpseToCheck; set => _corpseToCheck = value; }
}
