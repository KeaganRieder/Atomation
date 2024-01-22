using Godot;

/// <summary>
/// base abstarct class for nosie maps
/// </summary>
public abstract class NoiseObject
{
    protected int seed;
    protected Vector2 mapOffset;
    protected float zoomLevel;
    
    public virtual int Seed{get => seed; set{seed = value;}}
    public virtual float ZoomLevel{get=>zoomLevel; set{zoomLevel = Mathf.Max(1,value);}}
    public virtual Vector2 Offset{get => mapOffset; set{mapOffset = value;}}
    public virtual float this[int x, int y]{
        get{
            return 0.0f;
        }
    }
    public virtual float this[Vector2 cords]{
        get{
            int x = Mathf.RoundToInt(cords.X);
            int y = Mathf.RoundToInt(cords.Y);
            return this[x, y];
        }
    }      
}