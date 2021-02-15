using Apex.AI;
using Apex.Serialization;
using UnityEngine;

public class HasPlayerInRange : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public float range;

    public override float Score(EnemyContext context)
    {
        var player = context.entity.attackTarget;
        if (player == null) {
            return not ? score : 0f;
        }
        if (Vector3.Distance(
            context.entity.position,
            player.position) <= range) {
            return not ? 0f : score;
        }
        return not ? score : 0f;
    }
}
