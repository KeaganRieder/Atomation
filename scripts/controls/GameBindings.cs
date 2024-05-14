namespace Atomation.Controls;

using System.Collections.Generic;
using Godot;

public class GameBindings
{
    private Dictionary<string, InputEvent> playerBindings;
    private Dictionary<string, InputEventKey> cameraBindings;
    private Dictionary<string, InputEventKey> worldBindings;
    private Dictionary<string, InputEventKey> UiBindings;

    public GameBindings()
    {
        DefaultBindings();
    }

    public void DefaultBindings()
    {

        playerBindings = new Dictionary<string, InputEvent>{

            {"MoveLeft", new InputEventKey{PhysicalKeycode = Key.A}},
            {"MoveRight", new InputEventKey{PhysicalKeycode = Key.D}},
            {"MoveDown", new InputEventKey{PhysicalKeycode = Key.S}},
            {"MoveUp", new InputEventKey{PhysicalKeycode = Key.W}},
            {"Interact", new InputEventMouseButton{ButtonIndex = MouseButton.Left}},
        };

        UiBindings = new Dictionary<string, InputEventKey>{
            {"QuickSave",new InputEventKey{PhysicalKeycode = Key.F5}},
            {"QuickLoad",new InputEventKey{PhysicalKeycode = Key.F6}},
            {"Inventory",new InputEventKey{PhysicalKeycode = Key.I}},
        };

        cameraBindings = new Dictionary<string, InputEventKey>
        {
            {"ZoomIn", new InputEventKey{PhysicalKeycode = Key.Q}},
            {"ZoomOut", new InputEventKey{PhysicalKeycode = Key.E}},
        };

        worldBindings = new Dictionary<string, InputEventKey>{
            {"GenerateNewMap", new InputEventKey{PhysicalKeycode = Key.R}},
            {"NewSeed", new InputEventKey{PhysicalKeycode = Key.T}},
            {"VisualizeDefault", new InputEventKey{PhysicalKeycode = Key.Key1}},
            {"VisualizeMoisture", new InputEventKey{PhysicalKeycode = Key.Key2}},
            {"VisualizeHeat", new InputEventKey{PhysicalKeycode = Key.Key3}},
            {"VisualizeHeight", new InputEventKey{PhysicalKeycode = Key.Key4}},
        };

        foreach (var binding in playerBindings)
        {
            InputMap.AddAction(binding.Key);

            InputMap.ActionAddEvent(binding.Key, binding.Value);
        }
        foreach (var binding in UiBindings)
        {
            InputMap.AddAction(binding.Key);
            InputMap.ActionAddEvent(binding.Key, binding.Value);
        }
        foreach (var binding in cameraBindings)
        {
            InputMap.AddAction(binding.Key);
            InputMap.ActionAddEvent(binding.Key, binding.Value);
        }
        foreach (var binding in worldBindings)
        {
            InputMap.AddAction(binding.Key);
            InputMap.ActionAddEvent(binding.Key, binding.Value);
        }
    }

    public void SwapBindings(){

    }
}