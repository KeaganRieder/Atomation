namespace Atomation.Systems;

using System;
using Godot;

/// <summary>
/// CustomCamera is used to extend and add new functionality to godot's 2d camera
/// </summary>
public partial class CustomCamera : Camera2D
{
    private Vector2 targetPosition;

    private Vector2 defaultZoom;
    private float zoomTarget;
    private float minZoom;
    private float maxZoom;
    private float zoomSpeed;

    private bool processZoomEvent = false;
    private bool processMoveEvent = false;

    public CustomCamera(Node parent = null)
    {
        Name = "Main Cam";
        Position = Vector2.Zero;

        Zoom = defaultZoom = Vector2.One;
        zoomTarget = 0.5f;
        minZoom = 0.45f;
        maxZoom = 1.25f;
        zoomSpeed = 0.05f;

        UpdateTarget(parent);
    }

    /// <summary>
    /// speed in which the camera zooms in and out
    /// </summary>
    public float ZoomSpeed { get => zoomSpeed; set => zoomSpeed = value; }
    public float MinZoom { get => minZoom; set => minZoom = value; }
    public float MaxZoom { get => maxZoom; set => maxZoom = value; }

    public Vector2 TargetPosition { get => targetPosition; set => targetPosition = value; }


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (processZoomEvent)
        {
            Zoom = new Vector2(zoomTarget, zoomTarget);
        }
    }

    /// <summary>
    /// updates the cameras viewport size
    /// </summary>
    public void SetViewportSize()
    {
        GD.Print("setting of viewport size hasn't been implemented");
    }

    /// <summary>
    /// updates the target of the camera
    /// </summary>
    public void UpdateTarget(Node newParent)
    {
        if (newParent != null)
        {
            Node currentParent = GetParent();

            if (GetParent() != null)
            {
                currentParent.RemoveChild(this);
            }

            newParent.AddChild(this);
        }
    }


    /// <summary>
    /// zooms in camera
    /// </summary>
    public void ZoomIn()
    {
        zoomTarget = Mathf.Max(zoomTarget - zoomSpeed, minZoom);
        processZoomEvent = true;
    }

    /// <summary>
    /// zooms out camera
    /// </summary>
    public void ZoomOut()
    {
        zoomTarget = Mathf.Max(zoomTarget - zoomSpeed, maxZoom);
        processZoomEvent = true;
    }
}