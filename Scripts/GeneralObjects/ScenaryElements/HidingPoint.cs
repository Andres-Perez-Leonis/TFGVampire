using Godot;

public partial class HidingPoint : Area2D
{
    [Export] private AnimatedSprite2D _animator;


    [Export] private UserInterface _interface;


    private bool _nodeInArea = false;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        _nodeInArea = HasOverlappingBodies();
        //GD.Print("NodeOverlapping: ", _nodeInArea);
        _interface.Fading(_nodeInArea, delta);
    }

    private Node _hidenNode;

    public Node HidenNode  { get => _hidenNode; set=> _hidenNode = value; }

    public AnimatedSprite2D Animator { get => _animator; }
}
