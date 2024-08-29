namespace Atomation.GameMap;

using System.Collections.Generic;
using Atomation.Things;
using Godot;

/// <summary>
/// handles the game's map's generation. through
/// organizing genSteps to either generate chunks or 
/// a preview
/// </summary>
public class MapGenerator
{
    /// <summary>
    /// decides if generator configures it self to generate chunks [Chunk.CHUNK_SIZE X Chunk.CHUNK_SIZE]
    /// or if generator configures it self to generate map preview [PREVIEW_SIZE X PREVIEW_SIZE]
    /// also determines wether or not true size is total map size or just given
    /// </summary>
    private bool chunkMode;

    private Vector2I generationArea;
    private MapSettings settings;

    public MapGenerator(MapSettings settings)
    {
        this.settings = settings;
        chunkMode = false;
    }

    public void updateSize()
    {
        if (chunkMode)
        {
            GD.PushError("chunk Generation mode enabled size can't be changed");
            return;
        }
        generationArea = settings.worldSize;
    }

    public void SetChunkMode(bool chunkMode)
    {
        if (chunkMode)
        {
            generationArea = new Vector2I(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
        }
        this.chunkMode = chunkMode;
    }
    public bool InChunkMode()
    {
        return chunkMode;
    }

    /// <summary>
    /// run the games map generate
    /// </summary>
    public void GenerateMap(Vector2 areaOffset, ChunkHandler chunkHandler = null)
    {
        if (chunkHandler == null)
        {
            GD.PushError("can't generate sense chunk handler not provided");
            return;
        }

        GenerateNoiseMaps(areaOffset * Chunk.CHUNK_SIZE, out float[,] elevationMap, out float[,] temperatureMap, out float[,] moistureMap);

        GenStepLandScape genStepTerrain = new GenStepLandScape(elevationMap, temperatureMap, moistureMap);
        genStepTerrain.GenerateLandScape(out Terrain[,] generatedTerrain, out Structure[,] generatedMountains, areaOffset, generationArea);

        for (int x = 0; x < generationArea.X; x++)
        {
            for (int y = 0; y < generationArea.Y; y++)
            {
                Terrain terrain = generatedTerrain[x, y];
                Structure mountainWalls = generatedMountains[x, y];

                if (generatedTerrain[x, y] == null)
                {
                    GD.Print($"null At {x},{y}");
                }
                
                chunkHandler.GetChunk(areaOffset).SetTerrain(new Vector2(x,y), terrain);
                chunkHandler.GetChunk(areaOffset).SetStructure(mountainWalls);
            }
        }

    }

    /// <summary>
    /// generates noise maps which represents the elevation, temperature and
    /// moisture of given gen area at the offset
    /// </summary>
    private void GenerateNoiseMaps(Vector2 areaOffset, out float[,] elevation, out float[,] temperature, out float[,] moisture)
    {
        GradientMapGenerator GradientMapGenerator = new GradientMapGenerator();
        NoiseMapGenerator NoiseMapGenerator = new NoiseMapGenerator(settings.elevationMapConfigs);

        elevation = NoiseMapGenerator.Generate(areaOffset, generationArea);
        temperature = GradientMapGenerator.Run(areaOffset, generationArea, settings.worldSize, settings.trueCenter);
        temperature = GenerationUtil.GenerateTemperatureMap(generationArea, temperature, elevation, settings);

        NoiseMapGenerator = new NoiseMapGenerator(settings.rainfallMapConfigs);

        moisture = GenerationUtil.GenerateMoisture(generationArea, NoiseMapGenerator.Generate(areaOffset, generationArea), settings);
    }
}