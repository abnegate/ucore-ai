using Apex.AI;
using Apex.Serialization;

public class SetMoveSpeed : ActionBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Set Move Speed", "Set the movement speed of the entity")]
    public float speed;

    public override void Execute(ContextBase context)
    {

        context.Entity.MoveSpeed = speed;
    }
}
