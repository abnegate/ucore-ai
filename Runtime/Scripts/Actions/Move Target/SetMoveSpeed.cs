using Apex.AI;
using Apex.Serialization;

public class SetMoveSpeed : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Set Move Speed", "Set the movement speed of the entity")]
    public float speed;

    public override void Execute(EnemyContext context)
    {

        context.entity.moveSpeed = speed;
    }
}
