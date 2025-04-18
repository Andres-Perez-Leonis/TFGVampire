using Godot;
using System;

public partial class HideOutPlace : HidingPoint
{
    private Vampire _vampire;

    public override int Interact()
    {
        throw new NotImplementedException();
    }


    public override void _Ready()
    {
        base._Ready();
        _vampire = (Vampire) HidenNode;
    }

}
