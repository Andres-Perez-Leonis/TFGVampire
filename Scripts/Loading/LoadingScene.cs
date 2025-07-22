using Godot;
using Godot.Collections;

public partial class LoadingScene : Control
{
	private Array _progress = new();
	//private string _scene = 
	private ResourceLoader.ThreadLoadStatus _sceneLoadStatus;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<ProgressBar>("ProgressBar").ValueChanged += OnValueChange;
		GetNode<ProgressBar>("ProgressBar").Value = 0;
		ResourceLoader.LoadThreadedRequest("res://Scenes/MainScene.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_sceneLoadStatus = ResourceLoader.LoadThreadedGetStatus("res://Scenes/MainScene.tscn", _progress);
		//GD.Print(_progress);
		GetNode<ProgressBar>("ProgressBar").Value = ((double)_progress[0]) * 100;
	}

	private void OnValueChange(double value)
	{
		if (value == 100 && _sceneLoadStatus == ResourceLoader.ThreadLoadStatus.Loaded)
		{
			var scene = ResourceLoader.LoadThreadedGet("res://Scenes/MainScene.tscn");
			GetTree().CallDeferred("change_scene_to_packed", scene);
		} 
	}
	

}
