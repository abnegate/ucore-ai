using Apex.AI;
using Apex.Serialization;
using UnityEngine;

public class IsBehindAttackTarget : ContextualScorerBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(ContextBase context)
    {
        var entity = context.Entity;

        var attackTarget = entity.CurrentAttackTarget;
        if (attackTarget == null) {
            return 0f;
        }

        var entityForward = entity.GameObject.transform.forward;
        var playerForward = entity.CurrentAttackTarget.GameObject.transform.forward;

        var dot = Vector3.Dot(entityForward, playerForward);

        var det = (entityForward.x * playerForward.z)
            - (playerForward.z * entityForward.x);

        var angle = Mathf.Atan2(det, dot) * Mathf.Rad2Deg;

        var halfFieldOfView = entity.FieldOfView * 0.5f;

        var facingSameDirection = (angle >= 0 && angle <= halfFieldOfView)
            || (angle >= 360 - halfFieldOfView && angle <= 360);

        if (facingSameDirection) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
