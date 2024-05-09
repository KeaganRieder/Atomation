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

    public List<Terrain> SavedTerrain;
    public List<Structure> SavedStructure;

    public SavedChunk() { }

    public SavedChunk(Chunk toSave)
    {
        Cords = toSave.Coordinate;
        CellSize = toSave.CellSize;
        Rendered = toSave.CheckVisibility();

        SavedTerrain = new List<Terrain>();
        SavedStructure = new List<Structure>();

        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                if (toSave.TerrainGrid.GetObject(x, y) != null)
                {
                    SavedTerrain.Add(toSave.TerrainGrid.GetObject(x, y));
                }
                if (toSave.StructureGrid.GetObject(x, y) != null)
                {
                    SavedStructure.Add(toSave.StructureGrid.GetObject(x, y));
                }
            }
        }

    }
}