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
		GetParent().GetNode<OptionButton>("ViewMode").ItemSelected += _detectResolutionOnFullScreen;
	}

	private void _detectResolutionOnFullScreen(long item)
	{
		if (item == 0) return;

		Vector2I size = DisplayServer.WindowGetSize();
		for (int i = 0; i < _resolutions.Count; i++)
		{
			if (_resolutions[i][0] == size[0])
			{
				_resolutionSelected(i);
				return;
			}
		}

		_resolutions.Add(size);

	}

	private void _resolutionSelected(long option)
	{
		DisplayServer.WindowSetSize(_resolutions[(int)option]);
	}
}
