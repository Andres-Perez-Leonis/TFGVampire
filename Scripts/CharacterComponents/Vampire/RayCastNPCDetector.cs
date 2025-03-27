using Godot;

public partial class RayCastNPCDetector : RayCastDetector
{
	protected NPC _npcDetected;


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
