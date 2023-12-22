using Godot;
using System;

// [JsonConstructor] can be used to let tell the json to use this
//constructor on deseralization

/*
	is the base definition of all 'things' in Aomation
    extends Resource, and is menat to be biult on top 
    and expanded inorder to create more complex 
    'things'
*/
public partial class Thing : Resource
{
	public string Name{get;}
	public string Description{get;}

	public Thing(string name, string description){
		Name = name;
		Description = description;
	}
	public Thing(ThingDef def){
		Name = def.DefName;
		Description = def.Description;
	}
}



