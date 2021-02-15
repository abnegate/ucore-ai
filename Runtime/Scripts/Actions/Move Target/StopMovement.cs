namespace Apex.Examples.AI
{
    using Apex.AI;

    /// <summary>
    /// This AI action makes the entity stop its movement immediately, by issuing 'Stop' to teh nav mesh agent.
    /// </summary>
    public sealed class StopMovement : ActionBase<ContextBase>
    {
        public override void Execute(ContextBase context)
        {
            context.Entity.NavMeshAgent.isStopped = true;
        }
    }
}