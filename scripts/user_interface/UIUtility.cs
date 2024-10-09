namespace Atomation.Ui;

using Godot;

public static class UIUtility
{
    public static MarginContainer AddMargin(this MarginContainer container, int padding){
        foreach (var margin in new string[]{ "left", "right", "top", "bottom" })
        {
            container.AddThemeConstantOverride($"margin_{margin}",padding);
        }

        return container;
    }
}