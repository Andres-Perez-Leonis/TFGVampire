using Godot;
using System;

public partial class Entity : CharacterBody2D
{

	[Export]
	private int _health  {get; set; }
	[Export]
	public float Speed {get; set;}
	[Export]
	public float Force {get; set;}


	public int Health {
		get => _health;
		set => _health = Math.Max(0, value);
	}

}
