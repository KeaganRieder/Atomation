namespace Atomation.Map;

using System.Collections.Generic;
using Atomation.Things;
using Godot;
using Newtonsoft.Json;

public class SavedChunk
{
    public float CellSize;
    public bool Rendered;
    public Coordinate Cords;

    public List<SavedTerrain> SavedTerrain;
    public List <SavedStructure> SavedStructure;

    public SavedChunk(){}

    public SavedChunk(Chunk toSave)
    {
        Cords = toSave.coordinate;
        CellSize = toSave.CellSize;
        Rendered = toSave.Rendered;

        SavedTerrain = new List<SavedTerrain>();
        SavedStructure = new List<SavedStructure>();

        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                if (toSave.Terrain.GetObject(x, y) != null)
                {
                    SavedTerrain.Add  (new SavedTerrain(toSave.Terrain.GetObject(x, y)));
                }
                if (toSave.Buildings.GetObject(x, y) != null)
                {
                    SavedStructure.Add (new SavedStructure(toSave.Buildings.GetObject(x, y)));
                }
            }
        }

    }

    public void Load()
    {
        GD.Print("Loading Not Implemented");
    }
}