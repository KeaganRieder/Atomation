using Godot;

namespace Atomation.Map
{
    /// <summary>
    /// base class for all GenSteps, defining functions used
    /// by all steps
    /// </summary>
    public abstract class GenStep
    {
        protected int worldMaxWidth;
        protected int worldMaxHeight;

        protected Vector2 offset;

        public virtual void RunStep(Vector2 origin, ChunkHandler chunkHandler) { }

        /// <summary>
        /// gets cord values from smaller intervals
        /// </summary>
        protected virtual void SampleCords(int x, int y, float scale, out float sampleX, out float sampleY)
        {
            sampleX = (x + offset.X) / scale;
            sampleY = (y + offset.Y) / scale;
        }

        /// <summary>
        /// using given cords offsets them based on current offset. used to primarily find current 
        /// chunk being generated
        /// </summary>
        protected Vector2 CurrentChunk(int x, int y)
        {
            float xCord = x + offset.X;
            float yCord = y + offset.Y;

            return new Vector2(xCord, yCord);
        }
        /// <summary>
        /// uses the offset to find the correct world position for the given cords
        /// </summary>
        // protected Vector2 GetWorldPosition(float x, float y)
        // {
        //     // int
        //     x = (offset.X < 0) ? x * -1 + offset.X : x + offset.X;
        //     y = (offset.Y < 0) ? y * -1 + offset.Y : y + offset.Y;

        //     //align back to pixel grid
        //     x = Mathf.FloorToInt(x);
        //     y = Mathf.FloorToInt(y);


        //     if (offset.X < 0 || offset.Y < 0)
        //     {
        //         GD.Print($"Cords for Offset {offset} are {x}, {y}");
        //     }

        //     return new Vector2(x, y);
        // }

    }
}