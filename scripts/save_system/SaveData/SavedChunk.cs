namespace Atomation.Map;

using System.Collections.Generic;
using Atomation.Things;
using Godot;
using Newtonsoft.Json;

public class SavedChunk
{
    public float CellSize;
    public bool Rendered;
    public ChunkCoordinate Cords;

    public List<SavedTerrain> SavedTerrain;
    public List <SavedStructure> SavedStructure;

    public SavedChunk(){}

    public SavedChunk(Chunk toSave)
    {
        Cords = toSave.Coordinate;
        CellSize = toSave.CellSize;
        Rendered = toSave.CheckVisibility();

        SavedTerrain = new List<SavedTerrain>();
        SavedStructure = new List<SavedStructure>();

        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                if (toSave.TerrainGrid.GetObject(x, y) != null)
                {
                    SavedTerrain.Add  (new SavedTerrain(toSave.TerrainGrid.GetObject(x, y)));
                }
                if (toSave.StructureGrid.GetObject(x, y) != null)
                {
                    SavedStructure.Add (new SavedStructure(toSave.StructureGrid.GetObject(x, y)));
                }
            }
        }

    }

    public void Load()
    {
        GD.Print("Loading Not Implemented");
    }
}