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
        /// gets cord values from smaller intervals
        /// </summary>
        protected virtual void SampleCords(int x, int y, Vector2 offset,float scale, out float sampleX, out float sampleY)
        {
            sampleX = (x + offset.X)/scale;
            sampleY = (y + offset.Y)/scale;
        }

        protected virtual void AlignCordsToChunk(int x, int y, Vector2 offset,out float sampleX, out float sampleY){

                sampleX = x + offset.X;
                sampleY = y + offset.Y;


            if (offset.X < 0)
            {
                sampleX = (x* -1) + offset.X;
            }
            else
            {
                sampleX = x + offset.X;
                
            }
            if (offset.Y < 0)
            {
                sampleY = (y*-1) + offset.Y;
            }
            else
            {
                sampleY = y + offset.Y;
            }
        }
/*
 /// <summary>
        /// offset provided cords are based in correct chunk
        /// </summary>
        public static void SampleChunkPos(Vector2 offset, int x, int y, out float sampleX, out float sampleY)
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
*/
    }
}