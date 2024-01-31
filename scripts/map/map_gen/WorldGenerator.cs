using System.Collections.Generic;
using Godot;

/// <summary>
/// class which is used to pass a collection of data that relates to the 
/// configuration of world world generation.
/// this data is passed between the many world generation classes
/// </summary>
public class GenConfigs
{
    //general configs
    /// <summary>
    /// bounderys/max size of the world
    /// x = width
    /// y = height
    /// </summary>
    public Vector2I worldBounds;
      
    //generation configs    
    public NoiseMapConfig elevationMapConfigs;
    public NoiseMapConfig moistureMapConfigs;
    public NoiseMapConfig heatMapConfigs;

    //terrain configs
    public float seaLevel = .2f;
    public float mounatinSize = .8f; //no mountains is anything above 1
    
}

/// <summary>
/// the games world generators, which manange and oversees
/// the running/executaion of genesteps to genertate the game
/// world or chunks with in it
/// </summary>
public class WorldGenerator
{
    //configs
    private GenConfigs genConfig;
    private GenerationData generationData;
    // genSteps
    private GenStepNoise genStepNoise;

    public WorldGenerator(GenConfigs genConfig){
        this.genConfig = genConfig;
        genStepNoise = new GenStepNoise(genConfig);
    }

    //getters and setters
    public GenConfigs GenConfig{get{return genConfig;} set{genConfig = value;}}
    public GenStepNoise GenStepNoise{get{return GenStepNoise;} set{GenStepNoise = value;}}

    public void ExecuteGenSteps(Vector2 origin, Node2D map){
        generationData = genStepNoise.RunStep(origin);
    }
///CordConversionUtility
    public GeneratedChunk GenerateChunk(Vector2 chunkCord, Node2D map){
        int tileID = 0; 
        ExecuteGenSteps(chunkCord,map);
        //todo make check for if at world bounds
        Dictionary<Vector2, Tile> generatedTerrain = new();
        Node2D ChunkNode = new Node2D(){
            Name = $"Chunk {chunkCord}",
            Position = chunkCord*WorldMap.CELL_SIZE
        };
        map.AddChild(ChunkNode);

        // GenData.ElevationMap.Offset = chunkCord;

        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                string ID = $"Tile{tileID}";
                float elevation = generationData.elevationMap[x, y];

                Vector2 cords = CordConversionUtility.CellSizeCords( x,  y);
                TileData tileData = new TileData(cords,elevation, 0, 0);
                Tile tile = new(tileData);
               
                ChunkNode.AddChild(tile);
                generatedTerrain.Add(cords,tile);
                tileID++;
            }
        }
        return new GeneratedChunk(generatedTerrain,ChunkNode);
    }  




}
