namespace Atomation.GameMap;

using Atomation.Things;
using Godot;

public class GenStepTerrainNew : GenStepNew
{

    public override void RunStep(WorldConfigs configs, GenStepDataNew genStepData)
    {
        SetOffset(genStepData.GenOffset);
        SetSize(genStepData.GenSize);

        genStepData.MapLayers["terrain"] = new Thing[genSize.X, genSize.Y];
        genStepData.MapLayers["mountains"] = new Thing[genSize.X, genSize.Y];

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                Structure mountain = null;
                Terrain terrain;

                Vector2 cords = new Vector2(x, y);

                float elevation = genStepData.NoiseMaps["Elevation"][x, y];
                float temperature = genStepData.NoiseMaps["Temperature"][x, y];
                float moisture = genStepData.NoiseMaps["Moisture"][x, y];

                // if (CreateElevatedLandScape(cords, elevation, configs.GetMountainSize(), out terrain, out mountain))
                // {
                //     if (mountain != null)
                //     {
                        genStepData.MapLayers["mountains"][x, y] = mountain;
                //     }
                //     genStepData.MapLayers["terrain"][x, y] = terrain;
                // }

                // else
                // {
                    CreateLandscape(cords, elevation, temperature, moisture, configs.GetWaterLevel(), out terrain);
                    genStepData.MapLayers["terrain"][x, y] = terrain;
                // }
            }
        }
    }

    /// <summary>
    ///   /// attempts generates the elevated which are the worlds mountains and rocky terrain
    ///   returns true if it's possible otherwise returns false
    /// </summary>
    private bool CreateElevatedLandScape(Vector2 cords, float elevation, float mountainSize, out Terrain terrain, out Structure mountain)
    {
        cords = cords.MapToGlobal();

        mountain = null;
        terrain = null;

        // mountain wall
        if (elevation > mountainSize)
        {
            mountain = new Structure(cords);
            terrain = new Terrain(cords);

            mountain.Configure("Slate Wall");
            terrain.Configure("Slate");
            return true;
        }
        //mountain floor without wall
        else if (elevation > mountainSize - 0.01f)
        {
            terrain = new Terrain(cords);
            terrain.Configure("Slate");
            return true;
        }

        return false;
    }

    /// <summary>
    /// attempts generates the rest of the landscape, which includes water and land that isn't 
    /// elevated (mountains) returns true if it's possible otherwise returns false
    /// </summary>
    private void CreateLandscape(Vector2 cords, float elevation, float temperature, float moisture, float waterLevel, out Terrain terrain)
    {
        cords = cords.MapToGlobal();
            // GD.Print(elevation);

        // water
        if (elevation < waterLevel)
        {
            terrain = new Terrain(cords);

            if (elevation < waterLevel - 0.15)
            {
                terrain.Configure("Deep Ocean");
            }
            else if (elevation < waterLevel)
            {
                terrain.Configure("Shallow Ocean");
            }
        }

        // land
        else
        {
            // string terrainID = GetTerrainType(temperature, moisture);
            terrain = new Terrain(cords);
            terrain.Configure("undef");
            terrain.Graphic.CurrentColor = new Color(elevation, elevation, elevation);

        }
    }

    private string GetTerrainType(float temperature, float moisture)
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