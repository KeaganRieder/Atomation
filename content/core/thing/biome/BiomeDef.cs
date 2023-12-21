using System.Collections.Generic;
/*
    defines a biomes definition file
*/
public class BiomeDef : ThingDef{
    // public string defName = "";
    // public string description = "";
    // holds the key(name of gen req ) value (requiremnet value)
    // pair for tiles in the terrain
    //generation values should be between 0 and 1
    public Dictionary<string, float> genReqs = null;
    // holds the key(string name of tile) value (hieght of tile)
    // pair for tiles in the terrain
    public Dictionary<string, float> tileTypes = null;

    //todo add stuff for hills/mountains?,m nj
}