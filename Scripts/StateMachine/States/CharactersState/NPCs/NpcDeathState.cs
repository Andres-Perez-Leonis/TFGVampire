using Godot;
public partial class NpcDeathState : NpcStateBase
{
    public override void Start()
    {
        base.Start();
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Death);
        _npc.AnimationStateMachine.Travel(AnimationNameNPC.Death);
        _npc.Position = new(_npc.Position.X, 0);
		_npc.IsDeath = true;
		_npc.SetCollisionLayerValue(4, true); // Set the entity to the Corpse Layer
		_npc.SetCollisionLayerValue(3, false); // Unset the entity to the NPC Layer
    }

}
