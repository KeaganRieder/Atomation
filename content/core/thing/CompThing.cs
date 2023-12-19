using Godot;
using System;
/*
	is the base definition of all 'complex things' in Aomation
    extends Thing, adding properties shared by all compelex things
    will be used asand expanded upon inorder to define specific 
    complex things like floors and charcters
*/
public partial class CompThing : Thing
{
    protected Stat[] stats;

}