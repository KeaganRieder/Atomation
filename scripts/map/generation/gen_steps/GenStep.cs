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

        protected Vector2 chunkPos;

        public virtual void RunStep(Vector2 origin, WorldMap chunkHandler) { }


    }
}