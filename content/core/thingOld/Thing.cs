using Godot;
using Newtonsoft.Json;
using System;

/*
	is the base definition of all 'things' in Aomation
    extends Resource, and is menat to be biult on top 
    and expanded inorder to create more complex 
    'things'
*/
public partial class Thing : Resource, IThing
{
	[JsonProperty("name")]
	public string Name{get; set;}
	[JsonProperty("label")]
	public string Label{get; set;}
	[JsonProperty("description")]
	public string Description{get; set;}

	public Thing(string name, string description){
		Name = name;
		Description = description;
	}
	// public Thing(ThingDef def){
	// 	Name = def.DefName;
	// 	Description = def.Description;
	// }
}

public interface IThing{
	public string Label{get; set;}
}