using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class NotifyingAboutSuspisionState : NPCMovingStateBase
{
	private List<Node2D> _nearestGuards;
	public override void Start()
	{
		base.Start();
		List<Node> guards = GetTree().GetNodesInGroup(NameGroups.GuardGroup).ToList();
		_nearestGuards = guards.OfType<Node2D>().OrderBy(guard => _npc.GlobalPosition.DistanceSquaredTo(guard.GlobalPosition)).Take(2).ToList();
		_npc.Destination = ((Guard)_nearestGuards[0]).PathFollow.LastPassMarker;
	}


	protected override void InMyDestination()
	{
		Entity entity = ((Villager)_npc).SuspicionSystem.ObteinMostSuspision();
		foreach (Guard guard in _nearestGuards)
		{
			if (guard.HasBeenAlerted) continue;
			guard.ReportSuspicion(entity);
		}
		_npc.ToHome();
		StateMachine.ChangeState(NpcStateNames.Moving);
	}

}
