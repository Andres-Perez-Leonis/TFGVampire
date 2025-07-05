using Godot;
using System;

public partial class VolverBtn : Button
{
	private Control _initMenu;
	public override void _Ready()
	{
		_initMenu = Owner.GetParent().GetNode<Control>("./InitMenu");
		Pressed += _onPressed;
	}

	private void _onPressed()
	{
		if (Owner is Control menu) menu.Visible = false;
		_initMenu.Visible = true;

	}
}
