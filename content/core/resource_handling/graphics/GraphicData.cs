
/*
    defines a object which stores an object graphics data
    that is used to modify/create an objects trexure during
    runtime
*/
using Godot;

public enum EdgeType{
    Default,
    Rough, 
    Smooth,
}
public class GraphicData{
     public Vector2 graphicSize;
    public Color color;
    public EdgeType edgeType;

    //add more later

}