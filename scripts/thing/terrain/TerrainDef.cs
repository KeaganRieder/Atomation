using Atomation.Resources;

namespace Atomation.Thing
{
    /// <summary>
    /// config file used for all terrain in the game
    /// </summary>
    public class TerrainDef : CompThingDef
    {
        //maybe something here more specif for terrain later?
        //for now it's just used as a way of orgizing def files
        public TerrainDef(string name, string description, StatDef[] statDefs, GraphicConfig graphicData){
            Name = name;
            Description = description;
            StatDefs = statDefs;
            GraphicData = graphicData;
        }
    }
}
	// Terrain[] natural = new Terrain[]{
			//     new Terrain("Grass","", new Dictionary<string, StatBase>{
			//         {"Fertility", new Stat("Fertility", "Objects fertility",1.0f,0,0)},
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.8f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/grass", new Color())
			//     ),