namespace Atomation.Things;

using System.Collections.Generic;
using Atomation.GameMap;
using Atomation.Resources;
using Atomation.Systems;
using Godot;
using System;

/// <summary>
/// plants are things which can be grown over time, hand harvested for resource or
/// just for looks. they serve a variety of purposes
/// </summary>
public partial class Plant : Thing
{
    private float growthTime;
    private float growth;
    private bool harvestable;

    private Dictionary<string, int> resources; // todo

    private int temperatureReq;
    private int moistureReq;
    private float fertilityEffect;

    public Plant(Vector2 position)
    {
        graphic = new Graphic();
        AddChild(graphic);
        graphic.Position = position * Map.CELL_SIZE;

        growth = 1;
        harvestable = false;
        GameClock.Instance.NewHour += AttemptToGrow;
    }

    public float Growth { get => growth; }
    public bool Harvestable { get => harvestable; }

    public int TemperatureReq { get => temperatureReq; }
    public int MoistureReq { get => moistureReq; }
    public float FertilityEffect { get => fertilityEffect; }

    public void Configure(PlantDef def, bool loading = false)
    {
        //todo
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
}