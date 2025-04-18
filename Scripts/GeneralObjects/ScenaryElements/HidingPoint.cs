using Godot;

public abstract partial class HidingPoint : Area2D
{
    [Export] private AnimatedSprite2D _animator;


    [Export] private UserInterface _interface;
    
    [Export] private float _defaultFindHiddenNodeProbability = 0.5f;
    [Export] protected float _currentFindHiddenNodeProbability;

    private Node _hidenNode;


    private bool _nodeInArea = false;

    public override void _Ready()
    {
        base._Ready();
        _currentFindHiddenNodeProbability =_defaultFindHiddenNodeProbability;
    }
    public abstract int Interact();

    public override void _PhysicsProcess(double delta)
    {
        _nodeInArea = HasOverlappingBodies();
        //GD.Print("NodeOverlapping: ", _nodeInArea);
        _interface.Fading(_nodeInArea, delta);
    }


    public Node HidenNode  { get => _hidenNode; set=> _hidenNode = value; }

    public AnimatedSprite2D Animator { get => _animator; }

    public float CurrentFindHiddenNodeProbability {
        get => _currentFindHiddenNodeProbability;
        set => _currentFindHiddenNodeProbability = Mathf.Min(1, value);
    }
}
