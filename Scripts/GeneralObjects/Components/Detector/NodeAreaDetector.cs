using System.Data.Common;
using Godot;

public partial class NodeAreaDetector : Area2D
{
    [Export] private UserInterface _interface;

    private bool _nodeInArea;

    public override void _PhysicsProcess(double delta)
    {
        _nodeInArea = HasOverlappingBodies();
        _interface.Fading(_nodeInArea, delta);
    }
}
