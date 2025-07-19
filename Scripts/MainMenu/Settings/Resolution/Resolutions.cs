using Godot;
using Godot.Collections;

public partial class Resolutions : OptionButton
{
	private Array<Vector2I> _resolutions = new();
	public override void _Ready()
	{
		_resolutions.Add(new(1920, 1080));
		_resolutions.Add(new(1600, 900));
		_resolutions.Add(new(1280, 720));
		ItemSelected += _resolutionSelected;
		Owner.GetNode<OptionButton>("VBoxContainer/VBoxContainer3/ViewMode").ItemSelected += _detectResolutionOnFullScreen;
	}

	private void _detectResolutionOnFullScreen(long item)
	{
		//GD.Print("Selection Detected: " + item);
		if (item == 0) return;

		Vector2I size = DisplayServer.WindowGetSize(DisplayServer.WindowGetCurrentScreen());
		//GD.Print("Comprueba cual es");
		for (int i = 0; i < _resolutions.Count; i++)
		{
			if (_resolutions[i][0] == size[0])
			{
				//GD.Print("Se ha determinado que es: " + _resolutions[i]);
				_resolutionSelected(i);
				return;
			}
		}

		//GD.Print("Nuevo detectado");
		_resolutions.Add(size);
		_resolutionSelected(_resolutions.Count - 1);
	}

	private void _resolutionSelected(long option)
	{
		//if (Owner is ParentNodeMenu c) c.OnSwitchClick();
		DisplayServer.WindowSetSize(_resolutions[(int)option]);
	}
}
