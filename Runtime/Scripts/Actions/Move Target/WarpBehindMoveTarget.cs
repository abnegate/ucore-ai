using Apex.AI;
using Apex.Serialization;
using UnityEngine;

public class WarpBehindMoveTarget : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Distance Behind", "How far behind the target position to warp in units/meters")]
    public float distanceBehind = 50f;

    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;
        var target = context.entity.attackTarget.position;
        var targetPosition = target + Vector3.back * distanceBehind;

        entity.gameObject.transform.position = targetPosition;
    }
}
