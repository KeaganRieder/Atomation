namespace Atomation.Things;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Map;
using Atomation.Resources;

/// <summary>
/// defines basic structures
/// </summary>
public partial class Structure : Thing
{
	public Structure(Coordinate coord)
	{
		coordinate = coord;
		Position = coordinate.WorldPosition;

		Graphic = new StaticGraphic();
		AddChild(Graphic);
	}
	public Structure(SavedStructure savedStructure)
	{
		Graphic = new StaticGraphic();
		AddChild(Graphic);
		
		Coordinate = savedStructure.Cords;
		Position = coordinate.WorldPosition;
		StatSheet = savedStructure.StatSheet;
		if (savedStructure.Name != null)
		{
			ReadConfigs(DefDatabase.GetInstance().GetStructureDef(savedStructure.Name), true);
		}
	}

	/// <summary>
	/// reading the configuration data for the given tile
	/// and setting it for anything in which is needing
	/// configuration at current call
	/// </summary>
	public void ReadConfigs(StructureDef config, bool loading = false)
	{
		DefName = config.Name;
		Name = DefName + Coordinate.ToString();
		Description = config.Description;
		if (!loading)
		{
			StatSheet = new StatSheet(config.StatSheet, this);
		}
		Graphic.Configure(config.GraphicData);
	}

	public override void Damage(float amount)
	{
		StatSheet.GetStat(StatKeys.MAX_HEALTH).Damage(amount);
	}
	public override void Heal(float amount)
	{
		StatSheet.GetStat(StatKeys.MAX_HEALTH).Heal(amount);
	}



}

