using System.Collections.Generic;
using Godot;

namespace Atomation.Thing
{
    /// <summary>
    /// A stat are statistics, attributes or in short
    /// information about a thing, which can be modified
    /// </summary>
    public class StatOld : Thing
    {
        private float baseVal;
        private float currentValue;
        private float minValue;
        private float maxValue;

        private Dictionary<string, StatModifier> modifiers;
        private bool updateValue;

        public StatOld(StatDef configs)
        {
            name = configs.Name;
            description = configs.Description;
            currentValue = configs.BaseValue;
            baseVal = configs.BaseValue;
            minValue = configs.MinValue;
            maxValue = configs.MaxValue;

            modifiers = new Dictionary<string, StatModifier>();
            updateValue = false;
        }
        public StatOld(string name, string description, float baseVal, float min, float max)
        {
            this.name = name;
            this.description = description;
            this.baseVal = baseVal;
            currentValue = baseVal;
            minValue = min;
            maxValue = max;
        }

        public float Value { get { return CalculateFinal(); } }

        public void AddModifier(StatModifier statModifier)
        {
            if (modifiers.ContainsKey(statModifier.Key))
            {
                updateValue = true;

            }

        }
        public void RemoveModifier(StatModifier statModifier)
        {

            if (modifiers.ContainsKey(statModifier.Key))
            {
                updateValue = true;

            }
        }

        /// <summary>
        /// increase the stat value
        /// </summary>
        public void Increase(float val)
        {
            //StatModifier
        }
        /// <summary>
        /// decrease the stat value
        /// </summary>
        public void Decrease(float val)
        {

        }
        /// <summary>
        /// increase the stat max value
        /// </summary>
        public void IncreaseMax(float val)
        {
            maxValue += val;
        }
        /// <summary>
        /// decrease the stat max value
        /// </summary>
        public void DecreaseMax(float val)
        {
            maxValue -= val;
        }
        /// <summary>
        /// updates current value of the stat using modifiers
        /// </summary>
        private float CalculateFinal()
        {
            if (updateValue)
            {
                //apply modifiers and update value
                updateValue = false;

            }
            return currentValue;
        }
    }
}