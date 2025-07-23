using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GuardAttackState : GuardMovingStateBase
{

	private static Vampire _vampire;

	public override void _Ready()
	{
		base._Ready();
		CallDeferred("_readyCallDefered");
    }

	private void _readyCallDefered()
	{
		_vampire = (Vampire)GetTree().GetFirstNodeInGroup(NameGroups.Vampire);
	}


	public override void Start()
	{
		GetNode<Area2D>("../../VampireDetector/VisionConeArea").BodyEntered += _nearVampire;
		List<Node> markerPathSwitch = GetTree().GetNodesInGroup(NameGroups.InterconnectionRoute).ToList();

		MarkerPathSwitch nearestMarker = markerPathSwitch.OfType<MarkerPathSwitch>()
			.OrderBy(marker => _vampire.GlobalPosition.DistanceSquaredTo(marker.GlobalPosition))
			.First();

		_guard.Destination = nearestMarker;
		CheckOrientation(nearestMarker.GlobalPosition);
	}

	private void _nearVampire(Node2D node2D)
	{
		// GAME OVER
		_guard.AnimationStateMachine.Travel((_guard.Scale.X > 0) ? AnimationNameGuard.Attack_Right : AnimationNameGuard.Attack_Left);
		GetTree().Quit();
	}

    protected override void InMyDestination()
    {
    }
}
