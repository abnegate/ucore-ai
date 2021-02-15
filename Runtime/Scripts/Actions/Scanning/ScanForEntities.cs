using Apex.AI;
using UnityEngine;

/// <summary>
/// This AI action handles the scanning of other entities by using Unity's OverlapSphere and adding an observation for each other valid entity, including recording the visibility state.
/// </summary>
public sealed class ScanForEntities : ActionBase<EnemyContext>
{
    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;

        var entityManager = EntityManager.instance;

        var hits = Physics.OverlapSphere(
            entity.position,
            entity.entityScanRange,
            LayersManager.instance.enemiesLayer);
        for (int i = 0; i < hits.Length; i++) {
            var hit = hits[i];

            var hitEntity = entityManager.GetEntity(hit.gameObject);
            if (hitEntity == null) {
                continue;
            }

            if (ReferenceEquals(hitEntity, entity)) {
                continue;
            }

            if (hitEntity.isDead) {
                continue;
            }

            var visibility = ApexUtils.IsVisible(entity.position, hitEntity.position, entity.entityScanRange);

            var observation = new Observation(hitEntity, visibility);
            context.memory.AddOrUpdateObservation(observation);
        }
    }
}
