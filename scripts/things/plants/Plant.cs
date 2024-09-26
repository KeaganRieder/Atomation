namespace Atomation.Things;

using System.Collections.Generic;
using Atomation.GameMap;
using Atomation.Resources;
using Atomation.Systems;
using Godot;
using System;
using Atomation.StatSystem;

/// <summary>
/// plants are things which can be grown over time, hand harvested for resource or
/// just for looks. they serve a variety of purposes
/// </summary>
public partial class Plant : Thing
{
    private int growthTime;
    private float growth;
    private bool harvestable;

    private Dictionary<string, int> resources;

    private float temperatureReq;
    private float moistureReq;
    private float fertilityEffect;

    public Plant(Vector2 position)
    {
        Name = position.ToString();
        graphic = new Graphic();
        AddChild(graphic);
        Position = position * Map.CELL_SIZE;

        // growth = 1;
        // harvestable = false;
        // GameClock.Instance.NewHour += AttemptToGrow;
    }

    public float Growth { get => growth; }
    public bool Harvestable { get => harvestable; }
    public float TemperatureReq { get => temperatureReq; }
    public float MoistureReq { get => moistureReq; }
    public float FertilityEffect { get => fertilityEffect; }

    public override void Configure(string defName)
    {
        base.Configure(ThingDefDatabase.Instance.GetPlantDef(defName), defName);
        graphic.CurrentColor = graphic.CurrentColor;
    }

    public override Dictionary<string, object> FormatThingDef()
    {
        Dictionary<string, object> thingDef = base.FormatThingDef();
        thingDef.Add("Harvestable", harvestable);
        thingDef.Add("Resources", resources);
        thingDef.Add("GrowthTime", growthTime);
        thingDef.Add("FertilityEffect", fertilityEffect);
        thingDef.Add("MoistureReq", moistureReq);
        thingDef.Add("TemperatureReq", temperatureReq);

        return thingDef;
    }

    public override void ConfigureFromDef(Dictionary<string, object> def)
    {
        base.ConfigureFromDef(def);
        if (gridLayer == -1)
        {
            gridLayer = GameLayers.plants;
        }
        resources = def.ContainsKey("Resources") ? def["Resources"].ConvertJsonObject<Dictionary<string, int>>() : null;
        harvestable = def.ContainsKey("Harvestable") ? (bool)def["Harvestable"] : false;
        growthTime = def.ContainsKey("GrowthTime") ? Convert.ToInt32(def["GrowthTime"]) : 0;
        temperatureReq = def.ContainsKey("FertilityEffect") ? Convert.ToSingle(def["FertilityEffect"]) : 0;
        moistureReq = def.ContainsKey("MoistureReq") ? Convert.ToSingle(def["MoistureReq"]) : 0;
        fertilityEffect = def.ContainsKey("TemperatureReq") ? Convert.ToSingle(def["TemperatureReq"]) : 0;
    }

    public override void Save()
    {
        GD.Print("saving of things not implemented");
    }

    public override void Load()
    {
        GD.Print("loading of things not implemented");

    }

    /// <summary>
    /// attempts to progress the growth of the plant
    /// </summary>
    public void AttemptToGrow()
    {
        //todo make condition to allow growth
        growth += 1;
        if (growth >= growthTime)
        {
            harvestable = true;
            growth = growthTime;
        }
        UpdateGraphic();
    }

    /// <summary>
    /// updates graphic based on growth percent
    /// </summary>
    private void UpdateGraphic()
    {
        if (growth < Mathf.RoundToInt(growthTime * .25f))
        {
            graphic.CurrentColor = Colors.Red;
        }
        else if (growth < Mathf.RoundToInt(growthTime * .5f))
        {
            graphic.CurrentColor = Colors.Purple;

        }
        else if (growth < growthTime)
        {
            graphic.CurrentColor = Colors.Orange;
        }
    }

    /// <summary>
    /// applies the specified damage to the structure. if the damage received 
    /// is fatal, destroy structure and given back an amount of resources
    /// </summary>
    public void Damage(float amount)
    {
        statSheet.GetStat("health").Damage = statSheet.GetStat("health").Damage + amount;
        GD.Print($"Health remaining: {statSheet.GetStat("health").CurrentValue}");

        if (statSheet.GetStat("health").CurrentValue <= 0)
        {
            Vector2 position = Position.GlobalToMap();
            chunk.RemoveGridObject<Plant>(position, gridLayer);

            foreach (var item in resources)
            {
                //todo make have to find next valid space
                Item droppedItem = new Item(position * Map.CELL_SIZE);
                droppedItem.Configure(item.Key);
                droppedItem.CurrentStackSize = item.Value;
                chunk.SetGridObject(position, droppedItem);
            }

            DestroyNode();
            return;
        }
    }
    /// <summary>
    /// applies the specified damage to the structure.this damage is gotten from 
    /// the provide stat in the statSheet
    /// </summary>
    public void Damage(StatSheet statSheet)
    {
        StatBase dmg = statSheet.GetStat("attack");
        if (dmg != null)
        {
            Damage(dmg.CurrentValue);
        }
    }

    /// <summary>
    /// applies the specified healing amount to the structure. 
    /// </summary>
    public void Heal(float amount)
    {
        statSheet.GetStat("health").Damage -= amount;
    }
}