namespace Atomation.Things;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Map;
using Atomation.Resources;


/// <summary>
/// defines basic structures
/// </summary>
public partial class Structure : CompThing
{
	// public BasicGraphic FloorGraphic { get; set; }

	public Structure(Coordinate coord)
	{
		coordinate = coord;
		Position = coordinate.WorldPosition;

		Graphic = new StaticGraphic();
		AddChild(Graphic);
	}

	/// <summary>
	/// reading the configuration data for the given tile
	/// and setting it for anything in which is needing
	/// configuration at current call
	/// </summary>
	public void ReadConfigs(StructureDef config)
	{
		Name = config.Name + Coordinate.ToString();
		Description = config.Description;
		// StatSheet = config.StatSheet;
		// Graphic.Configure(config.GraphicData);
	}



}

