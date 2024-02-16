using Godot;

namespace Atomation.Map
{
    /// <summary>
    /// config data used for setting how noise maps are generated
    /// </summary>
    public class NoiseMapConfig
    {
        public int seed;
        public int octaves;
        public float zoom;
        public float frequency;
        public float persistence;
        public float lacunarity;

        /// <summary>
        /// the distance a point is from the center position
        /// </summary>
        public Vector2 offset;
        /// <summary>
        /// the max distance a vertices/point can be form the center point
        /// NOTE: this is mainly used in the generation of the heat map
        /// </summary>
        public Vector2 maxDistance;
        /// <summary>
        /// the center points cords
        /// NOTE: this is mainly used in the generation of the heat map
        /// </summary>
        public Vector2 centerPoint;

    }

    /// <summary>
    /// class which is used to pass a collection of data that relates to the 
    /// configuration of world world generation.
    /// this data is passed between the many world generation classes
    /// </summary>
    public class GenConfigs
    {
        //general configs
        /// <summary>
        /// bounds/max size of the world
        /// x = width
        /// y = height
        /// </summary>
        public Vector2I worldBounds;

        //generation configs    
        public NoiseMapConfig elevationMapConfigs;
        public NoiseMapConfig moistureMapConfigs;
        public NoiseMapConfig heatMapConfigs;

        //terrain configs
        public float seaLevel = .2f;
        public float mountainSize = .8f; 
    }
}