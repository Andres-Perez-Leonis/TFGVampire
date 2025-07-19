using Godot;
using Godot.Collections;
public partial class ParentNodeMenu : Control
{
	[Export] AudioStreamPlayer audioBtn;
	[Export] AudioStreamPlayer audioSwitch;
	[Export] AudioStreamPlayer audioRollOver;
	public override void _Ready()
	{
		GD.Print("Soy el control: " + Name);
		base._Ready();
		Array<Node> buttons = FindChildren("*", "Button", true, false);
		GD.Print("Los botones que he detectado son: " + buttons);
		foreach (Button button in buttons)
		{
			button.Pressed +=  (button is OptionButton) ? OnSwitchClick : OnButtonClick;
			GD.Print("Soy de tipo: " + button.GetType());
			button.MouseEntered +=OnMouseEntered;
		}

		
	}

	private void OnMouseEntered()
	{
		audioRollOver.Play();
	}

	private void OnButtonClick()
	{
		audioBtn.Play();
	}

	private void OnSwitchClick()
	{
		audioSwitch.Play();
	}
}
