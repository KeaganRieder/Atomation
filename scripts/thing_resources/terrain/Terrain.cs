using System.Collections.Generic;
using Godot;

/// <summary>
/// terrain tiles diplay mode
/// Default = graphic
/// Height = height map
/// Heat = heat map
/// Moisture = moisture map
/// </summary>
public enum TerrainDispalyMode
{
    Default = 0,
    Height = 1,
    Heat = 2,
    Moisture = 3,
}

/// <summary>
/// defines what a terrain object is in the game world 
/// </summary>
public partial class Terrain : CompThing
{
    private float heightValue;
    private float heatValue;
    private float moistureValue;
    private Node2D terrainObj;

    private Gradient heatGradient;
    private Gradient heightGradient; 
    private Gradient moistureGradient;
    private ColorRect colorRect; //this is temporay and will be changed

    private Terrain northNeighbour; //up
    private Terrain southNeighbour; //down
    private Terrain westNeighbour; //left
    private Terrain eastNeighbour; //right

    private bool collidable;

    //consturctors
    public Terrain()
    {
        name = "Default";
        description = "";
    }
    public Terrain(Vector2 cords)
    {
        string name = $"Tile {cords}";
        Vector2 position = CordConversionUtility.CellSizeCords(cords);
        terrainObj = new Node2D() //maybe get rid of? and just make either a sprite or color rect
        {
            Name = name,
            Position = position,
        };
        colorRect = new ColorRect(){
           Size = new Vector2(WorldMap.CELL_SIZE,WorldMap.CELL_SIZE),
        };

        // parent.AddChild(terrainObj);
        terrainObj.AddChild(colorRect);

        heatGradient = new Gradient();
      
        // heatGradient.AddPoint(-1f,Colors.DarkRed); //hotest
        // heatGradient.AddPoint(-0.8f,Colors.Red); //hotest
        // heatGradient.AddPoint(-0.5f,Colors.Orange); //hotter
        // heatGradient.AddPoint(-0.2f,Colors.Yellow); // hot
        // // heatGradient.AddPoint(0.0f,Colors.Green); //cold
        // heatGradient.AddPoint(0.2f,Colors.SeaGreen); //cold
        // heatGradient.AddPoint(0.5f,Colors.Cyan); //colder
        // heatGradient.AddPoint(0.8f,Colors.Blue); //coldest
        // heatGradient.AddPoint(1f,Colors.DarkBlue);
      
        heatGradient.AddPoint(0f,Colors.DarkRed);
        heatGradient.AddPoint(0.18f,Colors.Orange);
        heatGradient.AddPoint(0.3f,Colors.Yellow); //cold
        heatGradient.AddPoint(0.5f,Colors.Green);
        heatGradient.AddPoint(0.6f,Colors.Cyan);
        heatGradient.AddPoint(0.8f,Colors.Blue); //coldest
        heatGradient.AddPoint(1f,Colors.DarkBlue);

        heightGradient = new Gradient();

        

        moistureGradient = new Gradient();    
    }
    public Terrain(TerrainDef config)
    {
        name = config.Name;
        description = config.Description;
        stats = config.CreateStats();
        Graphic = new Graphic(config.graphicData,terrainObj);
    }

    //getters and setters
    public override Graphic Graphic { get => graphic; set { graphic = value; } }
    // public TerrainDispalyMode DispalyMode{set{dispalyMode = value;}} //make call display mode function
  
    public float HeightValue { get => heightValue; set { heightValue = value; } }
    public float HeatValue { get => heatValue; set { heatValue = value; } }
    public float MoistureValue{ get => moistureValue; set { moistureValue = value; } }

    public Node2D TerrainObj { get => terrainObj; set { terrainObj = value; } }

    public Terrain NorthNeighbour{get => northNeighbour; set{northNeighbour = value;}}
    public Terrain SouthNeighbour{get => southNeighbour; set{southNeighbour = value;}}
    public Terrain WestNeighbour{get => westNeighbour; set{westNeighbour = value;}}
    public Terrain EastNeighbour{get => eastNeighbour; set{eastNeighbour = value;}}

    //functions
    public void Display(TerrainDispalyMode dispalyMode){
        //todo move functioion to graphic class
        Color color;
        if (dispalyMode == TerrainDispalyMode.Default)
        {
            color = DefaultColor(heightValue); //todo make graphic rather then color
        }
        else if (dispalyMode == TerrainDispalyMode.Height)
        {
            color = HeightColor(heightValue);
        }
        else if (dispalyMode == TerrainDispalyMode.Heat)
        {
            color = HeatColor(heatValue);
        }
        else
        {
            color = MoistureColor(moistureValue);
        }
        colorRect.Color = color;
    }
    private Color DefaultColor(float value){
        if (value < 0.1)
        {
            return new Color(Colors.DarkBlue);
        }
        if (value < 0.2)
        {
            return new Color(Colors.Blue);
        }
        if (value < 0.3)
        {
            return new Color(Colors.Yellow);
        }
        else if (value < 0.5)
        {
            return new Color(Colors.Green);
        }
        else if (value < .6)
        {
            return new Color(Colors.DarkGreen);
        }
        else 
        {
            return new Color(Colors.Gray);
        }
    }

    private Color HeatColor(float value){

        return heatGradient.Sample(value);
    }
    private Color HeightColor(float value){
        //todos
        return new Color(value,value,value);

       

    }
    private Color MoistureColor(float value){
        //todo
        return new Color(value,value,value);
    }
   


}