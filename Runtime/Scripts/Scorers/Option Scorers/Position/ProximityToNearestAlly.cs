using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// An AI option scorer for scoring options of type 'Vector3' (e.g. positions). This option scorer scores positions that are closer to desired range to the nearest ally (nearest ally is calculated for each position). 
/// </summary>
public sealed class ProximityToNearestAlly : OptionScorerWithScore<Vector3, EnemyContext>
{
    [ApexSerialization, FriendlyName("Desired Range", "The desired range to score highest at, i.e. at entities at this range from the option position results in the highest scores.")]
    public float desiredRange = 4f;

    public override float Score(EnemyContext context, Vector3 position)
    {
        var c = (EnemyContext)context;
        var entity = c.entity;

        var observations = c.memory.allObservations;
        var count = observations.Count;
        if (count == 0) {
            return 0f;
        }

        var nearest = Vector3.zero;
        var shortest = float.MaxValue;
        for (int i = 0; i < count; i++) {
            var obs = observations[i];
            if (obs.entity.type != entity.type) {
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
