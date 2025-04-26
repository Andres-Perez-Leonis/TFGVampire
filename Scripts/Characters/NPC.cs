using Godot;
using Godot.Collections;

/**
    NPC class represents a Non-Playable character
        Will follow a routine

    Extends the Entity class
*/

public partial class NPC : Entity, IFading
{

    // Array which contein the routine. The routine are spiciefied by a sequence of Waypoints
    [Export] private Array<MarkerPathSwitch> _routine;
    // Index to track the current Waypoint/Task of the routine
    private int _indexRoutine = 0;
    protected MarkerPathSwitch _destination;
    
    protected NpcPathFollow _pathFollow;
    private bool _isHide = false;
    // Waypoint which indicate the NPC workplace
    [Export] private MarkerPathSwitch _workPlace;
    [Export] private MarkerPathSwitch _house;

    [Export] private Control _actionInterface;
    [Signal] public delegate void IamOnAttackEventHandler();
    [Signal] public delegate void IamOnTargetEventHandler(bool onTarget);

    public override void _Ready()
    {
        base._Ready();
        _destination = _routine[0];
        _pathFollow = GetParent<NpcPathFollow>();
    }



    public void EmitIamOnAttackSignal() {
        EmitSignal(SignalName.IamOnAttack);
    }

    public void EmitIamOnTargetSignal(bool onTarget) {
        EmitSignal(SignalName.IamOnTarget, onTarget);
    }

    public void NextTask() { _indexRoutine++; }

    public void Fading(bool iVanishing, double process)
    { 
        Color controlModulate = Modulate;

        controlModulate.A += (float)((!iVanishing) ? process : -process);
        
        Modulate = controlModulate;
        _isHide = true;
    }


    #region Getters and Setters

    public Control ActionInterface { get => _actionInterface; }

    public bool IsHide { get => _isHide;  set => _isHide = value; }
    public NpcPathFollow PathFollow { get => _pathFollow;}

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
