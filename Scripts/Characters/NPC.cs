using Godot;
using Godot.Collections;

/**
    NPC class represents a Non-Playable character
        Will follow a routine

    Extends the Entity class
*/

public partial class NPC : Entity, IFading
{

    [Export] public bool debugMode = false;
    // Array which contein the routine. The routine are spiciefied by a sequence of Waypoints
    [Export] private Array<MarkerPathSwitch> _routine;

    // Index to track the current Waypoint/Task of the routine
    private int _indexRoutine = 0;
    protected MarkerPathSwitch _destination;
    
    private bool _isHide = false;
    // Waypoint which indicate the NPC workplace
    [Export] private MarkerPathSwitch _workPlace;
    [Export] private MarkerPathSwitch _house;



    public override void _Ready()
    {
        base._Ready();
        if (_routine?.Count > 0) _destination = _routine[0];
    }


    public void NextTask() {  IndexRoutine = 0; GD.Print("Mi tarea actual: " + CurrentAction.Name); }

    public void Fading(bool iVanishing, double process)
    { 
        Color controlModulate = Modulate;

        controlModulate.A += (float)((!iVanishing) ? process : -process);
        
        Modulate = controlModulate;
        _isHide = true;
    }

    public void ToHome() { _destination = _house; }


    #region Getters and Setters


    public bool IsHide { get => _isHide;  set => _isHide = value; }

        // Get the current action of Routine
        public MarkerPathSwitch CurrentAction { get => _destination; }
        public MarkerPathSwitch House { get => _house; }

        // Get - Set all routine (routine Array)
        public Array<MarkerPathSwitch> Routine {
            get => _routine;
            set => _routine = value;
        }

        // Get NPC Workplace Marker
        public MarkerPathSwitch WorkPlace {
            get => _workPlace;
        }
        
        // Get the current index of routine
        public int IndexRoutine {
            get => _indexRoutine;
            set {
                _indexRoutine++;
                _destination = _routine[_indexRoutine];
            }
        }

        public MarkerPathSwitch Destination { set => _destination = value; }

    #endregion

}
