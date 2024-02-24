using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Resources;
using Newtonsoft.Json.Converters;

namespace Atomation.System
{
    /// <summary>
    /// the keybindings in the game. defines what they are and 
    /// how to properly mange them.
    /// 
    /// key codes can be found here https://docs.godotengine.org/en/stable/classes/class_%40globalscope.html#enum-globalscope-key
    /// </summary>
    public class KeyBindings
    {
        [JsonProperty]
        private Dictionary<string, Key> keyBindings;

        public KeyBindings()
        {
        }

        /// <summary>
        /// used for formatting bindings files
        /// </summary>
        public void FormatFile(string fileName)
        {
            keyBindings = new Dictionary<string, Key>(){
                {"GenerateNewMap", Key.R},
                {"Default", Key.D},
                {"VisualizeMoisture", Key.M},
                {"VisualizeHeat", Key.T},
                {"VisualizeHeight", Key.E},
            };

            Resources.JsonWriter.WriteFile(FileManger.BINDINGS_FOlDER, fileName, this);
        }

        /// <summary>
        /// reads specified key bindings and assigns them to input manger
        /// </summary>
        public void LoadBindings(string bindingFile)
        {
            string filePath = FileManger.BINDINGS_FOlDER + bindingFile + ".json";

            KeyBindings loadedBinding = Resources.JsonReader.ReadJson<KeyBindings>(filePath);

            //assigning 
            foreach (var binding in loadedBinding.keyBindings)
            {
                InputMap.AddAction(binding.Key);
                InputEventKey inputEventKey = new InputEventKey()
                {
                    Keycode = binding.Value,
                };

                InputMap.ActionAddEvent(binding.Key, inputEventKey);
            }
        }
    }
}
