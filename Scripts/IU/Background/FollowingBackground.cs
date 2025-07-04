using System.Text.RegularExpressions;
using Godot;
public partial class FollowingBackground : Sprite2D
{
	private Vampire _vampire;
	private Vector2 _offset;
	public override void _Ready()
	{
		_offset = GlobalPosition;
		GD.Print("[Fondo] Debbug: Offset: " + _offset);
		_vampire = GetTree().GetFirstNodeInGroup(NameGroups.Vampire) as Vampire;
	}

	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition = _vampire.GlobalPosition + _offset;
		GD.Print("[Fondo] Debbug: Siguiendo al jugador: " + GlobalPosition);
		GD.Print("[Fondo] Debbug: Posicion jugador: " + _vampire.GlobalPosition);
    }

}
