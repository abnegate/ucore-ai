using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating whether the entity's current attack target is dead or not.
/// </summary>
public sealed class IsAttackTargetDead : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context)
    {
        var entity = context.entity;

        if (entity.attackTarget == null) {
            return 0f;
        }

        if (entity.attackTarget.isDead) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
