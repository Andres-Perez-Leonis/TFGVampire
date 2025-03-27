using Godot;

public partial class NPCDetector : AreaDetector
{

    private NPC _nearestNPC;
    public override void _PhysicsProcess(double delta)
    {
        _nearestNPC = FindNearestNPC();
    }

    protected override NPC FindNearestNPC()
    {
		if (_nodes == null || _nodes.Count == 0)
            return null;

        Node2D nearestNPC = null;
        float minDistanceSquared = float.MaxValue; // Using squared distance avoids Sqrt() for performance

        foreach (Node2D npc in _nodes)
        {
            if (npc == null) continue;

            float distanceSquared = GlobalPosition.DistanceSquaredTo(npc.GlobalPosition);
            
            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                nearestNPC = npc;
            }
        }

        return nearestNPC as NPC;
    }
 
	public Node2D NearestNPC { get => _nearestNPC; }   
}
