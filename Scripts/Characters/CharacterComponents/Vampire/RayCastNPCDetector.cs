using Godot;

public partial class RayCastNPCDetector : RayCastDetector
{
	protected NPC _npcDetected = null;


    public override void _PhysicsProcess(double delta)
    {
		if(IsColliding()) {
			//if(npcDetected == GetCollider()) return;
			//GD.Print((GetCollider() as Node2D).Name);
			_npcDetected = (NPC)GetCollider();
			_npcDetected.EmitIamOnTargetSignal(true);
			GD.Print("Estoy colisionando soy: " +  Name);
			
		} else _npcDetected = null;
		
    }



	public NPC NPCDetected { get => _npcDetected; }

}
