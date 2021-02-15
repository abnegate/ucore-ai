using Apex.AI;
using Apex.Serialization;
using UCore.Entities;

/// <summary>
/// An AI option scorer for scoring options of type 'IEntity'. In this case, it scores if the option entity is visible from the context entity, within its scan range.
/// </summary>
public sealed class AttackTargetVisibilityScorer : OptionScorerWithScore<IAIEntity, EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context, IEntity attackTarget)
    {
        var entity = context.entity;

        // get the visibility from the context entity to the option entity within the context entity's scan range
        var visibility = ApexUtils.IsVisible(entity.position, attackTarget.position, entity.entityScanRange);
        if (visibility) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
