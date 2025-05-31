using Godot;
using System;

public partial class Personality : Resource
{
    [Export] public bool _easyInfluenced { get; set; }
    [Export] public bool _believeInMisticysm { get; set; }
    [Export] public bool _gossipy { get; set; }
    [Export] public bool _inMadness { get; set; }
    [Export] public bool _brave { get; set; }
    [Export] public bool _prudent { get; set; }
    public Personality() : this(false, false, false, false, false, false) { }

    public Personality(bool easyInfluenced, bool believeInMisticysm, bool gossipy, bool inMadness, bool brave, bool prudent)
    {
        _easyInfluenced = easyInfluenced;
        _believeInMisticysm = believeInMisticysm;
        _gossipy = gossipy;
        _inMadness = inMadness;
        _brave = brave;
        _prudent = prudent;
    }

}
