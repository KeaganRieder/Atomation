using System.Collections.Generic;
/*
    BiomeDef is the file reprsentation of a biome,
    object it conatins values which don't change with
    a biome' object during run time
*/
public class BiomeDef : ThingDef{

    // holds the key(name of gen req ) value (requiremnet value)
    // pair for tiles in the terrain
    // generation values should be between 0 and 1
    public Dictionary<string, float> genReqs {get; set;}
    // holds the key(string name of tile) value (hieght of tile)
    // pair for tiles in the terrain
    public Dictionary<string, float> terrainByHeight {get; set;}

    //todo add stuff for hills/mountains?,m nj
}