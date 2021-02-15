using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class MoveToRandomPatrolPoint : ActionBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Set Move Target", "Set to true to also set move target in addition to issuing a move command")]
    public bool setMoveTarget = true;

    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;
        var index = Random.Range(0, entity.PatrolPoints.Length);
        var randomPatrolPos = entity.PatrolPoints[index];

        randomPatrolPos.y = entity.Position.y;

        int mask = entity.NavMeshAgent.areaMask;
        if (!NavMesh.SamplePosition(
            randomPatrolPos,
            out var hit,
            5000f,
            mask)) {
            return;
        }

        if (setMoveTarget) {
            entity.CurrentMoveTarget = hit.position;
        }

        entity.MoveTo(hit.position);
    }
}
