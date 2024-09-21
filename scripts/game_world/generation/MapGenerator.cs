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

    private Vector2I genSize;
    private WorldSettings settings;
    private List<GenStep> genSteps;

    public MapGenerator(WorldSettings settings)
    {
        genSteps = new List<GenStep>();
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
        genSize = settings.WorldSize;
    }

    public void SetChunkMode(bool chunkMode)
    {
        if (chunkMode)
        {
            genSize = new Vector2I(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
        }
        this.chunkMode = chunkMode;
    }

    public bool InChunkMode()
    {
        return chunkMode;
    }

    /// <summary>
    /// generates new chunks based on the current configs, 
    /// assigning them to the maps chunk handler
    /// </summary>
    public void GenerateChunk(Vector2I genOffset, ChunkHandler chunkHandler)
    {
        if (chunkHandler == null)
        {
            GD.PushError("can't generate sense chunk handler not provided");
            return;
        }

        GeneratedNoiseMaps noiseMaps = new GeneratedNoiseMaps(settings, genSize, genOffset * Chunk.CHUNK_SIZE);
        GeneratedMapData generatedMapData = new GeneratedMapData(genOffset);

        genSteps.Add(new GenStepLandScape(noiseMaps, settings));
        genSteps.Add(new GenStepPlants(noiseMaps, settings));

        foreach (var genStep in genSteps)
        {
            genStep.RunStep(generatedMapData);
        }

        generatedMapData.GenerateChunk(chunkHandler);

    }

}