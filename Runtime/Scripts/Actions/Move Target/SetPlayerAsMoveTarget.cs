using Apex.AI;

public class SetPlayerAsMoveTarget : ActionBase<EnemyContext>
{
    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;
        var playerPos = entity.attackTarget.position;
        entity.moveTarget = playerPos;
    }
}
