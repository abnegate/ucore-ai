using Apex.AI;

/// <summary>
/// This AI action sets the entity's move target reference to null.
/// </summary>
public sealed class SetMoveTargetToNull : ActionBase<ContextBase>
{
    public override void Execute(ContextBase context)
    {
        context.Entity.CurrentMoveTarget = null;
    }
}
