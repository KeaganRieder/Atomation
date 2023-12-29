using System;
using Godot;

/// <summary>
/// A chunk is a 32 x 32 section of the map that contans
/// various values, it is eitehr loaded or unloaded depedning on where 
/// the player is, as well as other aspects
/// </summary>
public class Chunk
{
    public Node2D ChunkNode{get; set;}
    private Vector2 origin;  

    public Chunk(Vector2 cords)
    {
        origin = cords * MapData.ChunkSize; //setting chunsk global pos
        ChunkNode = new Node2D();       
    }

    
    //chunk rendering 
    public void UpdateChunk()
    {
        //todo
    }
    public bool IsRendered
    {
        get => IsRendered;
        set
        {
            IsRendered = value;
            UpdateChunk();
        }
    }
}

/*
 public GeneratedChunk GenerateChunk(Vector2 position, Transform chunk)
    {
        GeneratedChunk generatedChunk = new GeneratedChunk();
        PerlinNoiseMap perlinNoiseMap = new(MapData.CHUNK_SIZE, MapData.CHUNK_SIZE, 
        data.seed, noiseScale, octaves, data.avgElevation, lacunarity, position+ offset, NoiseMap.NormalizationMode.Global);
        perlinNoiseMap.ApplyCurve(heightCurve, heightCurveMultipler);
        float[,] elevationMap = perlinNoiseMap.GetNoiseMap();

        for (int x = 0; x < MapData.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < MapData.CHUNK_SIZE; y++)
            {
                float noiseValue = elevationMap[x, y];

                Color color = new Color(noiseValue, noiseValue, noiseValue);//get rid of

                int tileXCords = x + ((int)position.x);
                int tileYCords = y + ((int)position.y);
                Tile generatedTile = new Tile(x, y, chunk, sprite, color);

                generatedChunk.chunkGround.Add(new Vector2(tileXCords, tileYCords),generatedTile);

            }
        }
*/