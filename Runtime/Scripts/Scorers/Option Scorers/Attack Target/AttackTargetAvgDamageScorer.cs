using Apex.AI;
using Apex.Serialization;
using UCore.Entities;

/// <summary>
/// An AI option scorer for scoring options of type 'IEntity'. Thereby, this scorer can be used as an element in the list of an ActionWithOptions (of type IEntity)
/// </summary>
public sealed class AttackTargetAvgDamageScorer : OptionScorerWithScore<IEntity, ContextBase>
{
    [ApexSerialization, FriendlyName("Multiplier", "A multiplier to scale the average damage by")]
    public float multiplier = 1f;

    [ApexSerialization, FriendlyName("Reversed", "If set to true, instead of returning e.g. +100, it will output -100")]
    public bool reversed = false;

    public override float Score(ContextBase context, IEntity attackTarget)
    {
        var val = (attackTarget.MinDamage + attackTarget.MaxDamage) * 0.5f * multiplier;
        return reversed ? -val : val;
    }
}
