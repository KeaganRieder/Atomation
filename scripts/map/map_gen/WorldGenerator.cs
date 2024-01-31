using System.Collections.Generic;
using Godot;

/// <summary>
/// the games world generators, which manange and oversees
/// the running/executaion of genesteps to genertate the game
/// world or chunks with in it
/// </summary>
public class WorldGenerator
{
    //general values
    // public int MaxMapWidth{get;set;}
    // private int MaxMapHeight{get;set;}
    // private int seed;
    // private float zoomLevel = 1;
    private GenData genData;
    private GenerationData generationData;

    public WorldGenerator(GenData genData){
        this.genData = genData;
    }
    public GenData GenData{get=>genData; set{genData = value;}}
   
    public Vector2 NormalizeCords(int x, int y){
        float xCord = x * WorldMap.CELL_SIZE;
        float yCord = y * WorldMap.CELL_SIZE;
        return new Vector2(xCord,yCord);
    }

    public void ExecuteGenSteps(){
        //todo
    }

    public GeneratedChunk GenerateChunk(Vector2 chunkCord, Node2D map){
        int tileID = 0; 
        
        //todo make check for if at world bounds
        Dictionary<Vector2, Tile> generatedTerrain = new();
        Node2D ChunkNode = new Node2D(){
            Name = $"Chunk {chunkCord}",
            Position = chunkCord*WorldMap.CELL_SIZE
        };
        map.AddChild(ChunkNode);

        GenData.ElevationMap.Offset = chunkCord;

        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                string ID = $"Tile{tileID}";
                float elevation = GenData.ElevationMap[x, y];

                Vector2 cords = NormalizeCords( x,  y);
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
