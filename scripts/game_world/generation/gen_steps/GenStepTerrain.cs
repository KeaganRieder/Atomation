namespace Atomation.GameMap;

using Atomation.Resources;
using Atomation.Things;
using Godot;

/// <summary>
/// defines the step in generation which handles teh creation of the games
/// terrain
/// </summary>
public class GenStepLandScape : GenStep
{
    private float[,] elevation;
    private float[,] temperature;
    private float[,] moisture;

    private float waterLevel;
    private float mountainSize;

    public GenStepLandScape(GeneratedNoiseMaps noiseMaps, WorldSettings configs)
    {
        Step = 1;
        elevation = noiseMaps.ElevationMap;
        temperature = noiseMaps.TemperatureMap;
        moisture = noiseMaps.MoistureMap;

        waterLevel = configs.WaterLevel;
        mountainSize = configs.MountainSize;
    }

    public override bool Validate()
    {
        if (elevation == default)
        {
            GD.PushError("No elevation map given");
            return false;
        }
        if (temperature == default)
        {
            GD.PushError("No temperature map given");

            return false;
        }
        if (moisture == default)
        {
            GD.PushError("No moisture map given");

            return false;
        }
        return true;
    }
    /// <summary>
    /// runs the genStep and generates the landscape,
    /// consisting of mountains, lakes, rivers, other water bodies and landmass
    /// </summary>
    public override void RunStep(GeneratedMapData generatedMapData)
    {

        if (!Validate())
        {
            generatedMapData.GeneratedStructures = default;
            generatedMapData.GeneratedTerrain = default;
        }

        SetSize(generatedMapData.GenSize);

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                Terrain generatedTile;

                Structure generatedMountainWall = default;

                if (elevation[x, y] > mountainSize - .08)
                {
                    if (elevation[x, y] >= mountainSize)
                    {
                        generatedMountainWall = GetMountainWall(x, y);
                    }
                    generatedTile = GetMountainFloorTile(x, y);
                }
                else if (elevation[x, y] > waterLevel)
                {
                    generatedTile = GetTerrainTile(x, y);
                }
                else
                {
                    generatedTile = GetWaterTile(x, y);
                }
                generatedTile.Elevation = elevation[x, y];
                generatedTile.Temperature = temperature[x, y];
                generatedTile.Moisture = moisture[x, y];

                generatedMapData.GeneratedStructures[x, y] = generatedMountainWall;
                generatedMapData.GeneratedTerrain[x, y] = generatedTile;
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
    private Terrain GetTerrainTile(int x, int y)
    {
        Terrain terrainFloorTile = new Terrain(new Vector2(x, y));

        terrainFloorTile.Configure(SelectBiome(x, y));

        return terrainFloorTile;
    }

    /// <summary>
    /// decides what water is at the given x, y cords based
    /// on the elevation
    /// </summary>
    private Terrain GetWaterTile(int x, int y)
    {
        Terrain waterTile = new Terrain(new Vector2(x, y));
        if (elevation[x, y] > waterLevel - .1)
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
    private string SelectBiome(int x, int y)
    {
        // temp:-.8 to .6 moisture:-.8 to .8
        // cold
        if (temperature[x, y] < -.15f)
        {
            if (moisture[x, y] > 0.0)
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
        else if (temperature[x, y] < .25f)
        {
            if (moisture[x, y] > .4)
            {
                // Seasonal Forest
                // return new Color(0.2f, 0.7f, 0.2f);
                return "Forest Grass";
            }
            else if (moisture[x, y] > 0)
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
            if (moisture[x, y] > .6)
            {
                //rain forest
                return "Rich Soil";
                // return Colors.DarkGreen;
            }
            else if (moisture[x, y] > .3)
            {
                // temperate forest
                // return new Color(0.5f, 1.0f, 0.5f);
                return "Rich Soil";
            }
            else if (moisture[x, y] > 0.1)
            {
                // grass land
                // return Colors.LightGreen;
                return "Grass";
            }
            else if (moisture[x, y] > -.20)
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