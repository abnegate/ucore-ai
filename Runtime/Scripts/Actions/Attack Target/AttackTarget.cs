using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI action which makes the context unit attack its target and optionally set it to face towards the attack target.
/// </summary>
public sealed class AttackTarget : ActionBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Set Facing", "Whether to also rotate the entity so that it faces towards the attack target")]
    public bool setFacing = true;

    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;
        if (entity.CurrentAttackTarget == null) {
            return;
        }

        if (setFacing) {
            var lookAtPos = entity.Position;
            lookAtPos.y = entity.Position.y;
            entity.GameObject.transform.LookAt(lookAtPos);
        }

        entity.AttackTarget(entity.CurrentAttackTarget);
    }
}