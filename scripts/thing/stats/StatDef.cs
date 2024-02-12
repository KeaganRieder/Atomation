namespace Atomation.Thing
{
    /// <summary>
    /// config file used ofr all terrain in the game
    /// </summary>
    public class StatDef : ThingDef
    {
        public float BaseValue;
        public float MinValue;
        public float MaxValue;
        //add more things here 

        public StatDef(string name, string description, float baseVal, float min, float max){
            Name = name;
            Description = description;
            BaseValue = baseVal;
            MinValue = min;
            MaxValue = max;
        }
    }
}