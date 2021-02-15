using Apex.AI;

public class SetPlayerAsMoveTarget : ActionBase<ContextBase>
{
    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;
        var playerPos = entity.CurrentAttackTarget.Position;
        entity.CurrentMoveTarget = playerPos;
    }
}
