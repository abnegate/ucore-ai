using Apex.AI;
using Apex.Serialization;

/// <summary>
/// This AI action makes the entity stop its patrolling, either permanently or as a 'pause' of the patrolling.
/// </summary>
public sealed class StopPatrolling : ActionBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Pause Patrol", "Set to true to not stop the patrolling, but just pause it, so that it can continue from where it came.")]
    public bool pausePatrol = true;

    public override void Execute(ContextBase context)
    {
        context.Entity.IsPatrolling = false;

        if (pausePatrol) {
            context.Entity.IsPatrolPaused = true;
            return;
        }

        context.Entity.CurrentPatrolIndex = 0;
    }
}
