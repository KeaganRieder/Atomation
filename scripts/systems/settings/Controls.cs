using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// class whcih handles defining keybidnings/defineing
/// actions
/// </summary>
public partial class Controls : Node
{
    public const string DEFAULT_BINDING_FILE = "data/settings/default_key_bindings.json"; //todo
    public const string BINDING_FILE = "data/settings/key_bindings.json";

    // todo add default bindings which can be reset to
    [JsonProperty]
    private Dictionary<string, int> keyBindings;

    [JsonProperty]
    private Dictionary<string, int> mouseBindings;
    public Controls(){
        
        keyBindings = new Dictionary<string, int>();
        mouseBindings = new Dictionary<string, int>();
        // keyBindings = new Dictionary<string, int>(){
        //     {"MoveLeft", 65},
        //     {"MoveRight", 68},
        //     {"MoveUp", 83},
        //     {"MoveDown", 87},
        // };
        // mouseBindings = new Dictionary<string, int>(){
        //     {"LeftClick", 1},
        //     {"RightClick", 2},
        //     {"ZoomIn", 4},
        //     {"ZoomOut", 5},
        // };

    }

    /// <summary>
    /// loads keys from a file called key_bindings.json, if exsits
    /// or default_key_bindings.json
    /// </summary>
    public void LoadBindings(){
        Controls loadedControls = JsonReader.ReadJson<Controls>(BINDING_FILE);
        keyBindings = loadedControls.keyBindings;
        mouseBindings = loadedControls.mouseBindings;
        foreach (var binding in keyBindings)
        {
            InputMap.AddAction(binding.Key);
            InputEventKey inputEventKey = new InputEventKey(){
                Keycode = (Key)binding.Value,
            };
            InputMap.ActionAddEvent(binding.Key, inputEventKey);
        }
        foreach (var binding in mouseBindings)
        {
            InputMap.AddAction(binding.Key);
            InputEventMouseButton inputButton = new InputEventMouseButton(){
                ButtonIndex = (MouseButton)binding.Value,
            };
            InputMap.ActionAddEvent(binding.Key, inputButton);
        }
    }

    //change keybindings--todo

}