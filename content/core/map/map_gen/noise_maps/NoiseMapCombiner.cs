using System.Collections.Generic;
using Godot;


public enum CombinerType{
    Multiply,
    //more todo
}

/// <summary>
/// generates a noise map from a combintion of noiseMaps
/// how it combien is based on the combiner enum
/// </summary>
public class NoiseCombiner : NoiseObject
{
    public List<NoiseObject> sources;
    private CombinerType combinerType;

    public NoiseCombiner(){
        sources = new List<NoiseObject>();
    }
    public NoiseCombiner(CombinerType combinerType){
        this.combinerType = combinerType;
        sources = new List<NoiseObject>();
    }

    public void AddSource(NoiseObject source){
        // NoiseObject.sum
        sources.Add(source);
    }
    public void RemoveSource(NoiseObject source){
        sources.Remove(source);
    }
    public void ClearSoruce(){
         sources.Clear();
    }

    public override float this[int x, int y]{
        get{
            return CombineSources( x,  y);
        }
    }
    public override float this[Vector2 cords]{
        get{
            int x = Mathf.RoundToInt(cords.X);
            int y = Mathf.RoundToInt(cords.Y);
            return this[x, y];
        }
    } 

    public float CombineSources(int x, int y){
        float currentNoise = sources[0][x,y];
        for (int i = 1; i < sources.Count; i++)
        {
            currentNoise *= sources[i][x,y];
        }
        return currentNoise;
    }
}