using Apex.AI;
using Apex.Serialization;
using UCore.Entities;

/// <summary>
/// An AI option scorer for scoring options of type 'IEntity'. Thus, this scorer evaluates the current health
/// </summary>
public sealed class AttackTargetCurrentHealthScorer : OptionScorerWithScore<IEntity, ContextBase>
{
    [ApexSerialization, FriendlyName("Multiplier", "A multiplier used to scale the IEntity's current health")]
    public float multiplier = 1f;

    [ApexSerialization]
    public bool reversed = false;

    public override float Score(ContextBase context, IEntity entity)
    {
        var val = entity.CurrentHealth * multiplier;
        return reversed ? -val : val;
    }
}
