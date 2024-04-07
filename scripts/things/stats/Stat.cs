using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Things
{
    /// <summary>
    /// class used for all stats which can be modified
    /// </summary>
    public class Stat : Thing
    {
        [JsonProperty("Min Value", Order = 1)]
        private float minValue;

        [JsonProperty("Max Value", Order = 2)]
        private float maxValue;
        [JsonProperty("Base Value", Order = 3)]
        private float baseValue;
        private float damage;
        private float currentValue;

        private List<StatModifier> flatModifiers;
        private List<StatModifier> additiveModifiers;
        private List<StatModifier> multiplicativeModifiers;

        private bool updateStat;

        public Stat(){ }

        public Stat(string name, string description, float minValue, float maxValue, float baseValue)
        {
            Name = name;
            Description = description;

            this.minValue = minValue;
            this.maxValue = maxValue;
            this.baseValue = baseValue;
            currentValue = baseValue;

            damage = 0;
            updateStat = false;

            flatModifiers = new List<StatModifier>();
            additiveModifiers = new List<StatModifier>();
            multiplicativeModifiers = new List<StatModifier>();
        }

        public Stat(Stat stat){
            Name = stat.Name;
            Description = stat.Description;

            minValue = stat.minValue;
            maxValue = stat.maxValue;
            baseValue = stat.baseValue;
            currentValue = stat.baseValue;

            damage = 0;
            updateStat = false;

            flatModifiers = new List<StatModifier>();
            additiveModifiers = new List<StatModifier>();
            multiplicativeModifiers = new List<StatModifier>();
        }

        public float Value
        {
            get
            {
                if (updateStat)
                {
                    currentValue = UpdateStat();
                }
                return currentValue;
            }
        }

        /// <summary>
        /// apply damage
        /// </summary>
        public void ApplyDamage(int val)
        {
            damage += Mathf.Abs(val);
            updateStat = true;
        }
        /// <summary>
        /// removes damage
        /// </summary>
        public void RemoveDamage(int val)
        {
            damage -= Mathf.Abs(val);
            updateStat = true;
        }

        /// <summary>
        /// removes modifier
        /// </summary>
        public void AddModifier(StatModifier statModifier)
        {
            if (statModifier.Type == ModifierType.Flat)
            {
                AddModifier(statModifier, flatModifiers);
            }
            else if (statModifier.Type == ModifierType.AdditivePercentage)
            {
                AddModifier(statModifier, additiveModifiers);
            }
            else if (statModifier.Type == ModifierType.MultiplicativePercentage)
            {
                AddModifier(statModifier, multiplicativeModifiers);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void AddModifier(StatModifier statModifier, List<StatModifier> modifierTable)
        {
            updateStat = true;

            modifierTable.Add(statModifier);
        }

        /// <summary>
        /// remove modifier
        /// </summary>
        public void RemoveModifier(StatModifier statModifier)
        {
            if (statModifier.Type == ModifierType.Flat)
            {
                RemoveModifier(statModifier, flatModifiers);
            }
            else if (statModifier.Type == ModifierType.AdditivePercentage)
            {
                RemoveModifier(statModifier, additiveModifiers);
            }
            else if (statModifier.Type == ModifierType.MultiplicativePercentage)
            {
                RemoveModifier(statModifier, multiplicativeModifiers);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// removes all modifiers from a source
        /// </summary>
        public void RemoveModifier(object source)
        {
            RemoveModifier(source, flatModifiers);
            RemoveModifier(source, additiveModifiers);
            RemoveModifier(source, multiplicativeModifiers);
        }

        /// <summary>
        /// remove a modifier
        /// </summary>
        private void RemoveModifier(StatModifier statModifier, List<StatModifier> modifierTable)
        {
            if (modifierTable.Remove(statModifier))
            {
                updateStat = true;
            }
        }

        /// <summary>
        /// removes all modifiers from source
        /// </summary>
        private void RemoveModifier(object source, List<StatModifier> modifierTable)
        {
            for (int i = 0; i < modifierTable.Count; i++)
            {
                if (ReferenceEquals(source, modifierTable[i].Source))
                {
                    modifierTable.RemoveAt(i);
                    updateStat = true;
                }
            }
        }

        /// <summary>
        /// apply dmg and modifiers
        /// </summary>
        private float UpdateStat()
        {
            float final = baseValue;

            float flat = CalculateFlatMods(baseValue);
            float additive = CalculateAdditiveMods();
            final = CalculateMultiplicative(flat + damage + additive);

            updateStat = false;

            final = Mathf.Clamp(final, minValue, maxValue);
            return final;
        }

        private float CalculateFlatMods(float starting)
        {
            float calculated = starting;
            float modifierSum = 0;

            foreach (var modifier in flatModifiers)
            {
                modifierSum += modifier.Value;
            }

            calculated += modifierSum;
            return calculated;
        }
        private float CalculateAdditiveMods()
        {
            float calculated = 0;
            float modifierSum = 0;

            foreach (var modifier in flatModifiers)
            {
                modifierSum += modifier.Value;
            }

            calculated = baseValue *= modifierSum;
            return calculated;
        }
        private float CalculateMultiplicative(float startVal)
        {
            float calculated = startVal;

            foreach (var modifier in flatModifiers)
            {
                calculated *= 1 + modifier.Value;
            }

            return calculated;
        }


    }
}