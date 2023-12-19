using Godot;
using System;
/*
	is the base definition of all 'stats' in atomation
    extends Thing, and is menat to be biult on top 
    and expanded inorder to create more complex 
    'Stats'
*/
public partial class Stat : Thing
{
    //todo
    protected float baseValue;
    private float minValue;
    private float maxValue;
    private float currentValue;
    private float currentMax;
    private bool updateBase;
    private bool updateMax;

}