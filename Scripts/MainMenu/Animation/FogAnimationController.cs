using Godot;
using System;

public partial class FogAnimationController : TextureRect
{
	[Export] private float _speed = 0.5f;
	[Export] private Vector2I _iniPos;
	[Export] private Vector2I _finalPos;

	public override void _Ready()
	{
		base._Ready();
		GlobalPosition = _iniPos;
    }


	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector2 pos = GlobalPosition;
		pos.X += (float)delta * _speed;
		Debug(pos);
		GlobalPosition = (pos.X > _finalPos.X) ? _iniPos : pos;

	}

	private void Debug(Vector2 pos)
	{
		GD.Print("[Debug] Soy " + Name + " - Mi posicion es: " + GlobalPosition);
		GD.Print("[Debug] Soy " + Name + " - Mi siguiente posicion deberia ser: " + pos);
		GD.Print("[Debug] Devuelve: " + ((pos.X > _finalPos.X) ? _iniPos : pos));
	}

}
