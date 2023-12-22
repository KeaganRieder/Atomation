using System.Collections.Generic;
/*
	TerrainDef is the file reprsentation of a terrain,
    object it conatins values which don't change with
    a terrain object during run time
*/
public class TerrainDef : ThingDef
{
    public string texturePath{get; set;}
    public Dictionary<string, float> statBases;
    public GraphicData graphicData;

    //todo add cost
    //todo make it so differnt terrain(floors) can only support ceratin things

    
}
