using Apex.AI;
using Apex.Serialization;

/// <summary>
/// This action makes the entity start on its patrol route, as defined by its patrol points. It can optionally set move target and/or issue the actual move command.
/// </summary>
public sealed class StartPatrolling : ActionBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Set Move Target", "Set to true to also set move target in addition to issuing a move command")]
    public bool setMoveTarget = true;

    [ApexSerialization, FriendlyName("Issue Move", "Set to true to also issue an actual move command to the entity")]
    public bool issueMove = false;

    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;
        var patrolPoints = entity.PatrolPoints;
        var count = patrolPoints.Length;

        if (count == 0) {
            return;
        }

        if (!context.Entity.IsPatrolling) {
            entity.IsPatrolling = true;
            if (!context.Entity.IsPatrolPaused) {
                entity.CurrentPatrolIndex = 0;
            }
        } else {
            entity.CurrentPatrolIndex += 1;
            if (entity.CurrentPatrolIndex >= count) {
                entity.CurrentPatrolIndex = 0;
            }
        }

        var destination = patrolPoints[entity.CurrentPatrolIndex];
        if (setMoveTarget) {
            entity.CurrentMoveTarget = destination;
        }

        if (issueMove) {
            entity.MoveTo(destination);
        }
    }
}
