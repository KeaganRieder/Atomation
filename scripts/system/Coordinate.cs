using Atomation.Map;
using Godot;

namespace Atomation
{
    /// <summary>
    /// represents an objects Coordinate in the game world, chunk or grids
    /// </summary>
    public class Coordinate
    {
        private float cellSize;

        public Coordinate(Vector2 worldPosition)
        {
            cellSize = MapSettings.CELL_SIZE;

            WorldPosition = worldPosition * cellSize;
            UpdateXY();
            // UpdateCurrentChunk();

        }

        public Coordinate(int x, int y, Vector2 chunkCords)
        {
            cellSize = MapSettings.CELL_SIZE;

            XYPosition = new Vector2I(x, y);
            GetWorldPosition(chunkCords);
        }

        /// <summary>
        /// objects current world position 
        /// </summary>
        public Vector2 WorldPosition { get; private set; }

        /// <summary>
        /// objects current x y position in the world
        /// </summary>
        public Vector2I XYPosition { get; private set; }


        /// <summary>
        /// sets the world position to given value and then updates 
        /// all other cords
        /// </summary>
        public void UpdateWorldPosition(Vector2 cords)
        {
            WorldPosition = cords;
            UpdateXY();
            // UpdateCurrentChunk();
        }

        /// <summary>
        /// finds the position of the object aligned on the pixel grid
        /// </summary>
        private void UpdateXY()
        {
            int x = Mathf.FloorToInt(WorldPosition.X / cellSize);
            int y = Mathf.FloorToInt(WorldPosition.Y / cellSize);

            XYPosition = new Vector2I(x, y);
        }
        
        /// <summary>
        /// finds the position of the object from the XY Cords
        /// </summary>
        private void GetWorldPosition(Vector2 chunkCords)
        {
            
            // float x = (chunkCords.X < 0) ? XYPosition.X * -1 + chunkCords.X : XYPosition.X + chunkCords.X;
            // float y = (chunkCords.Y < 0) ? XYPosition.Y * -1 + chunkCords.Y : XYPosition.Y + chunkCords.Y; 

            float x = XYPosition.X + chunkCords.X;
            float y = XYPosition.Y + chunkCords.Y;

            WorldPosition = new Vector2(x, y) * MapSettings.CELL_SIZE;
            // GD.Print($"Cords {WorldPosition}");
        }

        public override string ToString()
        {
            string cords = $" {XYPosition}, {WorldPosition}, ";
            return cords;
        }
    }
}

