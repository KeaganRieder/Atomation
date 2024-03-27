using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Resources;
using Newtonsoft.Json.Converters;

namespace Atomation
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
        public KeyBindings(string path)
        {
            LoadBindings(path);
        }

        /// <summary>
        /// formats a key binding file using default setting/configuration
        /// </summary>
        public void FormatFile(string fileName)
        {
            keyBindings = new Dictionary<string, Key>(){
                {"GenerateNewMap", Key.H},
                {"Default", Key.F},
                {"VisualizeMoisture", Key.M},
                {"VisualizeHeat", Key.T},
                {"VisualizeHeight", Key.R},
                {"ZoomIn", Key.Q},
                {"ZoomOut", Key.E},
                {"MoveLeft", Key.A},
                {"MoveRight", Key.D},
                {"MoveDown", Key.S},
                {"MoveUp", Key.W},
            };

            FileManger.WriteJsonFile(FilePath.KEYBINDINGS_FOLDER, fileName, this);
        }

        /// <summary>
        /// reads specified key bindings and assigns them to input manger
        /// </summary>
        public void LoadBindings(string bindingFile)
        {
            KeyBindings loadedBinding = FileManger.ReadJsonFile<KeyBindings>(FilePath.KEYBINDINGS_FOLDER, bindingFile);

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
