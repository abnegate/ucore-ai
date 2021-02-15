using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// An AI option scorer for scoring options of type 'Vector3'. This scorer scores higher if the option position is nearer to the contextual entity's current attack target.
/// </summary>
public sealed class NearestToAttackTargetScorer : OptionScorerWithScore<Vector3, ContextBase>
{
    [ApexSerialization, FriendlyName("Distance Multiplier", "A multiplier used to scale the calculated magnitude/distance by.")]
    public float distanceMultiplier = 1f;

    public override float Score(ContextBase context, Vector3 pos)
    {
        var entity = context.Entity;
        var attackTarget = entity.CurrentAttackTarget;
        if (attackTarget == null) {
            return 0f;
        }

        var distance = (attackTarget.Position - pos).magnitude * distanceMultiplier;
        return Mathf.Max(0f, (score - distance));
    }
}
