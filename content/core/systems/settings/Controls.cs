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
    private Dictionary<string,int> keyBindings;

    public Controls(){
        keyBindings = new Dictionary<string, int>();
        // keyBindings = new Dictionary<string, int>(){
        //     {"MoveLeft", 65},
        //     {"MoveRight", 68},
        //     {"MoveUp", 83},
        //     {"MoveDown", 87},
        // };
        // InputMap.ActionAddEvent("hey",InputEventMouse.new())
    }

    public override void  _Ready(){
        LoadKeys();
        base._Ready();
    }

    /// <summary>
    /// loads keys from a file called key_bindings.json, if exsits
    /// or default_key_bindings.json
    /// </summary>
    public void LoadKeys(){
        Controls loadedControls = JsonReader.ReadJson<Controls>(BINDING_FILE);
        keyBindings = loadedControls.keyBindings;
        foreach (var binding in keyBindings)
        {
            InputMap.AddAction(binding.Key);
            // GD.Print($"setting binding: {binding.Key} to be {(Key)binding.Value} ");

            InputEventKey inputEventKey = new InputEventKey(){
                Keycode = (Key)binding.Value,
            };


            InputMap.ActionAddEvent(binding.Key, inputEventKey);
        }
    }

    //change keybindings--todo

}