using Godot;
using System;

public partial class Volume : HSlider
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Value = AudioServer.GetBusVolumeDb(0);
		ValueChanged += _setVolumeValue;
	}

	private void _setVolumeValue(double value)
	{
		AudioServer.SetBusVolumeDb(0, (float)(value / 5));
	}

}
