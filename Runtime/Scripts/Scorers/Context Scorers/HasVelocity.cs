﻿using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating whether the entity has velocity currently, or not.
/// </summary>
public sealed class HasVelocity : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Use Desired Velocity", "There are 2 types of velocity: Actual velocity and desired velocity. The actual velocity is the desired velocity + any undesired forces (e.g. gravity, force push, etc.).")]
    public bool useDesiredVelocity = false;

    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context)
    {
        var entity = context.entity;

        var velocity = useDesiredVelocity ? entity.navMeshAgent.desiredVelocity : entity.navMeshAgent.velocity;
        if (velocity.sqrMagnitude > 0f) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
