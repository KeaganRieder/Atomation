namespace Atomation.Things;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Map;
using Atomation.Resources;

/// <summary>
/// defines basic structures
/// </summary>
public partial class Structure : BaseThing
{
	public Structure(Coordinate coord)
	{
		coordinate = coord;
		Position = coordinate.GetWorldPosition();

		Graphic = new StaticGraphic();
		AddChild(Graphic);
	}
	public Structure(SavedStructure savedStructure)
	{
		Graphic = new StaticGraphic();
		AddChild(Graphic);

		coordinate = savedStructure.Cords;
		Position = coordinate.GetWorldPosition();
		StatSheet = new StatSheet(savedStructure.StatSheet, this);

		ReadConfigs(DefDatabase.GetInstance().GetStructureDef(savedStructure.Name), true);
	}

	/// <summary>
	/// reading the configuration data for the given tile
	/// and setting it for anything in which is needing
	/// configuration at current call
	/// </summary>
	public void ReadConfigs(StructureDef config, bool loading = false)
	{
		DefName = config.Name;
		Name = DefName + coordinate.ToString();
		Description = config.Description;
		if (!loading)
		{
			StatSheet = new StatSheet(config.StatSheet, this);
		}
		Graphic.Configure(config.GraphicData);
	}

	public void Damage(float amount)
	{
		StatSheet.GetStat(StatKeys.MAX_HEALTH).Damage(amount);

		if (StatSheet.GetStat(StatKeys.MAX_HEALTH).CurrentValue <= 0)
		{
			WorldMap.GetInstance().SetStructure(coordinate, null);
			DestroyNode();
		}
	}
	public void Damage(StatSheet statSheet)
	{
		StatBase dmg = statSheet.GetStat(StatKeys.ATTACK_DAMAGE);

		if (dmg != null)
		{
			GD.Print($"dmg: {dmg.CurrentValue}");

			Damage(dmg.CurrentValue);
		}
	}

	public void Heal(float amount)
	{
		StatSheet.GetStat(StatKeys.MAX_HEALTH).Heal(amount);
	}



}

