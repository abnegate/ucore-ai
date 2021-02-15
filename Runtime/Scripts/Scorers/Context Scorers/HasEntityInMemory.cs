using Apex.AI;
using Apex.Serialization;
using UCore.Entities;

/// <summary>
/// This AI scorer can be used in a range of situations. It can evaluate whether a given entity has any observations in memory fulfilling a range of variable 'filters', e.g. visibility, entity type or whether they are allied or not.
/// </summary>
public sealed class HasEntityInMemory : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Entity Type", "Only entities in memory of this desired type are considered, unless set to 'Any'")]
    public EntityType entityType = EntityType.Any;

    [ApexSerialization, FriendlyName("Custom Range", "The custom range to consider other entities at, set to '0' to disable completely"), MemberDependency("useScanRange", false), MemberDependency("useAttackRange", false)]
    public float customRange = 0f;

    [ApexSerialization, FriendlyName("Use Scan Range", "Whether to use the entity's scanning range as the maximum allowed range for enmies to factor in to the count"), MemberDependency("useAttackRange", false)]
    public bool useScanRange = true;

    [ApexSerialization, FriendlyName("Use Attack Range", "Whether to use the entity's attack range as the maximum allowed range for enemies to factor in to the count"), MemberDependency("useScanRange", false)]
    public bool useAttackRange = false;

    [ApexSerialization, FriendlyName("Only Visible", "Whether to filter out any currently non-visible entities in memory")]
    public bool onlyVisible = false;

    [ApexSerialization, FriendlyName("Skip Allies", "Whether to skip other entities of the same type as this entity (which are considered allies)")]
    public bool skipAllies = false;

    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context)
    {
        var entity = context.entity;

        var observations = context.memory.allObservations;
        var count = observations.Count;
        if (count == 0) {
            return 0f;
        }

        float rangeSqr;
        if (useScanRange) {
            rangeSqr = entity.entityScanRange * entity.entityScanRange;
        } else if (useAttackRange) {
            rangeSqr = entity.attackRange * entity.attackRange;
        } else {
            rangeSqr = customRange * customRange;
        }

        for (int i = 0; i < count; i++) {
            var obs = observations[i];
            if (skipAllies && obs.entity.type == entity.type) {
                continue;
            }

            if (entityType != EntityType.Any && obs.entity.type != entityType) {
                continue;
            }

            if (onlyVisible && !obs.isVisible) {
                continue;
            }

            if (rangeSqr > 0f) {
                var distance = (obs.position - entity.position).sqrMagnitude;
                if (distance > rangeSqr) {
                    continue;
                }
            }
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
