using Godot;
using System.Collections.Generic;

public partial class VillagerResourceData : Resource
{
    public bool IsHide { get; set; }
    public bool IsDead { get; set; }
    public Personality Personality { get; set; }
    public MarkerPathSwitch LastPassMarker { get; set; }
    public List<EntitySuspechData> EntitySuspechDatas { get; set; }


    public VillagerResourceData(Villager villager)
    {
        IsHide = villager.IsHide;
        IsDead = villager.IsDeath;
        Personality = villager.Personality;
        LastPassMarker = villager.PathFollow.LastPassMarker;
        EntitySuspechDatas = villager.SuspicionSystem.SuspechEntities;
    }
}
