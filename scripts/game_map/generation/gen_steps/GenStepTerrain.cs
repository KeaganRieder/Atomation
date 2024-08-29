namespace Atomation.GameMap;

using System.Collections.Generic;
using Atomation.Resources;
using Atomation.Things;
using Godot;

// -todo rock types

/// <summary>
/// defines the step in generation which handles teh creation of the games
/// terrain
/// </summary>
public class GenStepLandScape : Generator<object>
{
    private float[,] elevation;
    private float[,] temperature;
    private float[,] moisture;

    private float waterLevel = -0.23f;
    private float mountainSize = 0.45f;


    public GenStepLandScape(float[,] elevationMap = default, float[,] temperatureMap = default, float[,] moistureMap = default)
    {
        configure(elevationMap, temperatureMap, moistureMap);
    }

    public void configure(float[,] elevation = default, float[,] temperature = default, float[,] moisture = default)
    {
        this.elevation = elevation;
        this.temperature = temperature;
        this.moisture = moisture;
    }

    protected bool Validate()
    {
        if (elevation == null || temperature == null || moisture == null)
        {
            GD.PushError("Can't Generate landscape for required layers haven't been set");
            return false;
        }
        return true;
    }

    /// <summary>
    /// runs the genStep and generates the landscape,
    /// consisting of mountains, lakes, rivers, other water bodies and landmass
    /// </summary>
    public void GenerateLandScape(out Terrain[,] generatedTerrain, out Structure[,] generatedMountains, Vector2 offset = default, Vector2I size = default)
    {
        if (!Validate())
        {
            GD.PushError("Required maps haven't been set, as such can't generate");
            generatedTerrain = default;
            generatedMountains = default;
            return;
        }

        SetSize(size);
        SetOffset(offset);

        generatedTerrain = new Terrain[genSize.X, genSize.Y];
        generatedMountains = new Structure[genSize.X, genSize.Y];

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                Terrain generatedTile;

                Structure generatedMountainWall = default;

                if (elevation[x, y] > mountainSize)
                {
                    if (elevation[x, y] > mountainSize + .1)
                    {
                        generatedMountainWall = GetMountainWall(x, y);
                        // GD.Print(generatedMountainWall.Name);
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
                generatedMountains[x, y] = generatedMountainWall;
                generatedTerrain[x, y] = generatedTile;
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
        mountainWall.Configure(ThingDatabase.Instance.GetStructureDef("Slate Wall"));

        return mountainWall;
    }

    /// <summary>
    /// decides the mountain floor tile at the given x y cords 
    /// </summary>
    public Terrain GetMountainFloorTile(int x, int y)
    {
        Terrain mountainFloorTile = new Terrain(new Vector2(x, y));

        mountainFloorTile.Configure(ThingDatabase.Instance.GetTerrainDef("Slate"));

        return mountainFloorTile;
    }

    /// <summary>
    /// decides what terrain is at the given x, y cords based
    /// on moisture and temperature map 
    /// </summary>
    private Terrain GetTerrainTile(int x, int y)
    {
        Terrain terrainFloorTile = new Terrain(new Vector2(x, y));

        terrainFloorTile.Configure(ThingDatabase.Instance.GetTerrainDef(SelectBiome(x, y)));

        return terrainFloorTile;
    }

    /// <summary>
    /// decides what water is at the given x, y cords based
    /// on the elevation
    /// </summary>
    private Terrain GetWaterTile(int x, int y)
    {
        Terrain waterTile = new Terrain(new Vector2(x, y));
        if (elevation[x, y] > waterLevel - 0.12)
        {
            waterTile.Configure(ThingDatabase.Instance.GetTerrainDef("Shallow Ocean"));
            return waterTile;
        }
        else
        {
            waterTile.Configure(ThingDatabase.Instance.GetTerrainDef("Deep Ocean"));
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