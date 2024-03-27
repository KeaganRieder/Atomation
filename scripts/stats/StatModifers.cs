using System.Collections.Generic;
using Godot;

namespace Atomation.Stats
{
    public enum ModifierType
    {
        Flat,
        AdditivePercentage,
        MultiplicativePercentage,
    }

    /// <summary>
    /// class used for all stat modifiers
    /// </summary>
    public class StatModifier : Thing.Thing
    {
        public float Value { get; private set; }
        public object Source { get; private set; }
        public ModifierType Type { get; private set; }

        public StatModifier(StatModifierDef config, object source = null)
        {
            name = config.Name;
            description = config.Description;
            Value = config.BaseValue;
            Type = config.Type;
            Source = source;
        }
    }
}