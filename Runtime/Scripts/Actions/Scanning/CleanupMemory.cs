using Apex.AI;
using Apex.Serialization;
using UnityEngine;

public sealed class CleanupMemory : ActionBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Visibility Expiration Threshold", "How many seconds old an observation is before its 'isVisible' is set to false")]
    public float visibilityExpirationThreshold = 1.5f;

    public override void Execute(ContextBase context)
    {
        var memory = context.Memory;
        var observations = memory.allObservations;
        var count = observations.Count;

        if (count == 0) {
            return;
        }

        for (int i = count - 1; i >= 0; i--) {
            var obs = observations[i];
            if (obs.entity == null || obs.entity.IsDead) {
                memory.RemoveObservationAt(i);
            } else if ((Time.time - obs.timestamp) >= visibilityExpirationThreshold) {
                obs.isVisible = false;
            }
        }
    }
}
