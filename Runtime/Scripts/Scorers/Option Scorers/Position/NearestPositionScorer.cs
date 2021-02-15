using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// An AI option scorer for scoring options of type 'Vector3' (e.g. positions). This scorer scores higher when closer to the contextual entity.
/// </summary>
[FriendlyName("AI Example Nearest Position Scorer")]
public sealed class NearestPositionScorer : OptionScorerWithScore<Vector3, EnemyContext>
{
    [ApexSerialization, FriendlyName("Distance Multiplier", "A multiplier used to scale the calculated magnitude/distance by.")]
    public float distanceMultiplier = 1f;

    public override float Score(EnemyContext context, Vector3 pos)
    {
        var distance = (context.entity.position - pos).magnitude * distanceMultiplier;

        return Mathf.Max(0f, (score - distance));
    }
}
