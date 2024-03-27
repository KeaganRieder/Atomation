using Godot;

namespace Atomation.Thing
{
    /// <summary>
    /// species how the stat modifier gets applied
    /// </summary>
    public enum OldStatModifierType{
        Multiplicative,
        Additive,

    }

    /// <summary>
    /// StatModifiers are a things which gets applied to a stat
    /// and then modifies it's values for tell it's un applied
    /// </summary>
    public class StatModifier : Thing
    {
        public OldStatModifierType ModifierType{get; private set;}
        private int order;
        private float value;

        public StatModifier(StatDef configs){
            name = configs.Name;
            description = configs.Description;
            value = configs.BaseValue;
            ModifierType = configs.ModifierType;
            order = configs.ModifierOrder;
        } 

        public float Value{get; set;}
        public override string Key { get {return order + "" + name;}}

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