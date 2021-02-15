using Apex.AI;

public abstract class ActionWithOptions<T1, T2> : ActionWithOptions<T1>
    where T2 : IAIContext
{
    public override void Execute(IAIContext context)
    {
        Execute((T2)context);
    }

    public abstract void Execute(T2 context);
}
