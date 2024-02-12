using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Thing
{
    /// <summary>
    /// todo
    /// </summary>
    public partial class Stat : StatBase
    {
        [JsonProperty]
        protected float currentVal;
        [JsonProperty]
        protected float minValue;
        [JsonProperty]
        protected float maxValue;
        protected Dictionary<string, StatModifer> modifers;
        private bool updateValues = false;

        public Stat()
        {
            modifers = new Dictionary<string, StatModifer>();
        }
        public Stat(string name, string description, float baseVal, float min, float max)
        {
            modifers = new Dictionary<string, StatModifer>();
            this.name = name;
            this.description = description;
            baseValue = baseVal;
            currentVal = baseVal;
            minValue = min;
            maxValue = max;
        }
        public Stat(StatDef config)
        {
            modifers = new Dictionary<string, StatModifer>();
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

        public static float operator +(Stat obj, float val)
        {
            obj.baseValue = Mathf.Clamp(obj.baseValue + val, obj.minValue, obj.MaX);
            obj.currentVal = Mathf.Clamp(obj.currentVal + val, obj.minValue, obj.MaX);
            return obj.Value;
        }
        public static float operator -(Stat obj, float val)
        {
            obj.baseValue = Mathf.Clamp(obj.baseValue - val, obj.minValue, obj.MaX);
            obj.currentVal = Mathf.Clamp(obj.currentVal - val, obj.minValue, obj.MaX);
            return obj.Value;
        }

        public void AddModifer(StatModifer modifer)
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
        public void RemoveModifer(StatModifer modifer)
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
            foreach (StatModifer modifer in modifers.Values)
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