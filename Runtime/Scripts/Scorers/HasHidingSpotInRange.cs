using System.Collections.Generic;
using System.Linq;
using Apex.AI;
using Apex.Serialization;
using UnityEngine;

public class HasHidingSpotInRange : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public float range;

    public override float Score(EnemyContext context)
    {
        var hidingSpots = context.hidingSpots;
        if (hidingSpots == null
            || hidingSpots.Count == 0) {
            return not ? score : 0f;
        }

        var distances = new List<float>();
        for (int i = 0; i < hidingSpots.Count; i++) {
            distances.Add(Vector3.Distance(
                context.entity.position,
                hidingSpots[i]));
        }
        if (distances.Any(distance => distance <= range)) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
