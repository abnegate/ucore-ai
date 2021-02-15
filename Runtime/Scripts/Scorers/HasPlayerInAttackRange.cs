using Apex.AI;
using Apex.Serialization;

public class HasPlayerInAttackingRange : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context)
    {
        var entity = context.entity;
        var attackTarget = entity.attackTarget;

        if (attackTarget == null) {
            return not ? score : 0f;
        }

        var distance = (entity.position - attackTarget.position).sqrMagnitude;
        if (distance <= context.entity.attackRange) {
            return not ? 0f : score;
        }
        return not ? score : 0f;
    }
}
