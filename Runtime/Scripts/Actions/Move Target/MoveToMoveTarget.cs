using Apex.AI;

/// <summary>
/// This AI action makes the entity move towards its current move target, if it has a valid value.
/// </summary>
public sealed class MoveToMoveTarget : ActionBase<EnemyContext>
{
    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;
        //if (!entity.moveTarget.HasValue) {
        //    return;
        //}

        entity.MoveTo(entity.moveTarget.Value);
    }
}
