namespace Apex.Examples.AI
{
    using Apex.AI;

    /// <summary>
    /// This AI action makes the entity stop its movement immediately, by issuing 'Stop' to teh nav mesh agent.
    /// </summary>
    public sealed class StopMovement : ActionBase<EnemyContext>
    {
        public override void Execute(EnemyContext context)
        {
            context.entity.navMeshAgent.isStopped = true;
        }
    }
}