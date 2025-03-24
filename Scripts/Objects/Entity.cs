using Godot;

/**
	Entity class is the base of all character
		Extends of CharacterBody2D, since they will move and interact with the world
*/

public partial class Entity : CharacterBody2D
{

	// Entity health value
	[Export] private int _health;
	// Entity speed value
	[Export] private float _speed;
	// Entity force value
	[Export] private float _force;
	// Entity animation player object (for animations)
    [Export] private AnimationPlayer _animationPlayer;

	#region Getters y Setters
		// Get - Set entity health value
		public int Health {
			get => _health;
			set => _health = Mathf.Max(0, value);
		}

		// Get - Set entity speed value
		public float Speed {
			get => _speed;
			set => _speed = value;
		}

		// Get - Set entity force value
		public float Force {
			get => _force;
			set => _force = value;
		}

		// Get entity animation player object
		public AnimationPlayer AnimationPlayer {
			get => _animationPlayer;
		}
	#endregion

	

}
