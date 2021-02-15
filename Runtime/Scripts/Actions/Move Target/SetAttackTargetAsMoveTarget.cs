using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// This AI action sets the entity's current attack target as the move target, effectively creating a new move target where the attack target is currently.
/// </summary>
public sealed class SetAttackTargetAsMoveTarget : ActionBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Overwrite Move Target", "Whether to overwrite existing move target if it exists (TRUE)")]
    public bool overwriteMoveTarget = true;

    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;
        if (entity.CurrentAttackTarget == null) {
            return;
        }

        if (!overwriteMoveTarget && entity.CurrentMoveTarget.HasValue) {
            return;
        }

        // Find the nearest sampled position, because we know the sampled positions are valid (on the NavMesh) - so this is to prevent invalid moves
        var pos = entity.CurrentAttackTarget.Position;
        var nearest = Vector3.zero;
        var shortest = float.MaxValue;

        var count = context.SampledPositions.Count;
        for (int i = 0; i < count; i++) {
            var distance = (entity.Position - pos).sqrMagnitude;
            if (distance < shortest) {
                shortest = distance;
                nearest = pos;
            }
        }

        entity.CurrentMoveTarget = nearest;
    }
}
