using Godot;
using System;

public partial class Personality : Resource
{
    public bool EasyInfluenced { get; set; }
    public bool BelieveInMisticysm { get; set; }
    public bool Gossipy { get; set; }
    public bool InMadness { get; set; }
    public bool Brave { get; set; }
    public bool Prudent { get; set; }
    public Personality() : this(false, false, false, false, false, false) { }

    public Personality(bool easyInfluenced, bool believeInMisticysm, bool gossipy, bool brave, bool prudent,bool inMadness = false)
    {
        EasyInfluenced = easyInfluenced;
        BelieveInMisticysm = believeInMisticysm;
        Gossipy = gossipy;
        InMadness = inMadness;
        Brave = brave;
        Prudent = prudent;
    }

}
