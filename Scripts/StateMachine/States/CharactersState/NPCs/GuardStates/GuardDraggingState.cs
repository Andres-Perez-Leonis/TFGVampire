using Godot;

public partial class GuardDraggingState : GuardMovingStateBase
{
    private NPC _draggingCorpse;

    private MarkerPathSwitch _corpsesPoint;
    [Export] private float _nextToGuardOffset = 20;

    public override void Start()
    {
        //base.Start();
        _draggingCorpse = _guard.CorpseToCheck;
        _guard.Destination = _corpsesPoint;


        //GD.Print("Yo " + Owner.Name + " tengo un progreso " + _guard.PathFollow.ProgressRatio);
        //GD.Print("Yo " + "cadaver" + " tengo un progreso " + _draggingCorpse.PathFollow.ProgressRatio);
        //GetTree().Quit();


        _draggingCorpse.Scale /= _guard.Scale;
        _draggingCorpse.Position = new Vector2(0, 0);
        _draggingCorpse.PathFollow.GetParent<Path2D>().RemoveChild(_draggingCorpse.PathFollow);
        _guard.AddChild(_draggingCorpse.PathFollow);
        _draggingCorpse.PathFollow.GlobalPosition = new Vector2(_guard.PathFollow.GlobalPosition.X + _nextToGuardOffset, _guard.PathFollow.GlobalPosition.Y - 20);

        _guard.AnimationStateMachine.Travel((_guard.Scale.X > 0) ?  AnimationNameGuard.Dragging_Right : AnimationNameGuard.Dragging_Left);
        //GD.Print("Posicion Guardia: " + _guard.GlobalPosition);
        //GD.Print("Posicion Cadaver: " + _draggingCorpse.GlobalPosition);
        //GD.Print("Posicion local guardia: " + _guard.Position);
        //GD.Print("Posicion local Cadaver: " + _draggingCorpse.Position);
    }

    public override void _Ready()
    {
        base._Ready();
        _corpsesPoint = (MarkerPathSwitch)GetTree().GetFirstNodeInGroup(NameGroups.CorpsePoint);
    }


    protected override void InMyDestination()
    {
        GD.Print("El del dragging se ejecuta");
        //draggingCorpse.PathFollow.QueueFree();
        //_draggingCorpse.QueueFree();
    }

    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
        //GD.Print("Yo " + Owner.Name + " tengo un progreso " + _guard.PathFollow.ProgressRatio);
        //GD.Print("Yo " + "cadaver" + " tengo un progreso " + _draggingCorpse.PathFollow.ProgressRatio);

    }

}
