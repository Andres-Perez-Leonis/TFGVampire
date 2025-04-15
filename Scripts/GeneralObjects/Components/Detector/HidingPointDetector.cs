using Godot;
using System;

public partial class HidingPointDetector : Area2D
{
    private CorpseHidingPoint _corpseHidingPoint;
    private HideOutPlace _hideOutPlace;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }


    private void OnAreaEntered(Area2D area) {
        //if(node is not Vampire) return;
        // Se controlará con el mask, por lo que no reaccionará con los NPCs
        if(area is CorpseHidingPoint) _corpseHidingPoint = (CorpseHidingPoint)area;
        if(area is HideOutPlace) _hideOutPlace = (HideOutPlace)area;
        GD.Print("He detectado una zona de escondite");
    }

    public CorpseHidingPoint CorpseHidingPoint { get => _corpseHidingPoint; }
    public HideOutPlace HideOutPlace { get => _hideOutPlace; }
}
