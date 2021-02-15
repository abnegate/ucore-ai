using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// An AI option scorer for scoring options of type 'Vector3'. This option scorer scores positions highest if they are at the desired range, where the nearest enemy is identified for each position.
/// </summary>
public sealed class ProximityToNearestEnemy : OptionScorerWithScore<Vector3, EnemyContext>
{
    [ApexSerialization, FriendlyName("Desired Range", "The desired range at which entities score the highest.")]
    public float desiredRange = 14f;

    public override float Score(EnemyContext context, Vector3 position)
    {
        var entity = context.entity;
        var observations = context.memory.allObservations;
        var count = observations.Count;

        if (count == 0) {
            return 0f;
        }

        var nearest = Vector3.zero;
        var shortest = float.MaxValue;
        for (int i = 0; i < count; i++) {
            var obs = observations[i];
            if (obs.entity.type == entity.type) {
                continue;
            }

            var distance = (entity.position - obs.position).sqrMagnitude;
            if (distance < shortest) {
                shortest = distance;
                nearest = obs.position;
            }
        }

        if (nearest.sqrMagnitude == 0f) {
            return 0f;
        }

        var range = (position - nearest).magnitude;
        return Mathf.Max(0f, score - Mathf.Abs(desiredRange - range));
    }
}
