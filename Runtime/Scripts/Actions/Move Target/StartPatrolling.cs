using Apex.AI;
using Apex.Serialization;

/// <summary>
/// This action makes the entity start on its patrol route, as defined by its patrol points. It can optionally set move target and/or issue the actual move command.
/// </summary>
public sealed class StartPatrolling : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Set Move Target", "Set to true to also set move target in addition to issuing a move command")]
    public bool setMoveTarget = true;

    [ApexSerialization, FriendlyName("Issue Move", "Set to true to also issue an actual move command to the entity")]
    public bool issueMove = false;

    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;
        var patrolPoints = entity.patrolPoints;
        var count = patrolPoints.Length;

        if (count == 0) {
            return;
        }

        if (!context.entity.isPatrolling) {
            entity.isPatrolling = true;
            if (!context.entity.isPatrolPaused) {
                entity.currentPatrolIndex = 0;
            }
        } else {
            entity.currentPatrolIndex += 1;
            if (entity.currentPatrolIndex >= count) {
                entity.currentPatrolIndex = 0;
            }
        }

        var destination = patrolPoints[entity.currentPatrolIndex];
        if (setMoveTarget) {
            entity.moveTarget = destination;
        }

        if (issueMove) {
            entity.MoveTo(destination);
        }
    }
}
