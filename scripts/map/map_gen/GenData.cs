using System;

/// <summary>
/// class which is used to pass a collection of data that relates to the 
/// configuration of world world generation.
/// this data is passed between the many world generation classes
/// </summary>
public class GenData
{
    //general configs
    private int maxWidth;
    private int maxHeight;
      
    //generation configs    
    private int seed;
    private float zoomLevel;

    //noiseMaps
    // private NoiseCombiner heatMap;
    // private NoiseMap moistureMap;
    private NoiseMap elevationMap;

    //terrain configs
    private float waterLevel = .2f;
    private float mounatinSize = .8f; //no mountains is anything above 1

    // constructor
    public GenData(){

        elevationMap = new NoiseMap();
    }

    //getters and setters
    public int MaxMapWidth{get =>maxWidth; set{maxWidth = value;}}
    public int MaxMapHeight{get=>maxHeight;set{maxHeight = value;}}
    public int Seed{
        get => seed;
        set{
            seed = value;
            elevationMap.Seed = value;
        }
    }
    public float ZoomLevel{
        get => zoomLevel;
        set{
            zoomLevel = value;
            elevationMap.ZoomLevel = value;
        }
    }
    public int HeightOctaves{
        get => elevationMap.Octaves;
        set{
            elevationMap.Octaves = value;
        }
    }
    public float HeightFrequency{
        get => elevationMap.Frequency;
        set{
            elevationMap.Frequency = value;
        }
    }
    public float HeightPersistence{
        get => elevationMap.Persistence;
        set{
            elevationMap.Persistence = value;
        }
    }
    public float HeightLacunarity{
        get => elevationMap.Lacunarity;
        set{
            elevationMap.Lacunarity = value;
        }
    }

    //getting noise maps
    public NoiseMap ElevationMap{get=> elevationMap; set{elevationMap = value;}}

    public float WaterLevel{
        get=> waterLevel; 
        set{waterLevel = Math.Clamp(value,.2f,.5f);}
    }
    public float MounatinSize{
        get=> mounatinSize; 
        set{mounatinSize = Math.Clamp(value,.7f,1f);}
    }
    
}