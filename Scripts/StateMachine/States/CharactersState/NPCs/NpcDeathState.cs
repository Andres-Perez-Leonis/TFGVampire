using Godot;
public partial class NpcDeathState : VillagerStateBase
{
    public override void Start()
    {
        base.Start();
		//_npc.AnimationPlayer.Play(AnimationNameNPC.Death);
		_npc.IsDeath = true;
		_npc.SetCollisionLayerValue(4, true); // Set the entity to the Corpse Layer
		_npc.SetCollisionLayerValue(3, false); // Unset the entity to the NPC Layer
    }

}
