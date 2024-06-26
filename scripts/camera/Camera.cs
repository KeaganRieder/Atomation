using System;
using Godot;
namespace Atomation
{
    /// <summary>
    /// a camera which can be zoom in/out along with many 
    /// other things
    /// </summary>
    public partial class Camera : Camera2D
    {
        //redo this at some point
        private float minZoom = 0.5f;
        private float maxZoom = 2f;
        private float zoomSpeed = 0.1f;

        private Vector2 defaultZoom = new Vector2(1f, 1f);

        public Camera(Node2D focus)
        {
            Position = Vector2.Zero;
            Zoom = defaultZoom;
            focus.AddChild(this);
        }

        public void ZoomIn()
        {
            Vector2 zoomValue = Vector2.Zero;
            
            zoomValue.X = Math.Clamp(Zoom.X * (1 + zoomSpeed), minZoom, maxZoom);
            zoomValue.Y = Math.Clamp(Zoom.Y * (1 + zoomSpeed), minZoom, maxZoom);

            Zoom = zoomValue;
        }

        public void ZoomOut()
        {
            Vector2 zoomValue = Vector2.Zero;
            zoomValue.X = Math.Clamp(Zoom.X * (1 - zoomSpeed), minZoom, maxZoom);
            zoomValue.Y = Math.Clamp(Zoom.Y * (1 - zoomSpeed), minZoom, maxZoom);
            Zoom = zoomValue;
        }
    }
}