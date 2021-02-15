using Apex.AI;

/// <summary>
/// This AI action sets the entity's attack target to null
/// </summary>
public sealed class SetAttackTargetToNull : ActionBase<ContextBase>
{
    public override void Execute(ContextBase context)
    {
        context.Entity.CurrentAttackTarget = null;
    }
}
