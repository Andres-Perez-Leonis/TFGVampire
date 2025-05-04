using Godot;
using System;

public partial class GuardMovingState : GuardStateBase
{
    public override void Start()
    {
        base.Start();
        _guard.Destination = _guard.CorpseToCheck.PathFollow.LastPassMarker;
    }


}
