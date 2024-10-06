namespace Atomation.GameMap;

using Atomation.Things;
using Godot;

/// <summary>
/// defines the step in the generation of the game map, which generates
/// the games landscape
/// </summary>
public class GenStepLandScape : GenStep
{
    private WorldSettings configs;
    public GenStepLandScape() { }

    /// <summary>
    /// runs the genStep and generates the landscape,
    /// consisting of mountains, lakes, rivers, other water bodies and landmass
    /// </summary>
    public override void RunStep(GenStepData genStepData)
    {
        configs = genStepData.GenStepConfigs;
        SetSize(genStepData.GenSize);

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                float elevation = genStepData.GeneratedNoiseMaps.ElevationMap[x, y];
                float temperature = genStepData.GeneratedNoiseMaps.TemperatureMap[x, y];
                float moisture = genStepData.GeneratedNoiseMaps.MoistureMap[x, y];
                Terrain generatedTile;

                Structure generatedMountainWall = default;

                if (elevation > configs.MountainSize - .08)
                {
                    if (elevation >= configs.MountainSize)
                    {
                        generatedMountainWall = GetMountainWall(x, y);
                    }
                    generatedTile = GetMountainFloorTile(x, y);
                }
                else if (elevation > configs.WaterLevel)
                {
                    generatedTile = GetTerrainTile(x, y,temperature,moisture);
                }
                else
                {
                    generatedTile = GetWaterTile(x, y,elevation);
                }
                generatedTile.Elevation = elevation;
                generatedTile.Temperature = temperature;
                generatedTile.Moisture = moisture;

                genStepData.GeneratedStructures[x, y] = generatedMountainWall;
                genStepData.GeneratedTerrain[x, y] = generatedTile;
            }
        }
    }

    /// <summary>
    /// decides teh mountain wall at the given x, y cords
    /// </summary>
    private Structure GetMountainWall(int x, int y)
    {
        Structure mountainWall = new Structure(new Vector2(x, y));
        if (mountainWall == null)
        {
            GD.Print("null");
        }
        mountainWall.Configure("Slate Wall");

        return mountainWall;
    }

    /// <summary>
    /// decides the mountain floor tile at the given x y cords 
    /// </summary>
    public Terrain GetMountainFloorTile(int x, int y)
    {
        Terrain mountainFloorTile = new Terrain(new Vector2(x, y));

        mountainFloorTile.Configure("Slate");

        return mountainFloorTile;
    }

    /// <summary>
    /// decides what terrain is at the given x, y cords based
    /// on moisture and temperature map 
    /// </summary>
    private Terrain GetTerrainTile(int x, int y, float temperature, float moisture)
    {
        Terrain terrainFloorTile = new Terrain(new Vector2(x, y));

        terrainFloorTile.Configure(SelectBiome(x, y, temperature, moisture));

        return terrainFloorTile;
    }

    /// <summary>
    /// decides what water is at the given x, y cords based
    /// on the elevation
    /// </summary>
    private Terrain GetWaterTile(int x, int y, float elevation)
    {
        Terrain waterTile = new Terrain(new Vector2(x, y));
        if (elevation > configs.WaterLevel - .1)
        {
            waterTile.Configure("Shallow Ocean");
            return waterTile;
        }
        else
        {
            waterTile.Configure("Deep Ocean");
            return waterTile;
        }
    }

    /// <summary>
    /// selects the cells biome and terrain type based on
    /// the cells temperature and moisture
    /// </summary>
    private string SelectBiome(int x, int y, float temperature, float moisture)
    {
        // temp:-.8 to .6 moisture:-.8 to .8
        // cold
        if (temperature < -.15f)
        {
            if (moisture > 0.0)
            {
                // taiga
                // return new Color(0.3f, 0.4f, 0.3f);
                return "Taiga Soil";
            }
            else
            {
                // tundra
                // return Colors.LightBlue;
                return "Ice";
            }
        }
        //temperate
        else if (temperature < .25f)
        {
            if (moisture > .4)
            {
                // Seasonal Forest
                // return new Color(0.2f, 0.7f, 0.2f);
                return "Forest Grass";
            }
            else if (moisture > 0)
            {
                // grass land
                // return Colors.LightGreen;
                return "Grass";
            }
            else
            {
                // shrub land
                // return new Color(1.0f, 0.9f, 0.5f);
                return "Soil";

            }
        }
        // warmest
        else
        {
            if (moisture > .6)
            {
                //rain forest
                return "Rich Soil";
                // return Colors.DarkGreen;
            }
            else if (moisture > .3)
            {
                // temperate forest
                // return new Color(0.5f, 1.0f, 0.5f);
                return "Rich Soil";
            }
            else if (moisture > 0.1)
            {
                // grass land
                // return Colors.LightGreen;
                return "Grass";
            }
            else if (moisture > -.20)
            {
                // savanna
                // return new Color(0.9f, 0.9f, 0.2f);
                return "Dry Grass";
            }
            else
            {
                // dessert
                return "Sand";
                // return new Color(0.7f, 0.7f, 0.3f);
            }
        }
    }

}