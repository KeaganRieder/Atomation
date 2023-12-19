using Godot;
using System;
/*
	is the base definition of all 'things' in Aomation
    extends Resource, and is menat to be biult on top 
    and expanded inorder to create more complex 
    'things'
*/
public partial class Thing : Resource
{
	protected string thingName;
	protected string thingDescription;

   	[Export]
	public string Name{
		get => thingName;
		set => thingName = value;
	}

	[Export]
	public string Description{
		get => thingDescription;
		set => thingDescription = value;
	}
}
