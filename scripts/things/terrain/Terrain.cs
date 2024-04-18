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


	public Terrain(Coordinate coord)
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
	public void ReadConfigs(TerrainDef config)
	{
		Name = config.Name + Coordinate.ToString();
		Description = config.Description;
		// StatSheet = config.StatSheet;//new StatSheet(, this);
		// Graphic.Configure(config.GraphicData);
	}

	public void UpdateGraphic(VisualizationMode displayMode)
	{
		if (displayMode == VisualizationMode.Default)
		{
			Graphic.SetDefaultColor();
		}
		else if (displayMode == VisualizationMode.Height)
		{
			HeightGraphic();
		}
		else if (displayMode == VisualizationMode.Heat)
		{
			HeatGraphic();
		}
		else
		{
			MoistureGraphic();
		}
	}

	/// <summary>
	/// makes terrain display as heat map
	/// where -.9 is darkRed(hot), and 1 is blue(cold)
	/// </summary>
	public void HeatGraphic()
	{
		Color heatColor;

		if (HeatValue < -1.3)
		{
			heatColor = Colors.White;
		}
		else if (HeatValue < -1.0)
		{
			heatColor = Colors.Pink;
		}
		else if (HeatValue < -0.8)
		{
			heatColor = Colors.Purple;
		}
		else if (HeatValue < -0.5)
		{
			heatColor = Colors.DarkBlue;
		}
		else if (HeatValue < -0.25)
		{
			heatColor = Colors.Cyan;
		}
		else if (HeatValue < 0.25)
		{
			heatColor = Colors.Green;
		}
		else if (HeatValue < 0.7)
		{
			heatColor = Colors.DarkGreen;
		}
		else if (HeatValue < 1)
		{
			heatColor = Colors.Yellow;
		}
		else if (HeatValue < 1.25)
		{
			heatColor = Colors.Orange;
		}
		else if (HeatValue < 1.7)
		{
			heatColor = Colors.Red;
		}
		else
		{
			heatColor = Colors.DarkRed;
		}

		Graphic.Modulate  = heatColor;
	}

	/// <summary>
	/// makes terrain display as height map
	/// where -1 is black (lowest ground), and 1 is while (hightest ground)
	/// </summary>
	public void HeightGraphic()
	{
		Graphic.Modulate = new Color(HeightValue, HeightValue, HeightValue);
	}

	/// <summary>
	/// makes terrain display as height map
	/// where -1 is black (lowest ground), and 1 is while (hightest ground)
	/// </summary>
	public void MoistureGraphic()
	{
		//this needs work
		Color moistureColor;

		if (MoistureValue < 0.27)
		{
			moistureColor = Colors.Red;

		}
		else if (MoistureValue < 0.4)
		{
			moistureColor = Colors.Orange;

		}
		else if (MoistureValue < 0.5)
		{
			moistureColor = Colors.Yellow;

		}
		else if (MoistureValue < 0.7)
		{
			moistureColor = Colors.Green;

		}
		else if (MoistureValue < 0.8)
		{
			moistureColor = Colors.Cyan;
		}
		else
		{
			moistureColor = Colors.Blue;
		}
		Graphic.Modulate = moistureColor;
	}

}