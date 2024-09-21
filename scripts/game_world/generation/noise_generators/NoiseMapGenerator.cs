// namespace Atomation.GameMap;

// using Godot;

// public class NoiseMapGenerator : NoiseGenerators
// {
//     private FastNoiseLite generator;
//     private float scale;
//     private float baseValue;
//     private float baseStrength;

//     public NoiseMapGenerator(FastNoiseLite fastNoiseLite, float scale, int seedOffset, float baseValue = 0, float baseStrength = 1.5f)
//     {
//         Configure(fastNoiseLite, scale, seedOffset, baseValue, baseStrength);
//     }

//     public void Configure(FastNoiseLite fastNoiseLite, float scale, int seedOffset, float baseValue = 0, float baseStrength = 1.5f)
//     {
//         generator = fastNoiseLite;
//         fastNoiseLite.Seed = fastNoiseLite.Seed;
//         this.scale = scale;
//         this.baseValue = baseValue;
//         this.baseStrength = baseStrength;
//     }

//     public override float[,] Generate(Vector2 offset = default, Vector2I size = default)
//     {
//         if (generator == null)
//         {
//             GD.PushError("Can't Generate settings aren't set");
//             return default;
//         }
//         SetSize(size);
//         SetOffset(offset);
//         float[,] noiseMap = new float[size.X, size.Y];

//         // configs.NoiseOffset = ;

//         for (int x = 0; x < size.X; x++)
//         {
//             for (int y = 0; y < size.Y; y++)
//             {
//                 float sampleX = (x + offset.X);// scale;
//                 float sampleY = (y + offset.Y);// scale;
//                 float noise = generator.GetNoise2D(sampleX, sampleY);//Mathf.Clamp(                    (),// + 1 + baseValue) * (2 - baseStrength),
//                 //     0, 1
//                 // );

//                 noiseMap[x, y] = noise;

//             }
//         }
//         return noiseMap;
//     }


// }