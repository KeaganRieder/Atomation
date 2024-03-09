using Godot;
using Atomation.Thing;
using Atomation.Resources;
using Atomation.Utility;

namespace Atomation.Map
{
    public class GenStepLandScape : GenStep
    {
        private HeightMap heightMap;
        private TemperatureMap temperatureMap;
        private MoistureMap moistureMap;

        private float deepWater;
        private float shallowWater;
        private float shore;
        private float mountain;
        private float rockyGround;

        public GenStepLandScape(MapGenSettings genConfig)
        {
            heightMap = new HeightMap(genConfig, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
            temperatureMap = new TemperatureMap(genConfig, Chunk.CHUNK_SIZE,Chunk.CHUNK_SIZE);
            moistureMap = new MoistureMap(genConfig, Chunk.CHUNK_SIZE,Chunk.CHUNK_SIZE);

            deepWater = genConfig.seaLevel;
            shallowWater = genConfig.seaLevel + 0.1f;
            shore = genConfig.seaLevel + 0.2f;

            mountain = genConfig.mountainSize;
            rockyGround = genConfig.mountainSize - 0.1f;

        }

        public void UpdateConfigs(MapGenSettings genConfig)
        {
            heightMap.UpdateConfigs(genConfig);
            temperatureMap.UpdateConfigs(genConfig);
            moistureMap.UpdateConfigs(genConfig);
        }

        public override void RunStep(Vector2 GlobalCord, ChunkHandler chunkHandler)
        {
            heightMap.Offset = GlobalCord;
            temperatureMap.Offset = GlobalCord;
            moistureMap.Offset = GlobalCord;

            for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
                {
                    //convert to be based on chunk pos
                    CordConversion.SampleChunkPos(GlobalCord, x, y, out float sampleChunkX, out float sampleChunkY);

                    //pull terrain
                    Terrain terrain = chunkHandler.GetTerrain(Mathf.RoundToInt(sampleChunkX), Mathf.RoundToInt(sampleChunkY));

                    if (terrain == null)
                    {
                        //create new terrain
                        terrain = new(new(x, y));
                        chunkHandler.Set(Mathf.RoundToInt(sampleChunkX), Mathf.RoundToInt(sampleChunkY), terrain);
                    }

                    heightMap.CalculateHeight(x, y, terrain);
                    temperatureMap.CalculateHeat(y,terrain);
                    moistureMap.CalculateMoisture(y, terrain);

                    GenerateLand(terrain);

                    terrain.UpdateGraphic(VisualizationMode.Default);
                }
            }
            GD.Print($"MIN{temperatureMap.minValue} MAX: {temperatureMap.maxValue}");

        }

        private void GenerateLand(Terrain terrain)
        {
            CreateLandScape(terrain);
        }
        private void CreateLandScape(Terrain terrain)
        {

            if (IsWater(terrain))
            {
                return;
            }
            if (IsMountain(terrain))
            {
                return;
            }

            IsLand(terrain);
        }
        private bool IsWater(Terrain terrain)
        {
            if (terrain.HeightValue < deepWater)
            {
                terrain.FloorGraphic.Color = new Color(0, 0, 0);
                // terrain.MoistureValue = 1;

                return true;
            }
            else if (terrain.HeightValue < shallowWater)
            {
                terrain.FloorGraphic.Color = new Color(0.2f, 0.2f, 0.2f);
                // terrain.MoistureValue = 1;

                return true;
            }

            return false;
        }
        private void IsLand(Terrain terrain)
        {
            if (terrain.HeightValue < shore)
            {
                terrain.FloorGraphic.Color = new Color(0.4f, 0.4f, 0.4f);
            }
            else
            {
                terrain.FloorGraphic.Color = new Color(terrain.HeightValue, terrain.HeightValue, terrain.HeightValue);
            }


        }
        private bool IsMountain(Terrain terrain)
        {
            if (terrain.HeightValue > rockyGround && terrain.HeightValue < mountain)
            {
                terrain.FloorGraphic.Color = new Color(.8f, .8f, .8f);
                return true;
            }
            else if (terrain.HeightValue > mountain)
            {
                terrain.FloorGraphic.Color = new Color(1, 1, 1);


                return true;
            }
            return false;
        }


    }
}