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

        public virtual void RunStep(Vector2 origin, ChunkHandler chunkHandler) { }

        /// <summary>
        /// sample cords using given offset to ensure noise maps are generating
        /// based on provided chunk position (the offset)
        /// </summary>
        protected virtual void SampleCords(Vector2 offset, int x, int y, out float sampleX, out float sampleY)
        {
            sampleX = x - worldMaxWidth / 2 + offset.X;
            sampleY = y - worldMaxHeight / 2 + offset.Y;
        }
        /// <summary>
        /// offset provided cords are based in correct chunk
        /// </summary>
        protected virtual void SampleChunkPos(Vector2 offset, int x, int y, out float sampleX, out float sampleY)
        {
            // check if x offset cords are negative if so perform 
            // correct operation
            if (offset.X < 0)
            {
                sampleX = x - offset.X;
                sampleX *= -1;
            }
            else
            {
                sampleX = x + offset.X;
            }

            // check if y offset cords are negative if so perform 
            // correct operation
            if (offset.Y < 0)
            {
                sampleY = y - offset.Y;
                sampleY *= -1;
            }
            else
            {
                sampleY = y + offset.Y;
            }
        }

    }
}