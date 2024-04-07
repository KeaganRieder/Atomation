using Godot;
using Atomation.Things;
using Atomation.Resources;

namespace Atomation.Map
{
    /// <summary>
    /// base abstract class which defines things  shared for 
    /// all gen maps. gen maps are used during generation of 
    /// the map
    /// </summary>
    public abstract class GenMaps
    {
        /// <summary>
        /// Minimum value for the given gen map
        /// </summary>
        public float minValue{get; set;} = 100000;
        /// <summary>
        /// Maximum value for the given gen map
        /// </summary>
        public float maxValue{get; set;} =  -100000;

        /// <summary>
        /// Total size of the games map
        /// </summary>
        public Vector2I TotalMapSize{get;set;}
        /// <summary>
        /// size of the current GenMap
        /// </summary>
        public Vector2I MapSize{get;set;}

        /// <summary>
        /// maps offset from center
        /// </summary>
        public Vector2 Offset { get; set;}

        /// <summary>
        /// validates values for the gen map ensuring it's 
        /// within proper ranges/valid
        /// </summary>
        public virtual void ValidateValues(){}

        /// <summary>
        /// updates the configs for the given generation map based on ones
        /// provided
        /// </summary>
        public virtual void UpdateConfigs(MapGenSettings mapGenSettings){}

    }

}
