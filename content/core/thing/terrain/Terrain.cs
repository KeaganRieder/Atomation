using Godot;
using System;
/*
	defines what a floor is using thinsg defined in
    CompThing and thing
*/
public partial class Terrain : CompThing
{
    //todo make default constructor
    public Terrain(string name, string description)
        : base(name, description) {
        stats = new StatBase[0];
    }
    public Terrain(TerrainDef def) 
        : base(def){
        //todo
    }
}