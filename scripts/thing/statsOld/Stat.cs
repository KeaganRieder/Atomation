using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Thing
{
    /// <summary>
    /// todo
    /// </summary>
    public class StatOld : StatBase
    {
        [JsonProperty]
        protected float currentVal;
        [JsonProperty]
        protected float minValue;
        [JsonProperty]
        protected float maxValue;
        protected Dictionary<string, StatModifierOld> modifers;
        private bool updateValues = false;

        public StatOld()
        {
            modifers = new Dictionary<string, StatModifierOld>();
        }
        public StatOld(string name, string description, float baseVal, float min, float max)
        {
            modifers = new Dictionary<string, StatModifierOld>();
            this.name = name;
            this.description = description;
            baseValue = baseVal;
            currentVal = baseVal;
            minValue = min;
            maxValue = max;
        }
        public StatOld(StatDefOld config)
        {
            modifers = new Dictionary<string, StatModifierOld>();
            name = config.Name;
            description = config.Description;
            baseValue = config.BaseValue;
            currentVal = baseValue;
            minValue = config.MinValue;
            maxValue = config.MaxValue;
        }

        public override float Value
        {
            get
            {
                if (updateValues)
                {
                    ApplyModifers();
                    return currentVal;
                }
                return currentVal;
            }
            set
            {
                baseValue = value;
            }
        }
        [JsonIgnore]
        public float Min { get => minValue; private set { minValue = value; } }
        [JsonIgnore]
        public float MaX { get => maxValue; private set { maxValue = value; } }

        public static float operator +(StatOld obj, float val)
        {
            obj.baseValue = Mathf.Clamp(obj.baseValue + val, obj.minValue, obj.MaX);
            obj.currentVal = Mathf.Clamp(obj.currentVal + val, obj.minValue, obj.MaX);
            return obj.Value;
        }
        public static float operator -(StatOld obj, float val)
        {
            obj.baseValue = Mathf.Clamp(obj.baseValue - val, obj.minValue, obj.MaX);
            obj.currentVal = Mathf.Clamp(obj.currentVal - val, obj.minValue, obj.MaX);
            return obj.Value;
        }

        public void AddModifer(StatModifierOld modifer)
        {
            if (modifers.ContainsKey(modifer.Name))
            {
                GD.Print($"Warning modifer {modifer.Name} is already present in {this.Name}");
            }
            else
            {
                modifers.Add(modifer.Name, modifer);
            }
        }
        public void RemoveModifer(StatModifierOld modifer)
        {
            if (modifers.ContainsKey(modifer.Name))
            {
                modifers.Add(modifer.Name, modifer);

            }
            else
            {
                throw new KeyNotFoundException($"Modifer {modifer.Name} isn't presnt in {this.name}");
            }
        }

        private void ApplyModifers()
        {
            currentVal = baseValue;
            foreach (StatModifierOld modifer in modifers.Values)
            {
                if (currentVal == minValue || currentVal == maxValue)
                {
                    break;
                }
                currentVal = Mathf.Clamp(currentVal + modifer.Value, Min, MaX);
            }

            updateValues = false;
        }
    }
}