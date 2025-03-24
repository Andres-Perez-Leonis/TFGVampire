using Godot;
/**
	Class to keep Marker signals

*/
public partial class MarkerSignals : Node
{
    // Communicate Marker Signal
    [Signal] public delegate void MarkerToCommunicateEventHandler();

    // Method for emit the Communicate signal
    public void EmitCommunicateMarkerSignal() {
        EmitSignal(SignalName.MarkerToCommunicate);
    }

    

}
