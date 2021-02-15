using Apex.AI;
using Apex.Serialization;
using UCore.Entities;
using UnityEngine;

/// <summary>
/// An AI option scorer for scoring options of type 'IEntity'. In this case, it scores the distance from the context entity to the option entity
/// </summary>
public sealed class NearestAttackTargetScorer : OptionScorerWithScore<IEntity, ContextBase>
{
    [ApexSerialization, FriendlyName("Multiplier", "A multiplier used to scale the calculated magnitude by.")]
    public float multiplier = 1f;

    public override float Score(ContextBase context, IEntity entity)
    {
        var distance = (entity.Position - context.Entity.Position).magnitude * multiplier;
        return Mathf.Max(0f, (score - distance));
    }
}
