using Apex.AI;
using Apex.Serialization;
using UnityEngine;

public class IsBehindAttackTarget : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context)
    {
        var entity = context.entity;

        var attackTarget = entity.attackTarget;
        if (attackTarget == null) {
            return 0f;
        }

        var entityForward = entity.gameObject.transform.forward;
        var playerForward = entity.attackTarget.gameObject.transform.forward;

        var dot = Vector3.Dot(entityForward, playerForward);

        var det = (entityForward.x * playerForward.z)
            - (playerForward.z * entityForward.x);

        var angle = Mathf.Atan2(det, dot) * Mathf.Rad2Deg;

        var halfFieldOfView = entity.fieldOfView * 0.5f;

        var facingSameDirection = (angle >= 0 && angle <= halfFieldOfView)
            || (angle >= 360 - halfFieldOfView && angle <= 360);

        if (facingSameDirection) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
