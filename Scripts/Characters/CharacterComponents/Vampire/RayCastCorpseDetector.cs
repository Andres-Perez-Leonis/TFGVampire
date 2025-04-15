using Godot;

public partial class RayCastCorpseDetector : RayCastDetector
{
	protected NPC _npcDetected; // Thought this attribute is a NPC a Corpse is still being a NPC but with a different Layer Mask


    public override void _PhysicsProcess(double delta)
    {
		if(IsColliding()) {
			//if(npcDetected == GetCollider()) return;
			//GD.Print((GetCollider() as Node2D).Name);
			_npcDetected = (NPC)GetCollider();
			_npcDetected.EmitIamOnTargetSignal(true);
			
		}
		
    }



	public NPC NPCDetected { get => _npcDetected; }

}
