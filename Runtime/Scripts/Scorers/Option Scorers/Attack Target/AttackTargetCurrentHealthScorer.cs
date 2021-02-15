using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI option scorer for scoring options of type 'IEntity'. Thus, this scorer evaluates the current health
/// </summary>
public sealed class AttackTargetCurrentHealthScorer : OptionScorerWithScore<IEntity, EnemyContext>
{
    [ApexSerialization, FriendlyName("Multiplier", "A multiplier used to scale the IEntity's current health")]
    public float multiplier = 1f;

    [ApexSerialization]
    public bool reversed = false;

    public override float Score(EnemyContext context, IEntity entity)
    {
        var val = entity.currentHealth * multiplier;
        return reversed ? -val : val;
    }
}
