using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GuardArretingState : GuardMovingStateBase
{
	public override void Start()
	{
		base.Start();
		if (_guard.EntityInSuspech is Villager villager)
		{
			villager.StopRightThere();
			return;
		}

		Vampire vamp = _guard.EntityInSuspech as Vampire;
		List<MarkerPathSwitch> markers = GetTree().GetNodesInGroup(NameGroups.InterconnectionRoute).OfType<MarkerPathSwitch>().ToList();
		MarkerPathSwitch nearestMarkerToVamp = markers.OrderBy(m => m.GlobalPosition.DistanceSquaredTo(vamp.GlobalPosition)).First();
		_guard.Destination = nearestMarkerToVamp;
    }


    protected override void InMyDestination()
	{
	}

}
