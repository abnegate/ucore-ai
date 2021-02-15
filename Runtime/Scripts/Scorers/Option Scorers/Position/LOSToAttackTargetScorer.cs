using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// An AI option scorer for scoring options of type 'Vector3' (e.g. positions). In this case, it scores if there is visibility from the option position to the context entity's current attack target.
/// </summary>
public sealed class LOSToAttackTargetScorer : OptionScorerWithScore<Vector3, EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context, Vector3 pos)
    {
        var entity = context.entity;

        var visibility = ApexUtils.IsVisible(
            entity.position,
            pos,
            entity.entityScanRange);

        if (visibility) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
