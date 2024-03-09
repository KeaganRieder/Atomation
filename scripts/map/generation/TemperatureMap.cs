using Atomation.Thing;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Map
{
    /// <summary>
    /// configs for the Temperature Map
    /// </summary>
    public class TemperatureMapConfigs
    {
        /// <summary>
        /// highest temperature at the equator
        /// </summary>
        public float BaseTemperature;
        /// <summary>
        /// multiplier decreasing the temperature value with increasing latitude
        /// </summary>
        public float TemperatureMultiplier;
        /// <summary>
        /// is the amount of temperature that should be lost for each height step
        /// </summary>
        public float TemperatureLoss;
        /// <summary>
        /// incremental steps at which temperature should be lost
        /// </summary>
        public float TemperatureHeight;
    }

    /// <summary>
    /// generates the height/elevation using simplex noise
    /// </summary>
    public class TemperatureMap : GenMaps
    {
        private float equatorHeight;
        private float baseTemperature;
        private float temperatureMultiplier;
        private float temperatureLoss;
        private float temperatureHeight;

        public TemperatureMap(MapGenSettings mapGenSettings, int width, int height)
        {
            MapSize = new Vector2I(width, height);
            TotalMapSize = mapGenSettings.worldSize;
            //0 is dead center due to map generating both in negative and positive directions
            equatorHeight = 0;
            UpdateConfigs(mapGenSettings);
        }
        public override void ValidateValues()
        {
            // Todo    
        }

        public override void UpdateConfigs(MapGenSettings mapGenSettings)
        {
            TotalMapSize = mapGenSettings.worldSize;
            equatorHeight = 0;
            baseTemperature = mapGenSettings.temperatureMapConfigs.BaseTemperature;
            temperatureMultiplier = mapGenSettings.temperatureMapConfigs.TemperatureMultiplier;
            temperatureLoss = mapGenSettings.temperatureMapConfigs.TemperatureLoss;
            temperatureHeight = mapGenSettings.temperatureMapConfigs.TemperatureHeight;

            ValidateValues();
        }

        /// <summary>
        /// calculates a given points temperature 
        /// </summary>
        public void CalculateHeat(int y, Terrain terrain)
        {            
            // find the distance given point is from equator
            float latitude = Mathf.Abs(y+Offset.Y - equatorHeight);
            
            // equator heat is the distance given point is from equator
            //and how close it is to bounds
            float equatorHeat = latitude / TotalMapSize.Y;

            float avgTemperature = (equatorHeat * - temperatureMultiplier)
             -(terrain.HeightValue / temperatureHeight * temperatureLoss) + baseTemperature;
            
            if (avgTemperature<minValue )
            {
                minValue = avgTemperature;
            }
             if (avgTemperature>maxValue )
            {
                maxValue = avgTemperature;
            }

            terrain.HeatValue = avgTemperature;
        }
      
    }

}