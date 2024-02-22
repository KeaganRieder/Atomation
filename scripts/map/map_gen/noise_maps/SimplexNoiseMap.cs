using Godot;

namespace Atomation.Map
{
    /// <summary>
    /// noise map generates a noise map of various types 
    /// based on the a type chosen when it's constructed
    /// </summary>
    public class SimplexNoiseMap : NoiseObject
    {
        // The number of noise layers that are sampled to get the final value for fractal noise types.
        private int octaves;
        // Frequency of the noise which warps the space. 
        // Low frequency results in smooth noise while high frequency results in rougher, more granular noise.
        private float frequency;
        //A low value places more emphasis on the lower frequency base layers, 
        //while a high value puts more emphasis on the higher frequency layers
        private float persistence;
        // Frequency multiplier between subsequent octaves.
        // Increasing this value results in higher octaves producing noise with finer details and a rougher appearance.
        private float lacunarity;
        private FastNoiseLite noiseLite;


        /// <summary>
        /// creates a NoiseMap object which is set to generate simplex noise
        /// </summary>
        public SimplexNoiseMap(NoiseMapConfig config)
        {            
            noiseLite = new FastNoiseLite()
            {
                NoiseType = config.noiseType,
                FractalType = config.fractalType,
            };
            Seed = config.seed;
            ZoomLevel = config.zoom;
            Octaves = config.octaves;
            Frequency = config.frequency;
            Persistence = config.persistence;
            Lacunarity = config.lacunarity;   
        }

        public void UpdateConfigs(NoiseMapConfig config)
        {
            Seed = config.seed;
            ZoomLevel = config.zoom;
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
            get => mapOffset;
            set
            {
                mapOffset = value;
                if (noiseLite != null)
                {
                    noiseLite.Offset = new Vector3(mapOffset.X, mapOffset.Y, 0);
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

        //todo figure out zooming 
        public override float this[float x, float y]
        {
            get
            {///zoomLevel
                return noiseLite.GetNoise2D(x, y);//maybe change to be world size 
            }
        }
        public override float this[Vector2 cords]
        {
            get
            {
                return noiseLite.GetNoise2Dv(cords/zoomLevel);
            }
        }
    }
}