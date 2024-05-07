namespace Atomation.Player;

using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

public class Bindings
{
    private Dictionary<string, Key> bindings;

    // DefaultBindings();
    public Bindings()
    {
        DefaultBindings();
        LoadBindings();
        LoadMouseBindings();
    }

    public void DefaultBindings()
    {
        bindings = new Dictionary<string, Key>(){
                {"GenerateNewMap", Key.R},
                {"NewSeed", Key.T},
                {"VisualizeDefault", Key.Key1},
                {"VisualizeMoisture", Key.Key2},
                {"VisualizeHeat", Key.Key3},
                {"VisualizeHeight", Key.Key4},
                {"ZoomIn", Key.Q},
                {"ZoomOut", Key.E},
                {"MoveLeft", Key.A},
                {"MoveRight", Key.D},
                {"MoveDown", Key.S},
                {"MoveUp", Key.W},
                {"QuickLoad",Key.F6},
                {"QuickSave",Key.F5},
            };
    }

    public void LoadBindings()
    {

        //assigning 
        foreach (var binding in bindings)
        {
            InputMap.AddAction(binding.Key);
            InputEventKey inputEventKey = new InputEventKey()
            {
                Keycode = binding.Value,
            };

            InputMap.ActionAddEvent(binding.Key, inputEventKey);
        }
    }

    private void LoadMouseBindings()
    {
        InputEventMouseButton mouseButton;

        InputMap.AddAction("LeftClick");
        InputMap.AddAction("RightClick");

        mouseButton = new InputEventMouseButton
        {
            ButtonIndex = MouseButton.Left,
        };
        InputMap.ActionAddEvent("LeftClick", mouseButton);

        mouseButton = new InputEventMouseButton
        {
            ButtonIndex = MouseButton.Right,
        };
        InputMap.ActionAddEvent("RightClick", mouseButton);
    }
}