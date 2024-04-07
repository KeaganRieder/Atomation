using Atomation.Things;
using Godot;
namespace Atomation.Map
{
    /// <summary>
    /// configs for the Moisture Map
    /// </summary>
    public class MoistureMapConfigs
    {
        /// <summary>
        /// the temperature at which the air has to be cooled down to 
        /// in order to be saturated with water vapor
        /// </summary>
        public float DewPoint;
        /// <summary>
        /// the insistency of precipitation for the map
        /// </summary>
        public float PrecipitationIntensity;
        /// <summary>
        /// maybe
        /// </summary>
        public float FlatteningVal;
    }

    public class MoistureMap : GenMaps
    {
        private float scale;
        private float dewPoint;
        private float moistureIntensity;
        private float flatteningThreshold;
        private float equatorHeight;

        public MoistureMap(MapGenSettings mapGenSettings, int width, int height)
        {
            MapSize = new Vector2I(width, height);
            UpdateConfigs(mapGenSettings);

        }

        public override void ValidateValues()
        {
            // if (scale < 0)
            // {
            //     scale = 0.001f;
            // }
            // if (noiseGenerator.Seed == -1)
            // {
            //     RandomNumberGenerator numberGenerator = new RandomNumberGenerator();
            //     noiseGenerator.Seed = numberGenerator.RandiRange(0, 100000);
            // }
            // if (noiseGenerator.FractalOctaves < 0)
            // {
            //     noiseGenerator.FractalOctaves = 1;
            // }

            // Mathf.Clamp(noiseGenerator.Frequency, 0, 2);
            // Mathf.Clamp(noiseGenerator.FractalGain, 0, 6);
            // Mathf.Clamp(noiseGenerator.FractalGain, 0, 6);
            // Mathf.Clamp(noiseGenerator.FractalLacunarity, 0, 6);
        }

        public override void UpdateConfigs(MapGenSettings mapGenSettings)
        {
            TotalMapSize = mapGenSettings.worldSize;
            scale = mapGenSettings.scale;
            equatorHeight = 0;

            dewPoint = mapGenSettings.moistureMapConfigs.DewPoint;
            moistureIntensity = mapGenSettings.moistureMapConfigs.PrecipitationIntensity;
            flatteningThreshold = mapGenSettings.moistureMapConfigs.FlatteningVal;
            ValidateValues();
        }
        public void CalculateMoisture(float x, float y, Terrain terrain)
        {
            float celsius = terrain.HeatValue * 100;

            float saturatedVapor = 6.11f * 10 * (7.5f * celsius / 237 + celsius);
            float actualVapor = 6.11f * 10 * (7.5f * dewPoint / 237 + dewPoint);

            float humidity = (saturatedVapor == 0) ? (actualVapor):(actualVapor / saturatedVapor);

            if (humidity > 50)
            {
                humidity = 50;
            } 

            humidity = (terrain.HeightValue >= flatteningThreshold) ? (humidity * 2):100 - (humidity * 2);

            float precipitation =CalculatePrecipitation(humidity,terrain.HeatValue,y);
            precipitation/=100;

            if (precipitation < minValue)
            {
                minValue = precipitation;
            }
            if (precipitation > maxValue)
            {
                maxValue = precipitation;
            }
            terrain.MoistureValue = precipitation;
       
        }

        private float CalculatePrecipitation(float humidity, float temperature, float y)
        {
            float latitude = Mathf.Abs((y + Offset.Y - equatorHeight)/scale)* 0.5f + 0.5f;
            float estimatePrecipitation = -1 * Mathf.Cos(latitude * 3 * (Mathf.Pi * 2)) * 0.5f + 0.5f;

            float simulated = 2.0f * temperature * humidity;

            return moistureIntensity * (simulated + estimatePrecipitation);
        }

    }
}
