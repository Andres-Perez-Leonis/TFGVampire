using Godot;

public partial class RayCastVillagerDetector : RayCastDetector
{
	protected Villager _villagerDetected = null;


    public override void _PhysicsProcess(double delta)
    {
		if (IsColliding())
		{
			//if(npcDetected == GetCollider()) return;
			//GD.Print((GetCollider() as Node2D).Name);
			if (_villagerDetected is not null)
			{
				_villagerDetected.EmitIamOnTargetSignal(false);
			}

			_villagerDetected = (Villager)GetCollider();
			_villagerDetected.EmitIamOnTargetSignal(true);
			//GD.Print("Estoy colisionando soy: " +  Name);

		}
		else
		{
			_villagerDetected?.EmitIamOnTargetSignal(false);
			_villagerDetected = null;
		}

    }



	public Villager VillagerDetected { get => _villagerDetected; }

}
