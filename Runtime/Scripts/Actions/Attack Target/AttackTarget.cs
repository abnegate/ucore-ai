using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// An AI action which makes the context unit attack its target and optionally set it to face towards the attack target.
/// </summary>
public sealed class AttackTarget : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Set Facing", "Whether to also rotate the entity so that it faces towards the attack target")]
    public bool setFacing = true;

    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;
        if (entity.attackTarget == null) {
            return;
        }

        if (setFacing) {
            var lookAtPos = entity.position;
            lookAtPos.y = entity.position.y;
            entity.gameObject.transform.LookAt(lookAtPos);
        }

        Debug.Log("Attacking player");
        entity.AttackTarget(entity.attackTarget);
    }
}