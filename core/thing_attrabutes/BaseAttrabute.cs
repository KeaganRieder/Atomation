using Godot;
using System;

/*
	defiens the base attrabutes shared by any thing
	in the game
	this includes thing slike name and it's 
	description
*/
public partial class BaseAttrabutes : Resource
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

	public BaseAttrabutes(){
		thingName = null;
		thingDescription = null;
	}
}
