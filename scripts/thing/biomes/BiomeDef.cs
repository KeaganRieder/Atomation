
namespace Atomation.Thing
{
    /// <summary>
    /// config file used for all BiomeDef in the game
    /// </summary>
    public class BiomeDef : ThingDef
    {
        public float heightValue { get; set; }
        public float heatValue { get; set; }
        public float moistureValue { get; set; }

        //todo:
        //make a collection of tiles, plants and other spectic biome things
    }
}