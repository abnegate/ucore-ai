using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class MoveToRandomPatrolPoint : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Set Move Target", "Set to true to also set move target in addition to issuing a move command")]
    public bool setMoveTarget = true;

    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;
        var index = Random.Range(0, entity.patrolPoints.Length);
        var randomPatrolPos = entity.patrolPoints[index];

        randomPatrolPos.y = entity.position.y;

        int mask = entity.navMeshAgent.areaMask;
        if (!NavMesh.SamplePosition(
            randomPatrolPos,
            out var hit,
            5000f,
            mask)) {
            return;
        }

        if (setMoveTarget) {
            entity.moveTarget = hit.position;
        }

        entity.MoveTo(hit.position);
    }
}
