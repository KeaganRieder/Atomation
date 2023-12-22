using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/*
    defines a object which stores an object graphics data
    that is used to modify/create an objects trexure during
    runtime
*/
public enum EdgeType{
    Default,
    Rough, 
    Smooth,
}
public class GraphicData{
    // public Vector2 graphicSize{get; set;}
    public Color color {get; set;}
    // [JsonConverter(typeof(StringEnumConverter))]
    // public EdgeType edgeType {get; set;}

    //add more later

}