using Godot;

public partial class ActionInterface : Control
{
    public override void _Ready()
    {
        Visible = false;
        Villager villager = Owner as Villager;
        villager.IamOnTarget += _villagerOnTarget;    
    }

    private void _villagerOnTarget(bool onTarget)
    {
        Visible = onTarget;
    }
}