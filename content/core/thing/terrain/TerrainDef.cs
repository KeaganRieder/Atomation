using System;
using System.Collections.Generic;

public class TerrainDef : ThingDef
{
    public string texturePath = "";
    public string supports = "";
    public Dictionary<string, float> statBases;
    public GraphicData graphicData;

    //todo add cost
}
