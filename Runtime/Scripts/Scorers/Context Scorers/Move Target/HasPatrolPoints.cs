using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating whether the entity has any patrol points or not.
/// </summary>
public sealed class HasPatrolPoints : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context)
    {
        var patrolPoints = context.entity.patrolPoints;
        if (patrolPoints != null && patrolPoints.Length > 0) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
