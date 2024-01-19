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
    public int MaxMapWidth{get;set;}
    private int MaxMapHeight{get;set;}
    private int seed;
    private float zoomLevel = 1;

    //noiseMaps
    private NoiseMap elevationMap;
    // private NoiseCombiner heatMap;
    // private NoiseMap moistureMap;

    public WorldGenerator(int width, int height){
        
        MaxMapWidth = width;
        MaxMapHeight = height;

        // make defaults at some point
        // new NoiseMap(int seed, Vector2 seedOffset, float zoomLevel, int octaves, 
        // float frequency, float persistence, float lacunarity)
        elevationMap = new NoiseMap();
    }

    public int Seed{
        get => seed;
        set{
            seed = value;
            elevationMap.Seed = value;
        }
    }
    public float ZoomLevel{
        get => zoomLevel;
        set{
            zoomLevel = value;
            elevationMap.ZoomLevel = value;
        }
    }
    public int HeightOctaves{
        get => elevationMap.Octaves;
        set{
            elevationMap.Octaves = value;
        }
    }
    public float HeightFrequency{
        get => elevationMap.Frequency;
        set{
            elevationMap.Frequency = value;
        }
    }
    public float HeightPersistence{
        get => elevationMap.Persistence;
        set{
            elevationMap.Persistence = value;
        }
    }
    public float HeightLacunarity{
        get => elevationMap.Lacunarity;
        set{
            elevationMap.Lacunarity = value;
        }
    }

    public Vector2 NormalizeCords(int x, int y){
        float xCord = x * WorldMap.CELL_SIZE;
        float yCord = y * WorldMap.CELL_SIZE;
        return new Vector2(xCord,yCord);
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

        elevationMap.Offset = chunkCord;

        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                string ID = $"Tile{tileID}";
                float elevation = elevationMap[x, y];

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
