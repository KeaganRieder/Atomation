
using Godot;

namespace Atomation.Map
{
    /// <summary>
    /// class generates a noise map value map used for map generation
    /// </summary>
    public class GenValueMap : NoiseObject
    {
        private float min = 1000;
        private float max = -1000;

        private int worldWidth;
        private int worldHeight;
        private int width;
        private int height;
        private float[,] valueMap;

        private SimplexNoiseMap noiseMap;

        public GenValueMap(SimplexNoiseMap simplexNoiseMap, Vector2 origin,float width, float height, int worldWidth, int worldHeight ){
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight;
            this.width = Mathf.RoundToInt(width); 
            this.height = Mathf.RoundToInt(height);
            valueMap = new float[this.width, this.height];
            noiseMap = simplexNoiseMap;
            Offset =origin;
            GenerateMap();
        }

        /// <summary>
        /// gets cord values from smaller intervals
        /// </summary>
        protected  void SampleCords(int x, int y, out float sampleX, out float sampleY)
        {
            sampleX = x;// + mapOffset.X) /worldWidth;
            sampleY = y;//+mapOffset.Y)/worldHeight;
        }

        public void GenerateMap(){
            for (int x = 0; x < width; x++)
            {
               for (int y = 0; y < height; y++)
               {
                    SampleCords(x, y,  out float sampleX, out float sampleY);

                    float value = noiseMap[sampleX,sampleY];

                    if (value > max)
                    {
                        max = value;
                    }
                    if (value < min)
                    {
                        min = value;
                    }
                    valueMap[x,y] = value;
               }
            }
            GD.Print($"MIN {min},MAX {max}");
        }

        public override float this[float x, float y]
        {
            get
            {
                float value = valueMap[Mathf.RoundToInt(x),Mathf.RoundToInt(y)];
                // value = (value - min) / (max-min);
                // GD.Print(value);
                return value;
            }
        }

    }
}
