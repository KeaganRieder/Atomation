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

        /// <summary>
        /// offsets the starting cords for the noise map
        /// </summary>
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

        /// <summary>
        /// how many times to layer the noise map
        /// </summary>
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
        /// <summary>
        ///the frequency of noise
        /// </summary>
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
        /// <summary>
        /// Determines the strength of each subsequent layer of noise in fractal noise
        /// </summary>
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
        /// <summary>
        /// Frequency multiplier between subsequent octaves
        /// </summary>
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
                return noiseLite.GetNoise2D(x, y);
            }
        }
    }
}