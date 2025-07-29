using Godot;
using Godot.Collections;

public partial class LoadingScene : Control
{
	private Array _progress = new();
	private ResourceLoader.ThreadLoadStatus _sceneLoadStatus;
	private bool _isLoadingFinalScene = false;
	private float _fakeProgress = 0f;
	private const string SCENE_PATH = "res://Scenes/MainScene.tscn";

	public override void _Ready()
	{
		 var animPlayer = GetNode<AnimationPlayer>("TextureRect/AnimationPlayer");
		animPlayer.Play("Loading");
		GetNode<ProgressBar>("ProgressBar").ValueChanged += OnValueChange;
		GetNode<ProgressBar>("ProgressBar").Value = 0;
		ResourceLoader.LoadThreadedRequest(SCENE_PATH);
	}

	public override void _Process(double delta)
	{
		if (!_isLoadingFinalScene)
		{
			_sceneLoadStatus = ResourceLoader.LoadThreadedGetStatus(SCENE_PATH, _progress);

			// El progreso reportado por Godot llega solo hasta 0.5 (50%)
			double actualProgress = (double)_progress[0];
			GetNode<ProgressBar>("ProgressBar").Value = actualProgress * 100;

			if (_sceneLoadStatus == ResourceLoader.ThreadLoadStatus.Loaded)
			{
				_isLoadingFinalScene = true;
			}
		}
		else
		{
			// Simular el progreso de 50% a 100% (ajustable)
			_fakeProgress += (float)(delta * 50); // velocidad de simulación
			_fakeProgress = Mathf.Clamp(_fakeProgress, 0, 50);
			GetNode<ProgressBar>("ProgressBar").Value = 50 + _fakeProgress;

			if (_fakeProgress >= 50)
			{
				CallDeferred(nameof(LoadFinalScene));
			}
		}
	}

	private void LoadFinalScene()
	{
		var scene = ResourceLoader.LoadThreadedGet(SCENE_PATH);
		GetTree().ChangeSceneToPacked((PackedScene)scene);
	}

	private void OnValueChange(double value)
	{
		// Puedes usarlo si necesitas mostrar algún efecto cuando suba el valor
	}
}
