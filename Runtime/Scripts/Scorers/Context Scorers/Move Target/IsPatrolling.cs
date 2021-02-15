using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating whether the entity is currently patrolling or not.
/// </summary>
public sealed class IsPatrolling : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context)
    {
        if (context.entity.isPatrolling) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
