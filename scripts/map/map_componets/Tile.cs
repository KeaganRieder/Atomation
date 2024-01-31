using Godot;

/// <summary>
/// struct used to pass tile data to newly created tiles
/// </summary>
public struct TileData{
    public float Height {get; set;}
    public float Heat {get; set;}
    public float Moisture {get; set;}
    public Vector2 Position{get; set;}

    public TileData(Vector2 position,float height, float heat, float moisture){
        Height = height;
        Heat = heat;
        Moisture = moisture;
        Position= position;
    }
}

/// <summary>
/// this is the visual represntation of certain 'thing_resources like 
/// the terrain, also holds x,y cords and other usful data
/// </summary>
public partial class Tile : Node2D
{
    private float height;
    private float heat;
    private float moisture;

    private Terrain tileResource;
    
    public Tile(TileData data){
        Name = $"Tile {data.Position}";
        Position = data.Position;

        height = data.Height;
        heat = data.Moisture;
        moisture = data.Moisture;
        tileResource = null;
        DisplayMode();
    }
    public Tile(TileData data, Terrain resource) : this(data){       
        tileResource = resource;
    }

    public Terrain Resource{get => tileResource; set{tileResource = value;}}

    public void DisplayMode(){
        //todo add differnt types of display modes (to be used during debuging and other stuff)
        AddChild(new ColorRect(){
            Color = new Color(height,height,height),
            Size = new Vector2(WorldMap.CELL_SIZE,WorldMap.CELL_SIZE),
        });
    }
}