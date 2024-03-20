using Godot;

namespace Atomation.Thing
{
    /// <summary>
    /// StatModifiers are a things which gets applied to a stat
    /// and then modifies it's values for tell it's un applied
    /// </summary>
    public class StatModifier : Thing
    {
        private float value;
        //do modifier type

        public StatModifier(StatDef configs){
            name = configs.Name;
            description = configs.Description;
            value = configs.BaseValue;
        } 

        public float Value{get; set;}
        //public string Label { get;}

        /// <summary>
        /// increase the stat value by val
        /// </summary>
        public void Increase(float val){
            value += val;
        }
        /// <summary>
        /// decrease the stat value by val
        /// </summary>
        public void Decrease(float val){
            value -= val;
        }
    }
}