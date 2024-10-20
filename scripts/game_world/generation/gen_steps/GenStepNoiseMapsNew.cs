namespace Atomation.GameMap;

using Godot;

public class GenStepNoiseMapsNew : GenStepNew
{
    public override void RunStep(WorldConfigs configs, GenStepDataNew genStepData)
    {
        SetOffset(genStepData.GenOffset);
        SetSize(genStepData.GenSize);
        configs.ConfigureNoiseMaps();

        genStepData.NoiseMaps["Elevation"] = new float[genSize.X, genSize.Y];
        genStepData.NoiseMaps["Temperature"] = new float[genSize.X, genSize.Y];
        genStepData.NoiseMaps["Moisture"] = new float[genSize.X, genSize.Y];

        GradientMapGenerator GradientMapGenerator = new GradientMapGenerator(offset, genSize,
        configs.WorldSize, (bool)configs.Values["Temperature_TrueCenter"]);

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                float sampleX = (x + offset.X)/ configs.ZoomLevel;
                float sampleY = (y + offset.Y)/ configs.ZoomLevel;

                float elevation = configs.SimplexNoiseMapConfigs["Elevation"].GetNoise(sampleX, sampleY);

                float temperature = CalculateTemperature(elevation, GradientMapGenerator.GetGradientNoiseValue(y), configs);

                float moisture = CalculateMoisture(sampleX, sampleY, elevation, configs);

                genStepData.NoiseMaps["Elevation"][x, y] = elevation;
                genStepData.NoiseMaps["Temperature"][x, y] = temperature;
                genStepData.NoiseMaps["Moisture"][x, y] = moisture;
            }
        }
    }

    /// <summary>
    /// calculates the current tile (at x,y) moisture value
    /// </summary>
    private float CalculateMoisture(float x, float y, float elevation, WorldConfigs configs)
    {
        float moisture = configs.SimplexNoiseMapConfigs["Moisture"].GetNormalizedNoise(x, y);
        float baseMoisture = (float)configs.Values["Temperature_Base"];

        float waterLevel = configs.GetWaterLevel();

        if (elevation <= waterLevel)
        {
            return 1;
        }
        else
        {
            return Mathf.Clamp(moisture + baseMoisture, -1, 1);
        }
    }

    /// <summary>
    /// calculates the current tile (at x,y) temperature value
    /// </summary>
    private float CalculateTemperature(float elevation, float equatorTemperature, WorldConfigs configs)
    {
        float temperature;

        float baseTemp = (float)configs.Values["Temperature_Base"];
        elevation = elevation < 0 ? elevation * elevation * -1 : elevation * elevation;

        temperature = Mathf.Clamp((equatorTemperature * -1) - elevation + baseTemp, -1, 1);

        return temperature;
    }



}