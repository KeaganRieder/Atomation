using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Resources;
using Newtonsoft.Json.Converters;

namespace Atomation
{
    /// <summary>
    /// handles the key and mouse bindings for controls in the game
    /// </summary>
    public class ControlBindings
    {
        [JsonProperty]
        private Dictionary<string, Key> keyBindings;

        public ControlBindings()
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

        }
        public ControlBindings(string path)
        {
            LoadBindings(path);
          
        }

        /// <summary>
        /// reads specified key bindings and assigns them to input manger
        /// </summary>
        public void LoadBindings(string bindingFile)
        {
            LoadMouseBindings();

            ControlBindings loadedBinding = FileManger.ReadJsonFile<ControlBindings>(FilePath.KEYBINDINGS_FOLDER, bindingFile);

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
        
        private void LoadMouseBindings(){
            InputEventMouseButton mouseButton;
            
            InputMap.AddAction("Left Click");
            InputMap.AddAction("Right Click");

            mouseButton = new InputEventMouseButton{
                ButtonIndex = MouseButton.Left,
            };
            InputMap.ActionAddEvent("Left Click",mouseButton);
            
            mouseButton = new InputEventMouseButton{
                ButtonIndex = MouseButton.Right,
            };
            InputMap.ActionAddEvent("Right Click",mouseButton);
        }
    }
}
