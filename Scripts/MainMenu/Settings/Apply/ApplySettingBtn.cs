using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class ApplySettingBtn : Button
{
	// Called when the node enters the scene tree for the first time.
	private Dictionary settings;
	public static bool SomethingChanged = false;
	public override void _Ready()
	{
	}

	private void _obteinModifications()
	{
		Array<Node> settings = FindChildren("*", "Control", true, false);
		string name;
		Variant value;
		foreach (Node s in settings)
		{
			name = s.Name;
		}
	}
}
