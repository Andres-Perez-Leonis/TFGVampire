using System.Runtime.Serialization;
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
    private bool _isHide = false;
    // Waypoint which indicate the NPC workplace
    [Export] private MarkerPathSwitch _workPlace;

    [Export] private Control _actionInterface;
    [Signal] public delegate void IamOnAttackEventHandler();
    [Signal] public delegate void IamOnTargetEventHandler(bool onTarget);


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

        // Get the current action of Routine
        public MarkerPathSwitch CurrentAction { get => _routine[_indexRoutine]; }

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
            set => _indexRoutine++;
        }

    #endregion

}
