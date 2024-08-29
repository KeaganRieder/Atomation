namespace Atomation.Settings;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Resources;

/// <summary>
/// stores the player input keybind, also defines function to allow fo r
/// rebinding and saving/loading keybinding settings to a file
/// </summary>
public class PlayerKeybindSettings
{
    [JsonProperty]
    private Dictionary<string, InputEvent> keybindings;

    public PlayerKeybindSettings()
    {
        DefaultBindings();
    }

    public void DefaultBindings()
    {
        keybindings = new Dictionary<string, InputEvent>{
            {"MoveLeft", new InputEventKey{PhysicalKeycode = Key.A}},
            {"MoveRight", new InputEventKey{PhysicalKeycode = Key.D}},
            {"MoveDown", new InputEventKey{PhysicalKeycode = Key.S}},
            {"MoveUp", new InputEventKey{PhysicalKeycode = Key.W}},
            {"ZoomIn", new InputEventKey{PhysicalKeycode = Key.Q}},
            {"ZoomOut", new InputEventKey{PhysicalKeycode = Key.E}},

            {"Interact", new InputEventMouseButton{ButtonIndex = MouseButton.Left}},
            {"GenerateNewMap", new InputEventKey{PhysicalKeycode = Key.R}},
            {"NewSeed", new InputEventKey{PhysicalKeycode = Key.T}},
            {"VisualizeDefault", new InputEventKey{PhysicalKeycode = Key.Key1}},
            {"VisualizeMoisture", new InputEventKey{PhysicalKeycode = Key.Key2}},
            {"VisualizeHeat", new InputEventKey{PhysicalKeycode = Key.Key3}},
            {"VisualizeHeight", new InputEventKey{PhysicalKeycode = Key.Key4}},

            {"QuickSave", new InputEventKey{PhysicalKeycode = Key.F5}},
            {"QuickLoad", new InputEventKey{PhysicalKeycode = Key.F6}},
            {"Inventory", new InputEventKey{PhysicalKeycode = Key.I}},
            {"Pause", new InputEventKey{PhysicalKeycode = Key.Escape}},
        };
        RemapBindings();
    }

    private void RemapBindings()
    {
        foreach (var binding in keybindings)
        {
            InputMap.AddAction(binding.Key);
            InputMap.ActionAddEvent(binding.Key, binding.Value);
        }
    }

    public void Remap(){
        //todo
    }


}