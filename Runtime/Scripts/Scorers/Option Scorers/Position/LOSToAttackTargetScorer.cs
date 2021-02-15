using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// An AI option scorer for scoring options of type 'Vector3' (e.g. positions). In this case, it scores if there is visibility from the option position to the context entity's current attack target.
/// </summary>
public sealed class LOSToAttackTargetScorer : OptionScorerWithScore<Vector3, ContextBase>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(ContextBase context, Vector3 pos)
    {
        var entity = context.Entity;

        var visibility = ApexUtils.IsVisible(
            entity.Position,
            pos,
            entity.EntityScanRange,
            entity.CurrentAttackTarget.GameObject.layer);

        if (visibility) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
