using Apex.AI;
using UnityEngine;

public sealed class ScanForPlayer : ActionBase<ContextBase>
{
    public override void Execute(ContextBase context)
    {
        //var enemy = context.entity;

        //var colliders = Physics.OverlapSphere(
        //    enemy.Position,
        //    ((Enemy)enemy).playerScanRange,
        //    LayersManager.instance.playerLayer);

        //if (colliders == null
        //    || colliders.Length == 0) {
        //    return;
        //}

        //var col = colliders[0];
        //if (col == null
        //    || col.isTrigger) {
        //    return;
        //}

        //context.entity.CurrentAttackTarget = col.gameObject.GetComponent<Player>();
    }
}

