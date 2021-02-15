using Apex.AI;
using Apex.Serialization;
using UnityEngine;

/// <summary>
/// An AI option scorer for scoring options of type 'IEntity'. In this case, it scores the distance from the context entity to the option entity
/// </summary>
public sealed class NearestAttackTargetScorer : OptionScorerWithScore<IEntity, EnemyContext>
{
    [ApexSerialization, FriendlyName("Multiplier", "A multiplier used to scale the calculated magnitude by.")]
    public float multiplier = 1f;

    public override float Score(EnemyContext context, IEntity entity)
    {
        var ctx = (EnemyContext)context;
        var distance = (entity.position - ctx.entity.position).magnitude * multiplier;
        return Mathf.Max(0f, (score - distance));
    }
}
