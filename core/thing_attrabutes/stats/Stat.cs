using Godot;
using System;
using System.Collections.Generic;

/*
    extends on attrabutes defined in BaseAttrabutes to define
    what stats are, ie Health
*/
public partial class Stat : BaseAttrabutes
{   
    protected float baseValue;
    private float minValue;
    private float maxValue;
    private float currentValue;
    private float currentMax;
    private bool updateBase;
    private bool updateMax;

    private ActiveMoidfers activeMoidfers; // todo

    [Export]
    public float MinVal{
        get => minValue;
        set => minValue = value;
    }
    [Export]
    public float MaxVal{
        get => CalculateMax();
        set => currentMax = value;
    }
    [Export]
    public float Value{
        get => CalculateValue();
        set => currentValue = value;
    }

    public Stat(){
        baseValue = 0;
        minValue = 0;
        maxValue = 0;
        currentValue = baseValue;
        currentMax = maxValue;

        updateBase = false;
        updateMax = false;

        activeMoidfers = new ActiveMoidfers();

    }
    public Stat(string TodoMakeDefFile){
        //todo when def file import is set up
    }


    public void AddModifer(){
        updateBase = true;
    }

    public void RemoveModifer(){
        updateBase = true;
    }

    public float CalculateMax(){
        if(updateMax){
            //todo
            updateMax = false;
        }
        return currentMax;
    }
     public float CalculateValue(){
        if(updateBase){
            //todo
            updateBase = false;
        }
        return currentValue;
    }

}

public struct ActiveMoidfers{
    //todo figure out how to properly handle stats
    //rather then just having this temp setup
    private StatModifer baseModifers;
    private StatModifer maxModifers;
    public ActiveMoidfers(){
        baseModifers = new();
        maxModifers = new();
    }

}
