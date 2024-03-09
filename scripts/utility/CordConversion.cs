using Godot;
namespace Atomation.Utility{
    public static class CordConversion
    {
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
    }
}