namespace Atomation.Things;

using Godot;
using Atomation.Map;
using Atomation.Resources;

/// <summary>
/// the terrain/floor of the games world. this object is the base visual in
/// a game world with other objects being placed on top of it
/// </summary>
public partial class Terrain : Thing
{
	public float Elevation { get; set; }
	public float Temperature { get; set; }
	public float Moisture { get; set; }

	public Terrain(Coordinate coord)
	{
		coordinate = coord;
		Position = coordinate.WorldPosition;
		Graphic = new StaticGraphic();
		AddChild(Graphic);
	}

	public Terrain(SavedTerrain savedTerrain)
	{
		Graphic = new StaticGraphic();
		AddChild(Graphic);

		Elevation = savedTerrain.Elevation;
		Temperature = savedTerrain.Temperature;
		Moisture = savedTerrain.Moisture;

		Coordinate = savedTerrain.Cords;
		Position = coordinate.WorldPosition;
		StatSheet = savedTerrain.StatSheet;

		//move this into the def data base and make it return a default object
		if (savedTerrain.Name != null)
		{
			ReadConfigs(DefDatabase.GetInstance().GetTerrainDef(savedTerrain.Name), true);
		}
		else
		{
            Graphic.DefaultColor = Colors.Red;
		}
	}

	/// <summary>
	/// reading the configuration data for the given tile
	/// and setting it for anything in which is needing
	/// configuration at current call
	/// </summary>
	public void ReadConfigs(TerrainDef config, bool loading = false)
	{
		DefName = config.Name;
		Name = DefName + Coordinate.ToString();
		Description = config.Description;
		if (!loading)
		{
			StatSheet = new StatSheet(config.StatSheet, this);
		}

		Graphic.Configure(config.GraphicData);
		UpdateGraphic(VisualizationMode.Default);
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

		if (Temperature < -1.3)
		{
			heatColor = Colors.White;
		}
		else if (Temperature < -1.0)
		{
			heatColor = Colors.Pink;
		}
		else if (Temperature < -0.8)
		{
			heatColor = Colors.Purple;
		}
		else if (Temperature < -0.5)
		{
			heatColor = Colors.DarkBlue;
		}
		else if (Temperature < -0.25)
		{
			heatColor = Colors.Cyan;
		}
		else if (Temperature < 0.25)
		{
			heatColor = Colors.Green;
		}
		else if (Temperature < 0.7)
		{
			heatColor = Colors.DarkGreen;
		}
		else if (Temperature < 1)
		{
			heatColor = Colors.Yellow;
		}
		else if (Temperature < 1.25)
		{
			heatColor = Colors.Orange;
		}
		else if (Temperature < 1.7)
		{
			heatColor = Colors.Red;
		}
		else
		{
			heatColor = Colors.DarkRed;
		}

		Graphic.Modulate = heatColor;
	}

	/// <summary>
	/// makes terrain display as height map
	/// where -1 is black (lowest ground), and 1 is while (hightest ground)
	/// </summary>
	public void HeightGraphic()
	{
		Graphic.Modulate = new Color(Elevation, Elevation, Elevation);
	}

	/// <summary>
	/// makes terrain display as height map
	/// where -1 is black (lowest ground), and 1 is while (hightest ground)
	/// </summary>
	public void MoistureGraphic()
	{
		//this needs work
		Color moistureColor;

		if (Moisture < 0.27)
		{
			moistureColor = Colors.Red;

		}
		else if (Moisture < 0.4)
		{
			moistureColor = Colors.Orange;

		}
		else if (Moisture < 0.5)
		{
			moistureColor = Colors.Yellow;

		}
		else if (Moisture < 0.7)
		{
			moistureColor = Colors.Green;

		}
		else if (Moisture < 0.8)
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