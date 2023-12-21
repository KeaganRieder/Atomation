using Godot;
using System;
/*
	is the base definition of all 'complex things' in Aomation
    extends Thing, adding properties shared by all compelex things
    will be used asand expanded upon inorder to define specific 
    complex things like floors and charcters
*/
public partial class CompThing : Thing
{
    protected StatBase[] stats;

    public CompThing(string name, string description)
        : base(name, description) {
        stats = new StatBase[0];
    }
    
     public CompThing(ThingDef def)
        : base(def) {
        stats = new StatBase[0];
    }

}