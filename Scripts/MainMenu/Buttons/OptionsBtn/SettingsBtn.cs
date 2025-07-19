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
		if (Owner is ParentNodeMenu c)
		{
			//c.OnButtonClick();
			c.Visible = false;
		}
		_settingMenu.Visible = true;
	}
}
