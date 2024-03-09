using Atomation.Thing;
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
        private float dewPoint;
        private float moistureIntensity;
        private float flatteningVal;
        private float equatorHeight;

        public MoistureMap(MapGenSettings mapGenSettings, int width, int height)
        {
            MapSize = new Vector2I(width, height);

            // setting equator to be center of the map at  y = 0
            equatorHeight = 0;
            UpdateConfigs(mapGenSettings);
        }

        public override void UpdateConfigs(MapGenSettings mapGenSettings)
        {
            TotalMapSize = mapGenSettings.worldSize;
            dewPoint = mapGenSettings.moistureMapConfigs.DewPoint;
            moistureIntensity = mapGenSettings.moistureMapConfigs.PrecipitationIntensity;
            flatteningVal = mapGenSettings.moistureMapConfigs.FlatteningVal;

            ValidateValues();
        }

        /// <summary>
        /// calculates a tiles humidly/general moisture
        /// </summary>
        public void CalculateMoisture(int y, Terrain terrain)
        {
            //calculate temperature measured in celsius
            
            terrain.MoistureValue = 1;
        }

    }
}

/*
// float temperature = terrain.HeatValue * 100;

            // float saturatedVapor = 6.11f * 10 * (7.5f * temperature / (237.3f + temperature));
            // float actualVapor = 6.11f * 10 * (7.5f * dewPoint / (237.3f + dewPoint));

            // //make sure no division by 0 will not occur if temperature == 0
            // float humidity = actualVapor /saturatedVapor;// ((temperature == 0) ? 1 : );// * 10;

            // //decide wether to invert moisture or not ensuring spawning of certain biomes
            // bool invert = (terrain.HeightValue >= flatteningVal) ? true : false; 

            // if (humidity<minValue )
            // {
            //     // GD.Print()
            //     minValue = humidity;
            // }
            //  if (humidity>maxValue )
            // {
            //     maxValue = humidity;
            // }
            // float precipitation = (humidity-minValue )/(maxValue-minValue);//CalculatePrecipitation( y, terrain.HeatValue,  humidity);

           
*/