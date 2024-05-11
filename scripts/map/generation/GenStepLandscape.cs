using Atomation.Resources;
using Atomation.Things;
using Godot;

namespace Atomation.Map;

public class GenStepLandscape : GenStep
{
    private HeightMap heightMap;
    private TemperatureMap temperatureMap;
    private MoistureMap moistureMap;

    private float shoreLine;
    private float deepWater;

    private float mountainEdge;
    private float mountainBase;

    public GenStepLandscape() : base(1)
    {
        heightMap = new HeightMap();
        temperatureMap = new TemperatureMap();
        moistureMap = new MoistureMap();
    }

    public override void RunStep()
    {
        heightMap.SetOffset(origin);
        temperatureMap.SetOffset(origin);
        moistureMap.SetOffset(origin);

        CalculateLandHeight();

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                Coordinate coordinate = new Coordinate(x, y, origin);
                Terrain terrain = new Terrain(coordinate);

                float height = heightMap.CalculateNoise(x, y);
                float temperature = temperatureMap.CalculateNoise(x, y, height);

                terrain.Elevation = height;
                terrain.Temperature = temperature;
                terrain.Moisture = moistureMap.CalculateNoise(x, y, height, temperature);

                ChooseTerrain(terrain);
            }
        }
        temperatureMap.PrintMinMax();
    }

    private void CalculateLandHeight()
    {
        shoreLine = mapData.SeaLevel + 0.1f;
        deepWater = mapData.SeaLevel - 0.2f;

        mountainBase = mapData.MountainHeight - 0.1f;
        mountainEdge = mountainBase - 0.1f;
    }

    /// <summary>
    /// decides the type of terrain
    /// </summary>
    private void ChooseTerrain(Terrain terrain)
    {
        Structure naturalStructure;

        if (terrain.Elevation < shoreLine)
        {
            CreateWaterBody(terrain);
            naturalStructure = null;
        }
        else if (mountainEdge < terrain.Elevation)
        {
            CreateMountain(terrain, out naturalStructure);
        }
        else
        {
            CreateLandMass(terrain);
            naturalStructure = null;
        }

        WorldMap.Instance.SetTerrain(terrain);
        if (naturalStructure != null)
        {
            WorldMap.Instance.SetStructure(naturalStructure);
        }
    }

    /// <summary>
    /// decides land type
    /// </summary>
    private void CreateLandMass(Terrain terrain)
    {
        Biome biome = defDatabase.GetBiome(terrain.Moisture, terrain.Temperature);
        if (biome == null)
        {
            // GD.Print($"no Biome");

            terrain.Configure(TerrainDef.Undefined());

            return;
        }

        TerrainDef def = biome == null ? null : biome.GetTerrain(terrain.Elevation);

        if (def == null)
        {
            terrain.Configure(TerrainDef.Undefined());
            // GD.Print($"{biome.Name} {terrain.Elevation}");
            return;
        }

        terrain.Configure(def);
    }

    /// <summary>
    /// decides land type
    /// </summary>
    private void CreateWaterBody(Terrain terrain)
    {
        if (terrain.Elevation < deepWater)
        {
            terrain.Configure(defDatabase.GetTerrainDef("Deep Water"));
        }
        else if (terrain.Elevation < mapData.SeaLevel)
        {
            terrain.Configure(defDatabase.GetTerrainDef("Shallow Water"));
        }
        else
        {
            terrain.Configure(defDatabase.GetTerrainDef("Sand"));
        }
    }

    /// <summary>
    /// decides mountain type
    /// </summary>
    private void CreateMountain(Terrain terrain, out Structure mountainWall)
    {
        if (terrain.Elevation > mapData.MountainHeight)
        {
            terrain.Configure(DefDatabase.GetInstance().GetTerrainDef("Slate"));
            mountainWall = new Structure(terrain.GetCoordinate());
            mountainWall.Configure(DefDatabase.GetInstance().GetStructureDef("Slate Wall"));
        }
        else if (terrain.Elevation > mountainBase)
        {
            terrain.Configure(DefDatabase.GetInstance().GetTerrainDef("Slate"));
            mountainWall = new Structure(terrain.GetCoordinate());
            mountainWall.Configure(DefDatabase.GetInstance().GetStructureDef("Slate Wall"));
            // mountainWall = null;
        }
        else
        {
            terrain.Configure(DefDatabase.GetInstance().GetTerrainDef("Gravel"));
            mountainWall = null;
        }
    }

}