using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This AI action makes the entity move to a randomly calculated walkable position, optionally also sets a move target.
/// </summary>
public sealed class MoveToRandomPosition : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Destination Range", "How far away the random position should be in units/meters")]
    public float destinationRange = 10f;

    [ApexSerialization, FriendlyName("Max Sample Distance", "How far away at maximum from the desired destination is allowed for the navmesh position sampling")]
    public float maxSampleDistance = 2f;

    [ApexSerialization, FriendlyName("Set Move Target", "Set to true to also set move target in addition to issuing a move command")]
    public bool setMoveTarget = true;

    public override void Execute(EnemyContext context)
    {

        var entity = context.entity;
        var range = Mathf.Max(1f, destinationRange);
        var randomPos = entity.position + (Random.onUnitSphere * range);

        randomPos.y = entity.position.y;

        int mask = entity.navMeshAgent.areaMask;
        if (!NavMesh.SamplePosition(
            randomPos,
            out var hit,
            maxSampleDistance,
            mask)) {
            return;
        }

        if (setMoveTarget) {
            entity.moveTarget = hit.position;
        }

        entity.MoveTo(hit.position);
    }
}
