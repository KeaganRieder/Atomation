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

        public virtual void RunStep(Vector2 origin, ChunkHandlerOld chunkHandler) { }

        /// <summary>
        /// gets cord values from smaller intervals
        /// </summary>
        protected virtual void SampleCords(int x, int y, Vector2 offset,float scale, out float sampleX, out float sampleY)
        {
            sampleX = (x + offset.X)/scale;
            sampleY = (y + offset.Y)/scale;
        }

    }
}