using Godot;
using System;

public partial class HideOutPlace : HidingPoint
{
    private Vampire _vampire;

    public override int Interact()
    {
        int vampireFounded = 0;
        if (_vampire != null) vampireFounded = 1;
        return vampireFounded;
    }


    public override void _Ready()
    {
        base._Ready();
        _vampire = (Vampire) HidenNode;
    }

}
