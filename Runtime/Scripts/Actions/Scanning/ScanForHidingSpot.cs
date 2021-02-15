using System.Collections.Generic;
using Apex.AI;
using UnityEngine;

public sealed class ScanForHidingSpot : ActionBase<ContextBase>
{
    public override void Execute(ContextBase context)
    {
        //var enemy = context.entity;
        //var hidingSpots = context.hidingSpots;

        //if (hidingSpots == null) {
        //    hidingSpots = new List<Vector3>(5);
        //} else {
        //    hidingSpots.Clear();
        //}

        //var colliders = Physics.OverlapSphere(
        //    enemy.Position,
        //    ((Enemy)enemy).hidingSpotScanRange,
        //    LayersManager.instance.hidingSpotsLayer);

        //for (int i = 0; i < colliders.Length; i++) {
        //    var col = colliders[i];

        //    if (col.isTrigger) {
        //        continue;
        //    }

        //    hidingSpots.Add(col.gameObject.transform.position);
        //}
    }
}
