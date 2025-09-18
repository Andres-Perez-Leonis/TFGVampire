using Godot;

public partial class RayCastCorpseDetector : RayCastDetector
{
	protected Villager _villagerDetected; // Thought this attribute is a NPC a Corpse is still being a NPC but with a different Layer Mask


    public override void _PhysicsProcess(double delta)
    {
		if(IsColliding()) {
			//if(npcDetected == GetCollider()) return;
			//GD.Print((GetCollider() as Node2D).Name);
			_villagerDetected = (Villager)GetCollider();
			_villagerDetected.EmitIamOnTargetSignal(true);
			
		}
		
    }



	public Villager VillagerDetected { get => _villagerDetected; }

}
