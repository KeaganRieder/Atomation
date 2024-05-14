namespace Atomation.Controls;

using Godot;

public static class ControlUtility
{
    /// <summary>
    /// gets mouse position relative to provided node
    /// </summary>
    public static Vector2 GetMousePosition(this Node2D node, InputEventMouseButton input) 
    {
        //needs fixing
        Vector2 mousePos = input.Position - node.GetViewport().CanvasTransform.Origin;
        return mousePos;
    }

    public static Vector2 GetMousePosition(this Node2D node) 
    {
        Vector2 mousePos = node.GetViewport().GetMousePosition() - node.GetViewport().CanvasTransform.Origin;
        return mousePos;
    }
}