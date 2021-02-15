using Apex.AI;

/// <summary>
/// This AI action sets the entity's move target reference to null.
/// </summary>
public sealed class SetMoveTargetToNull : ActionBase<EnemyContext>
{
    public override void Execute(EnemyContext context)
    {
        context.entity.moveTarget = null;
    }
}
