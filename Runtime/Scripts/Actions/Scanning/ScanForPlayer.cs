using Apex.AI;
using UnityEngine;

public sealed class ScanForPlayer : ActionBase<EnemyContext>
{
    public override void Execute(EnemyContext context)
    {
        var enemy = context.entity;

        var colliders = Physics.OverlapSphere(
            enemy.position,
            ((Enemy)enemy).playerScanRange,
            LayersManager.instance.playerLayer);

        if (colliders == null
            || colliders.Length == 0) {
            return;
        }

        var col = colliders[0];
        if (col == null
            || col.isTrigger) {
            return;
        }

        context.entity.attackTarget = col.gameObject.GetComponent<Player>();
    }
}

