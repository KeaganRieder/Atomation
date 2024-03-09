// using Godot;

// namespace Atomation.Map
// {
//     /// <summary>
//     /// base abstract class for noise maps
//     /// </summary>
//     public abstract class NoiseObject
//     {
//         protected int seed = 0;
//         protected Vector2 offset = new Vector2(0, 0);
//         protected float zoomLevel = 1;
//         protected float minNoise = 1000;
//         protected float maxNoise = -1000;

//         public virtual int Seed { get => seed; set { seed = value; } }
//         public virtual float ZoomLevel { get => zoomLevel; set { zoomLevel = Mathf.Clamp(value, 0.1f, 10); } }
//         public virtual Vector2 Offset { get => offset; set { offset = value; } }

//         public virtual float GetNoise(float x, float y){
//             return 0.0f;
//         }

//         public virtual float this[float x, float y]
//         {
//             get
//             {
//                 return 0.0f;
//             }
//         }
//         public virtual float this[Vector2 cords]
//         {
//             get
//             {
//                 int x = Mathf.RoundToInt(cords.X);
//                 int y = Mathf.RoundToInt(cords.Y);
//                 return this[x, y];
//             }
//         }

//         protected virtual void UpdateMinMax(float value){
//             if (value < minNoise)
//             {
//                 minNoise = value;
//             }
//             if (value > maxNoise)
//             {
//                 maxNoise = value;
//             }
//         }
//     }
// }