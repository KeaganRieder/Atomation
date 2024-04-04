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
        private float chunkSize;

        private bool update;

        public Coordinate(Vector2 worldPosition)
        {
            cellSize = MapSettings.CELL_SIZE;
            chunkSize = Chunk.CHUNK_SIZE * cellSize;

            WorldPosition = worldPosition * cellSize;
            GetXY();
            GetCurrentChunk();

        }

        public Coordinate(int x, int y, Vector2 chunkCords)
        {
            cellSize = MapSettings.CELL_SIZE;

            chunkSize = Chunk.CHUNK_SIZE * cellSize;

            XYPosition = new Vector2I(x, y);
            GetWorldPosition(chunkCords);
            GetCurrentChunk();
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
        /// objects current chunk position 
        /// </summary>
        public Vector2 ChunkCords { get; private set; }

        public void UpdateCords(){
            update= true;
        }

        /// <summary>
        /// gets the current chunk the objects in
        /// </summary>
        private void GetCurrentChunk()
        {
            int x = Mathf.FloorToInt(WorldPosition.X / chunkSize);
            int y = Mathf.FloorToInt(WorldPosition.Y / chunkSize);

            ChunkCords = new Vector2(x, y);
            update = false;
        }

        // /// <summary>
        // /// returns the position of the object world position
        // /// </summary>
        private void GetWorldPosition(Vector2 chunkCords)
        {
            float x = XYPosition.X + chunkCords.X;
            float y = XYPosition.Y + chunkCords.Y;

            WorldPosition = new Vector2(x, y);
            update = false;
        }

        // /// <summary>
        // /// returns position of the object aligned on the pixel grid
        // /// </summary>
        private void GetXY()
        {
            int x = Mathf.FloorToInt(WorldPosition.X / cellSize);
            int y = Mathf.FloorToInt(WorldPosition.Y / cellSize);

            XYPosition = new Vector2I(x, y);
            update = false;
        }

        public override string ToString()
        {
            string cords = $" {XYPosition}, {WorldPosition}";
            return cords;
        }
    }
}

