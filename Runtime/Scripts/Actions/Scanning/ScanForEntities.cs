using Apex.AI;
using UCore.Entities;
using UnityEngine;

/// <summary>
/// This AI action handles the scanning of other entities by using Unity's OverlapSphere and adding an observation for each other valid entity, including recording the visibility state.
/// </summary>
public sealed class ScanForEntities : ActionBase<ContextBase>
{
    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;

        var entityManager = EntityManager.instance;

        // TODO: Update layers to alloq multi-type scanning
        var hits = Physics.OverlapSphere(
            entity.Position,
            entity.EntityScanRange,
            context.Entity.GameObject.layer);
        for (int i = 0; i < hits.Length; i++) {
            var hit = hits[i];

            var hitEntity = entityManager.GetEntity(hit.gameObject);
            if (hitEntity == null) {
                continue;
            }

            if (ReferenceEquals(hitEntity, entity)) {
                continue;
            }

            if (hitEntity.IsDead) {
                continue;
            }

            var visibility = ApexUtils.IsVisible(
                entity.Position,
                hitEntity.Position,
                entity.EntityScanRange,
                context.Entity.GameObject.layer);

            var observation = new Observation(hitEntity, visibility);

            context.Memory.AddOrUpdateObservation(observation);
        }
    }
}
