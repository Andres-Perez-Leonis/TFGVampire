using Godot;
using System;

public partial class SettingsBtn : Button
{
	private Control _settingMenu;
	public override void _Ready()
	{
		_settingMenu = Owner.GetParent().GetNode<Control>("./SettingsMenu");
		Pressed += _onPressed;
	}

	private void _onPressed()
	{
		_settingMenu.Visible = true;
		((Control)Owner).Visible = false;
	}
}
