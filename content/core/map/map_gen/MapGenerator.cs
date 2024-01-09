using System;
using System.Linq;
using Godot;


/// <summary>
/// manges world generation, by primarly define how the genSteps are carried out.
/// generates the map preveiw before finalizlation, which then it generates new
/// chunks when the chunk hanlder regensters a new one is laoded
/// </summary>

//todo make this be able to handle ui when that point is reached
public class MapGenerator
{
    public const int ELEVATION_MAP_IDX = 0;
    public const int MOISTURE_MAP_IDX = 1;
    public const int TEMPETURE_MAP_IDX = 2;
    // public const int ELEVATION_MAP_IDX = 0;

    

    public bool Update{get;set;}
    public bool Preveiw{get;set;}

    //generation configs
    private Vector2 MapSize;
    private NoiseObject[] noiseMaps;
    private int seed;
    private int octaves;
    private float zoomLevel;
    private float frequency;
    private float lacunarity;
    private float persistence;
   
    public MapGenerator(int width, int height){
        MapSize = new Vector2(width,height);
        noiseMaps = new NoiseObject[3]{
            new NoiseObject(),
            new NoiseObject(),
            new NoiseObject(),
        };
    }
    
    //geters and seters
    public int Seed{
        get => seed;
        set{
            seed = value;
            foreach (NoiseObject noiseObject in noiseMaps)
            {
                noiseObject.Seed = value;
            }
        } 
    }
    public int Octaves{
        get => octaves;
        set{
            octaves = value;
            foreach (NoiseObject noiseObject in noiseMaps)
            {
                noiseObject.Octaves = value;
            }
        } 
    }
    public float ZoomLevel{
        get => zoomLevel;
        set{
            zoomLevel = value;
            foreach (NoiseObject noiseObject in noiseMaps)
            {
                noiseObject.Zoom = value;
            }
        } 
    }
    public float Frequency{
        get => frequency;
        set{
            frequency = value;
            foreach (NoiseObject noiseObject in noiseMaps)
            {
                noiseObject.Frequency = value;
            }
        } 
    }
    public float Lacunarity{
        get => lacunarity;
        set{
            lacunarity = value;
            foreach (NoiseObject noiseObject in noiseMaps)
            {
                noiseObject.Lacunarity = value;
            }
        } 
    }
    public float Persistence{
        get => persistence;
        set{
            persistence = value;
            foreach (NoiseObject noiseObject in noiseMaps)
            {
                noiseObject.Persistence = value;
            }
        } 
    }
    
    //generation functions

    /// <summary>
    /// performs and final stemps to generation/ like spawning the player 
    /// </summary>
    public void FinalzieGenartion(){
        MapSize = default;
    }

    /// <summary>
    /// generates the map based on noise maps, after each update
    /// during the intial generation preveiw
    /// </summary>
    public void GenerateMap(WorldMap map){
        int tileID = 0; 
        for (int x = 0; x < MapSize.X; x++)
        {
            for (int y = 0; y < MapSize.X; y++)
            {
                // GD.Print("test");
                string ID = $"Tile{tileID}"; 
                float elevation = noiseMaps[ELEVATION_MAP_IDX][x,y];
                Color color = GetColor(elevation);//(elevation,elevation,elevation)
                Graphic graphic = new Graphic("",color);
                
                Terrain terrain = new Terrain(ID, "", null, graphic){
                    Position = new Vector2(x*WorldMap.CELL_SIZE, y*WorldMap.CELL_SIZE),
                };
                terrain.AddChild(graphic.GetTexture());
                map.AddChild(terrain);
                tileID++;
            }
        }        
    }

    public Color GetColor(float noiseVal){
        //below zero
        if (noiseVal <= 0)
        {
            if (noiseVal < -.6)
            {
                GD.Print($"below {noiseVal}");
            }
            // GD.Print($"below {noiseVal}");
            float temp = Math.Abs(noiseVal);
            return new Color(temp,temp,200);
        }

        //above .6
        else
        {
            return new Color(50,noiseVal,noiseVal);
        }
    }


    /// <summary>
    /// called by chunkhandler to generate a new chunk, if it finds
    /// an area loaded that needs a new chunk
    /// </summary>
    public Chunk GenerateChunk(){
        //todo
        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                //generating terrain
                //deciding biome 

                // float currentElev = noiseMaps[ELEVATION_MAP_IDX]
                // //then run through height
                // for (int i = 0; i < noiseMaps[e]; i++)
                // {
                    
                // }
            }
        }
        return default;
    }
    

   
}