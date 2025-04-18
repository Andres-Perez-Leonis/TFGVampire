using Godot;
using System;

public partial class HidingPointDetector : Area2D
{
    private HidingPoint _hidingPoint;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }


    private void OnAreaEntered(Area2D area) {
        //if(node is not Vampire) return;
        // Se controlará con el mask, por lo que no reaccionará con los NPCs
        _hidingPoint = (HidingPoint) area;
        GD.Print("He detectado una zona de escondite");
    }

    public HidingPoint HidingPoint { get => _hidingPoint; }
}
