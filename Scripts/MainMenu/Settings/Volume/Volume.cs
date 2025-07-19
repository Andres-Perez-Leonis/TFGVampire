using Godot;
using System;

public partial class Volume : HSlider
{
	[Export] private Label _dbLabel;
	 public override void _Ready()
	{
		// Configuración inicial del slider
		MinValue = 0;
		MaxValue = 100;
		Step = 1;

		// Establece el valor inicial convirtiendo de dB a escala lineal
		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(0)) * 100;
		_dbLabel.Text = Value.ToString();
		ValueChanged += OnVolumeChanged;
	}

    private void OnVolumeChanged(double value)
    {
        // Convierte el valor del slider (0-100) a dB
        float volumeDb;
        
        if (value <= 0)
        {
            // Silencio completo
            volumeDb = -80; // Godot mutea automáticamente bajo -30dB
            AudioServer.SetBusMute(0, true);
        }
        else
        {
            AudioServer.SetBusMute(0, false);
            // Conversión a escala logarítmica (dB)
            volumeDb = Mathf.LinearToDb((float)value / 100f);
        }
		_dbLabel.Text = value.ToString();
        GD.Print($"Setting volume: {value}% -> {volumeDb}dB"); // Debug

        AudioServer.SetBusVolumeDb(0, volumeDb);
    }
}

