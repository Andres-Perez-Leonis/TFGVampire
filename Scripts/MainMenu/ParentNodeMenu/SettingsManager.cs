using Godot;
using Godot.Collections;
public partial class SettingsManager : Control
{
	[Export] AudioStreamPlayer audioBtn;
	[Export] AudioStreamPlayer audioSwitch;
	[Export] AudioStreamPlayer audioRollOver;
	
	private const string options_path = "user://options.data";

	private Dictionary _options = new();

	private Dictionary _readOptions()
	{
		Dictionary options = null;
		if (FileAccess.FileExists(options_path))
		{
			var file = FileAccess.Open(options_path, FileAccess.ModeFlags.Read);
			options = (Dictionary)file.GetVar();
			file.Close();
			GD.Print("Options readed: " + options);
		}
		return options;
	}

	private void _writeOptions()
	{
		var file = FileAccess.Open(options_path, FileAccess.ModeFlags.Write);
		file.StoreVar(_options);
		file.Close();
		GD.Print("Options saved: " + _options);
	}

	private void _setOption()
	{


	}

	public override void _Ready()
	{
		GD.Print("Soy el control: " + Name);
		base._Ready();
		Array<Node> buttons = FindChildren("*", "Button", true, false);
		GD.Print("Los botones que he detectado son: " + buttons);
		foreach (Button button in buttons)
		{
			button.Pressed += (button is OptionButton) ? OnSwitchClick : OnButtonClick;
			GD.Print("Soy de tipo: " + button.GetType());
			button.MouseEntered += OnMouseEntered;
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
