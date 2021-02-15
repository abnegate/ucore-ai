using Apex.AI;
using Apex.Serialization;
using UnityEngine;

public class HasPlayerInRange : ContextualScorerBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public float range;

    public override float Score(ContextBase context)
    {
        var player = context.Entity.CurrentAttackTarget;
        if (player == null) {
            return not ? score : 0f;
        }
        if (Vector3.Distance(
            context.Entity.Position,
            player.Position) <= range) {
            return not ? 0f : score;
        }
        return not ? score : 0f;
    }
}
