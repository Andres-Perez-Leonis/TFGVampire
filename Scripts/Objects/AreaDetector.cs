using Godot;
using Godot.Collections;

public partial class AreaDetector : Area2D
{
	protected Array<Node2D> _nodes = new();
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	public Array<Node2D> AllNPCDetected { get => _nodes; }

	protected virtual Node2D FindNearestNPC() { return null; }
	
    private void OnBodyEntered(Node2D body) {
        if(!_nodes.Contains(body)) _nodes.Add(body);
		GD.Print("Body entered");
    }

	private void OnBodyExited(Node2D body) {
        _nodes.Add(body);
		GD.Print("Body exited");
    }

}
