using Apex.AI;

/// <summary>
/// This AI action sets the entity's attack target to null
/// </summary>
public sealed class SetAttackTargetToNull : ActionBase<EnemyContext>
{
    public override void Execute(EnemyContext context)
    {
        context.entity.attackTarget = null;
    }
}
