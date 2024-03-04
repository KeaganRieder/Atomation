using Godot;

namespace Atomation.Map
{
    /// <summary>
    /// noise map generates a noise map of various types 
    /// based on the a type chosen when it's constructed
    /// </summary>
    public class SimplexNoiseMap : NoiseObject
    {
        private int octaves;
        private float frequency;
        private float persistence;
        private float lacunarity;
        private FastNoiseLite noiseLite;


        /// <summary>
        /// creates a NoiseMap object which is set to generate simplex noise
        /// </summary>
        public SimplexNoiseMap(NoiseMapSettings config, int seed)
        {            
            noiseLite = new FastNoiseLite()
            {
                NoiseType = config.noiseType,
                FractalType = config.fractalType,
                DomainWarpType = FastNoiseLite.DomainWarpTypeEnum.SimplexReduced,
                DomainWarpEnabled = false,
                DomainWarpAmplitude = 8,
                DomainWarpFractalGain = 0.5f,
                DomainWarpFrequency = 0.005f,
                DomainWarpFractalLacunarity = 4,
                DomainWarpFractalOctaves = 3,
            };
            Seed = seed;
            Octaves = config.octaves;
            Frequency = config.frequency;
            Persistence = config.persistence;
            Lacunarity = config.lacunarity;   
        }

        public void UpdateConfigs(NoiseMapSettings config, int seed)
        {
            Seed = seed;
            Octaves = config.octaves;
            Frequency = config.frequency;
            Persistence = config.persistence;
            Lacunarity = config.lacunarity;
        }

        public override int Seed
        {
            get => seed;
            set
            {
                seed = value;
                if (noiseLite != null)
                {
                    noiseLite.Seed = value;
                }
            }
        }
        public override Vector2 Offset
        {
            get => offset;
            set
            {
                offset = value;
                if (noiseLite != null)
                {
                    noiseLite.Offset = new Vector3(offset.X, offset.Y, 0);
                }
            }
        }
        public int Octaves
        {
            get => octaves;
            set
            {
                octaves = Mathf.Max(1, value);
                if (noiseLite != null)
                {
                    noiseLite.FractalOctaves = octaves;
                }
            }
        }
        public float Frequency
        {
            get => frequency;
            set
            {
                frequency = value;
                if (noiseLite != null)
                {
                    noiseLite.Frequency = frequency;
                }
            }
        }
        public float Persistence
        {
            get => persistence;
            set
            {
                persistence = value;
                if (noiseLite != null)
                {
                    noiseLite.FractalGain = persistence;
                }
            }
        }
        public float Lacunarity
        {
            get => lacunarity;
            set
            {
                lacunarity = value;
                if (noiseLite != null)
                {
                    noiseLite.FractalLacunarity = lacunarity;
                }
            }
        }

        public override float GetNoise(float x, float y){
            
            float noiseVal = noiseLite.GetNoise2D(x, y);

            return noiseVal;
        }


        //todo figure out zooming 
        public override float this[float x, float y]
        {
            get
            {
                return GetNoise(x, y);
            }
        }
    }
}