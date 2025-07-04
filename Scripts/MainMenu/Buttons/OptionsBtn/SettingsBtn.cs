using Godot;
using System;

public partial class SettingsBtn : Button
{
	[Export] private Control _settingMenu;
	public override void _Ready()
	{
		Pressed += _onPressed;
	}

	private void _onPressed()
	{
		_settingMenu.Visible = true;
	}
}
