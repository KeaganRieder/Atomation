using Atomation.PlayerChar;

using Godot;
using System;

//delete this
public partial class Test : Node2D
{
    private Inventory inventory;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inventory = new Inventory();
		AddChild(inventory);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
