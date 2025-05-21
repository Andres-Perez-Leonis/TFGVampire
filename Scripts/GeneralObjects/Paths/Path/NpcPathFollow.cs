using Godot;

/**
	NpcPathFollow class is the base of all character
		Extends of PathFollow2D, since they will be responsible for follow the current Path2D
*/

public partial class NpcPathFollow : PathFollow2D
{

	[Export] private MarkerPathSwitch _lastPassMarker;
    // OnChangePath Signal
	[Signal] public delegate void OnChangePathEventHandler(Vector2 finalPointOfNextPath);
	// InMyDestination Signal
	[Signal] public delegate void InMyDestinationEventHandler();

	// Method for emit OnChangePath signal
	public void EmitInMyDestinationSignal(){
		EmitSignal(SignalName.InMyDestination);
	}
	// Method for emit InMyDestination signal
	public void EmitOnChangePathSignal(Vector2 finalPointOfNextPath) {
		//GD.Print("El punto que me han enviado es: " + finalPointOfNextPath);
		EmitSignal(SignalName.OnChangePath, finalPointOfNextPath);
	}

	public MarkerPathSwitch LastPassMarker {
		get => _lastPassMarker;
		set => _lastPassMarker = value;
	}
}
