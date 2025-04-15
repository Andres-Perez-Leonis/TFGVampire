using Godot;
using System;

public partial class UserInterface : Control, IFading
{
    public void Fading(bool iVanishing, double process) {
        Color controlModulate = Modulate;
        controlModulate.A += (float)(iVanishing ? process : -process);
        if(controlModulate.A < 0 || controlModulate.A > 1) return;
        Modulate = controlModulate;
    }
}
