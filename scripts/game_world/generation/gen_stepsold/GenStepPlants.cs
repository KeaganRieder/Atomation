// namespace Atomation.GameMap;

// using Atomation.Things;
// using Godot;

// /// <summary>
// /// this generation step handles teh generation of plants, resources and 
// /// other things (tbd)
// /// </summary>
// public class GenStepPlants : GenStep
// {
//     private WorldSettings configs;
//     public GenStepPlants()
//     {
//     }

//     public override void RunStep(GenStepData genStepData)
//     {
//         configs = genStepData.GenStepConfigs;


//         SetSize(genStepData.GenSize);

//         for (int x = 0; x < genSize.X; x++)
//         {
//             for (int y = 0; y < genSize.Y; y++)
//             {
//                 float elevation = genStepData.GeneratedNoiseMaps.ElevationMap[x, y];
//                 float temperature = genStepData.GeneratedNoiseMaps.TemperatureMap[x, y];
//                 float moisture = genStepData.GeneratedNoiseMaps.MoistureMap[x, y];

//                 Plant plant = null;
//                 if (elevation > configs.WaterLevel && elevation < configs.MountainSize - .1)
//                 {
//                     if (genStepData.GeneratedNoiseMaps.TreeDensityMap[x, y] > 1 - configs.TreeDensity)
//                     {
//                         plant = new Plant(new Vector2(x, y));
//                         plant.Configure("Pine");
//                     }
//                 }

//                 genStepData.GeneratedFoliage[x, y] = plant;
//             }
//         }
//     }

//     private void TryToSpawnTree()
//     {
//         //todo
//     }

// }