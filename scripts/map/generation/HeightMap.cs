using Atomation.Thing;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Map
{
    /// <summary>
    /// configs for the Height Map
    /// </summary>
    public class HeightMapConfigs
    {
        /// <summary>
        /// how many layers are in the noise map
        /// </summary>
        public int octaves;
        /// <summary>
        /// frequency of the noise, 
        /// Low frequency results in smooth noise while high frequency results in rougher, more granular noise
        /// </summary>
        public float frequency;
        /// <summary>
        /// Frequency multiplier between subsequent octaves. 
        /// Increasing this value results in higher octaves producing noise with finer details and a rougher appearance.
        /// </summary>
        public float lacunarity;
        /// <summary>
        /// gain of the noise per layer
        /// </summary>
        public float gain;
        /// <summary>
        /// the noise type which is used
        /// </summary>
        public FastNoiseLite.NoiseTypeEnum noiseType;
        /// <summary>
        /// decides the fractal type/method which noise gets layered
        /// </summary>
        public FastNoiseLite.FractalTypeEnum fractalType;
    }

    /// <summary>
    /// generates the height/elevation using simplex noise
    /// </summary>
    public class HeightMap : GenMaps
    {
        private float scale;
        private FastNoiseLite noiseGenerator;

        public HeightMap(MapGenSettings mapGenSettings, int width, int height)
        {
            MapSize = new Vector2I(width, height);
            TotalMapSize = mapGenSettings.worldSize;
            scale = mapGenSettings.scale;
            noiseGenerator = new FastNoiseLite();
            UpdateConfigs(mapGenSettings);

        }

        public override void ValidateValues()
        {
            if (scale < 0)
            {
                scale = 0.001f;
            }
            if (noiseGenerator.Seed == -1)
            {
                RandomNumberGenerator numberGenerator = new RandomNumberGenerator();
                noiseGenerator.Seed = numberGenerator.RandiRange(0, 100000);
            }
            if (noiseGenerator.FractalOctaves < 0)
            {
                noiseGenerator.FractalOctaves = 1;
            }

            Mathf.Clamp(noiseGenerator.Frequency, 0, 2);
            Mathf.Clamp(noiseGenerator.FractalGain, 0, 6);
            Mathf.Clamp(noiseGenerator.FractalGain, 0, 6);
            Mathf.Clamp(noiseGenerator.FractalLacunarity, 0, 6);
        }

        public override void UpdateConfigs(MapGenSettings mapGenSettings)
        {
            TotalMapSize = mapGenSettings.worldSize;
            scale = mapGenSettings.scale;
            noiseGenerator.NoiseType = mapGenSettings.heightMapConfigs.noiseType;
            noiseGenerator.Seed = mapGenSettings.seed;
            noiseGenerator.Frequency = mapGenSettings.heightMapConfigs.frequency;
            noiseGenerator.FractalType = mapGenSettings.heightMapConfigs.fractalType;
            noiseGenerator.FractalGain = mapGenSettings.heightMapConfigs.gain;
            noiseGenerator.FractalOctaves = mapGenSettings.heightMapConfigs.octaves;

            ValidateValues();
        }

        /// <summary>
        /// samples given cords from smaller intervals
        /// </summary>
        private void SampleCords(float x, float y, out float sampleX, out float sampleY)
        {
            sampleX = (x + Offset.X) / scale;
            sampleY = (y + Offset.Y) / scale;
        }
        
        public void CalculateHeight(float x, float y, Terrain terrain){
            SampleCords(x, y, out float sampleX, out float sampleY);
            float height = noiseGenerator.GetNoise2D(sampleX, sampleY);

            terrain.HeightValue = height;
            // terrain.MoistureValue = height;// - Mathf.Cos(height)*height;       
        }

        
    }
}