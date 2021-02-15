using Apex.AI;

/// <summary>
/// This AI action makes the entity move towards its current move target, if it has a valid value.
/// </summary>
public sealed class MoveToMoveTarget : ActionBase<ContextBase>
{
    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;
        //if (!entity.MoveTarget.HasValue) {
        //    return;
        //}

        entity.MoveTo(entity.CurrentMoveTarget.Value);
    }
}
