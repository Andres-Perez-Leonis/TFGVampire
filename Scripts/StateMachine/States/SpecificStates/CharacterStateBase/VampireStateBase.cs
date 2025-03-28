using Godot;
using System;

public partial class VampireStateBase : StateBase
{
    /***
     * Reference to the Vampire node that this state controls.
     */
    protected Vampire _vampire;

    /***
     * Called when the node enters the scene tree for the first time.
     * Initializes the _vampire reference by casting the ControlledNode to a Vampire.
     */
    public override void _Ready()
    {
        base._Ready();
        _vampire = (Vampire) ControlledNode;
    }

    // Uncomment the following line if gravity is needed for this state:
    // private float _gravity = (float) ProjectSettings.GetSetting("physics/2d/default_gravity");

}
