// using Godot;
// using System;

// public class MapGenerator : Node2D
// {
//     public int width = 600;
//     public int height = 200;
//     private TileMap tilemap;
//     private Dictionary<Vector2, float> temperature = new Dictionary<Vector2, float>();
//     private Dictionary<Vector2, float> moisture = new Dictionary<Vector2, float>();
//     private Dictionary<Vector2, float> altitude = new Dictionary<Vector2, float>();
//     private Dictionary<Vector2, string> biome = new Dictionary<Vector2, string>();
//     private OpenSimplexNoise openSimplexNoise = new OpenSimplexNoise();
//     private Dictionary<Vector2, object> objects = new Dictionary<Vector2, object>();

//     private Dictionary<string, int> tiles = new Dictionary<string, int>
//     {
//         { "grass", 0 },
//         { "jungle_grass", 1 },
//         { "sand", 2 },
//         { "water", 3 },
//         { "snow", 4 },
//         { "stone", 5 }
//     };

//     private Dictionary<string, PackedScene> objectTiles = new Dictionary<string, PackedScene>
//     {
//         { "tree", GD.Load<PackedScene>("res://Tree.tscn") },
//         { "cactus", GD.Load<PackedScene>("res://Cactus.tscn") },
//         { "spruce_tree", GD.Load<PackedScene>("res://Spruce_tree.tscn") }
//     };

//     private Dictionary<string, Dictionary<string, float>> biomeData = new Dictionary<string, Dictionary<string, float>>
//     {
//         { "plains", new Dictionary<string, float> { { "grass", 1 } } },
//         { "beach", new Dictionary<string, float> { { "sand", 0.99f }, { "stone", 0.01f } } },
//         { "jungle", new Dictionary<string, float> { { "jungle_grass", 1 } } },
//         { "desert", new Dictionary<string, float> { { "sand", 0.98f }, { "stone", 0.02f } } },
//         { "lake", new Dictionary<string, float> { { "water", 1 } } },
//         { "mountain", new Dictionary<string, float> { { "stone", 0.98f }, { "grass", 0.02f } } },
//         { "snow", new Dictionary<string, float> { { "snow", 0.97f }, { "stone", 0.02f }, { "grass", 0.02f } } },
//         { "ocean", new Dictionary<string, float> { { "water", 1 } } }
//     };

//     private Dictionary<string, Dictionary<string, float>> objectData = new Dictionary<string, Dictionary<string, float>>
//     {
//         { "plains", new Dictionary<string, float> { { "tree", 0.03f } } },
//         { "beach", new Dictionary<string, float> { { "tree", 0.01f } } },
//         { "jungle", new Dictionary<string, float> { { "tree", 0.04f } } },
//         { "desert", new Dictionary<string, float> { { "cactus", 0.03f } } },
//         { "lake", new Dictionary<string, float>() },
//         { "mountain", new Dictionary<string, float> { { "spruce_tree", 0.02f } } },
//         { "snow", new Dictionary<string, float> { { "spruce_tree", 0.02f } } },
//         { "ocean", new Dictionary<string, float>() }
//     };

//     private void GenerateMap(int per, int oct)
//     {
//         openSimplexNoise.Seed = (uint)GD.Randi();
//         openSimplexNoise.Period = per;
//         openSimplexNoise.Octaves = oct;
//         Dictionary<Vector2, float> gridName = new Dictionary<Vector2, float>();
//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 float rand = 2 * Mathf.Abs(openSimplexNoise.GetNoise2d(x, y));
//                 gridName[new Vector2(x, y)] = rand;
//             }
//         }
//     }

//     private void Ready()
//     {
//         temperature = GenerateMap(300, 5);
//         moisture = GenerateMap(300, 5);
//         altitude = GenerateMap(150, 5);
//         SetTile(width, height);
//     }

//     private void SetTile(int width, int height)
//     {
//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 Vector2 pos = new Vector2(x, y);
//                 float alt = altitude[pos];
//                 float temp = temperature[pos];
//                 float moist = moisture[pos];

//                 // Ocean
//                 if (alt < 0.2)
//                 {
//                     biome[pos] = "ocean";
//                     tilemap.SetCellv(pos, tiles[RandomTile(biomeData, "ocean")]);
//                 }
//                 // Beach
//                 else if (Between(alt, 0.2f, 0.25f))
//                 {
//                     biome[pos] = "beach";
//                     tilemap.SetCellv(pos, tiles[RandomTile(biomeData, "beach")]);
//                 }
//                 // Other Biomes
//                 else if (Between(alt, 0.25f, 0.8f))
//                 {
//                     // Plains
//                     if (Between(moist, 0, 0.9f) && Between(temp, 0, 0.6f))
//                     {
//                         biome[pos] = "plains";
//                         tilemap.SetCellv(pos, tiles[RandomTile(biomeData, "plains")]);
//                     }
//                     // Jungle
//                     else if (Between(moist, 0.4f, 0.9f) && temp > 0.6f)
//                     {
//                         biome[pos] = "jungle";
//                         tilemap.SetCellv(pos, tiles[RandomTile(biomeData, "jungle")]);
//                     }
//                     // Desert
//                     else if (temp > 0.6f && moist < 0.4f)
//                     {
//                         biome[pos] = "desert";
//                         tilemap.SetCellv(pos, tiles[RandomTile(biomeData, "desert")]);
//                     }
//                     // Lakes
//                     else if (moist >= 0.9f)
//                     {
//                         biome[pos] = "lake";
//                         tilemap.SetCellv(pos, tiles[RandomTile(biomeData, "lake")]);
//                     }
//                 }
//                 // Mountains
//                 else if (Between(alt, 0.8f, 0.95f))
//                 {
//                     biome[pos] = "mountain";
//                     tilemap.SetCellv(pos, tiles[RandomTile(biomeData, "mountain")]);
//                 }
//                 // Snow
//                 else
//                 {
//                     biome[pos] = "snow";
//                     tilemap.SetCellv(pos, tiles[RandomTile(biomeData, "snow")
