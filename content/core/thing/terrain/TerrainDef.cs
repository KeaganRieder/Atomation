using System;
using System.Collections.Generic;

public class TerrainDef
{
    public string defName = "";
    public string description = "";
    public string texturePath = "";
    public string supports = "";
    public Dictionary<string, float> statBases;

    //todo graphic stuff
    // colors
    //egde type

    //todo add cost
}
/*
<statBases>
      <Beauty>-1</Beauty>
      <Cleanliness>-0.1</Cleanliness>
      <WorkToBuild>120</WorkToBuild>
      <FilthMultiplier>0.05</FilthMultiplier>
      <Flammability>1.5</Flammability>
    </statBases>
*/