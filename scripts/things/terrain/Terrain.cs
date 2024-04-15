namespace Atomation.Things;

using Godot;
using Atomation.Map;
using Atomation.Resources;
using System;


/// <summary>
/// the terrain/floor of the games world. this object is the base visual in
/// a game world with other objects being placed on top of it
/// </summary>
public partial class Terrain : CompThing
{
	public float HeightValue { get; set; }
	public float HeatValue { get; set; }
	public float MoistureValue { get; set; }

	public BasicGraphic Graphic { get; set; }

	public Terrain(Coordinate coord)
	{
		coordinate = coord;
		Position = coordinate.WorldPosition;

		Graphic = new BasicGraphic(this);
	}

	/// <summary>
	/// reading the configuration data for the given tile
	/// and setting it for anything in which is needing
	/// configuration at current call
	/// </summary>
	public void ReadConfigs(TerrainDef config)
	{
		Name = config.Name + Coordinate.ToString();
		Description = config.Description;
		stats = config.Stats();
		modifiers = config.StatModifers();
		Graphic.ConfigureGraphic(config.GraphicData);
	}


	public void UpdateGraphic(VisualizationMode displayMode)
	{
		if (displayMode == VisualizationMode.Default)
		{
			Graphic.DefaultGraphic();
		}
		else if (displayMode == VisualizationMode.Height)
		{
			Graphic.HeightGraphic(HeightValue);
		}
		else if (displayMode == VisualizationMode.Heat)
		{
			Graphic.HeatGraphic(HeatValue);
		}
		else
		{
			Graphic.MoistureGraphic(MoistureValue);
		}
	}
}

