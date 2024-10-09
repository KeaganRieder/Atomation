namespace Atomation.GameSettings;
using Godot;

public class SettingsElement
{
	public string Name { get; set; }
}
/// <summary>
/// collection of values used to configure a slider ui element
/// </summary>
public class SettingsSlider : SettingsElement
{
    public float Value { get; set; }
    public float MinValue { get; set; }
    public float MaxValue { get; set; }
    public float Step { get; set; }
}

/// <summary>
/// used to configure text input ui elements
/// </summary>
public class SettingsLineEdit : SettingsElement
{
	public string Value { get; set; }
}

/// <summary>
/// used to configure checkbox ui elements 
/// </summary>
public class SettingsToggle : SettingsElement
{
	public bool Toggled { get; set; }
}