using Apex.AI;
using Apex.Serialization;

/// <summary>
/// This AI action makes the entity stop its patrolling, either permanently or as a 'pause' of the patrolling.
/// </summary>
public sealed class StopPatrolling : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Pause Patrol", "Set to true to not stop the patrolling, but just pause it, so that it can continue from where it came.")]
    public bool pausePatrol = true;

    public override void Execute(EnemyContext context)
    {
        context.entity.isPatrolling = false;

        if (pausePatrol) {
            context.entity.isPatrolPaused = true;
            return;
        }

        context.entity.currentPatrolIndex = 0;
    }
}
