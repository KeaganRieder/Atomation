namespace Atomation.Things;

using Atomation.Resources;
using Atomation.Map;
using Godot;

/// <summary>
/// base of all complex things inside the game. these are things which 
/// generally appear in the world or have more complex functionality
/// </summary>
public abstract partial class CompThing : Node2D, ICompThing
{
	protected Coordinate coordinate;
	public string Description { get; set; }
	public Coordinate Coordinate { get => coordinate; protected set { coordinate = value; } }
	public StatSheet StatSheet { get; protected set; }

	public Node2D Node { get => this; }

	public StaticGraphic Graphic { get; set; }

	public virtual void Damage(float amount){}
	public virtual void Heal(float amount){}



}