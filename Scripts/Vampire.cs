using Godot;
using System;
using System.Reflection.PortableExecutable;

public partial class Vampire : Entity
{
	[Export]
	public float JumpForce {get; set;}

    public void ReduceVelocity(float weightEntityCarried, float frictionFloor, int inclinationFloor) {
		
	}
}
