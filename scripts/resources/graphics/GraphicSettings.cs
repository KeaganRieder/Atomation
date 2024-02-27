using Godot;
using Newtonsoft.Json;

namespace Atomation.Resources
{   
     /// <summary>
    /// graphic configuration file
    /// </summary>
    public class GraphicConfig
    {
        public string TexturePath{get; set;}
        public Color Color {get; set;}

        public GraphicConfig(string texturePath,Color color){
            TexturePath = texturePath;
            Color = color;            
        }
    }
}