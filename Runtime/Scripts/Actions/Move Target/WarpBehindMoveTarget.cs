using Apex.AI;
using Apex.Serialization;
using UnityEngine;

public class WarpBehindMoveTarget : ActionBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Distance Behind", "How far behind the target position to warp in units/meters")]
    public float distanceBehind = 50f;

    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;
        var target = context.Entity.CurrentAttackTarget.Position;
        var targetPosition = target + Vector3.back * distanceBehind;

        entity.GameObject.transform.position = targetPosition;
    }
}
