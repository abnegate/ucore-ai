﻿using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This AI action handles the scanning and storing of valid, walkable positions in a square around the entity.
/// </summary>
public sealed class ScanForPositions : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Sampling Range", "How large a range points are sampled in, in a square with the entity in the center")]
    public float samplingRange = 12f;

    [ApexSerialization, FriendlyName("Sampling Density", "How much distance there is between individual point samples")]
    public float samplingDensity = 1.5f;

    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;

        context.sampledPositions.Clear();

        var halfSamplingRange = samplingRange * 0.5f;
        var pos = entity.position;

        for (var x = -halfSamplingRange; x < halfSamplingRange; x += samplingDensity) {
            for (var z = -halfSamplingRange; z < halfSamplingRange; z += samplingDensity) {
                var sampledPos = new Vector3(pos.x + x, 0f, pos.z + z);

                int mask = entity.navMeshAgent.areaMask;

                if (NavMesh.SamplePosition(
                    sampledPos,
                    out var hit,
                    samplingDensity * 0.5f,
                    mask)) {
                    context.sampledPositions.Add(hit.position);
                }
            }
        }
    }
}
