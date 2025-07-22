using System.Text.RegularExpressions;
using Godot;
public partial class FollowingBackground : Sprite2D
{
	private Vampire _vampire;
	private int _offset = 2360;
	public override void _Ready()
	{
		//_offset = GlobalPosition;
		//GD.Print("[Fondo] Debbug: Offset: " + _offset);
		_vampire = GetTree().GetFirstNodeInGroup(NameGroups.Vampire) as Vampire;
	}

	public override void _PhysicsProcess(double delta)
	{
		return;
		Vector2 pos = _vampire.GlobalPosition;
		pos.X += _offset;
		GlobalPosition = new (pos.X, GlobalPosition.Y);
		GD.Print("[Fondo] Debbug: Siguiendo al jugador: " + GlobalPosition);
		GD.Print("[Fondo] Debbug: Posicion jugador: " + _vampire.GlobalPosition);
    }

}
