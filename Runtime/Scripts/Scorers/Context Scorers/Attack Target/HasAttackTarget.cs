using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating whether the entity has an attack target or not.
/// </summary>
public sealed class HasAttackTarget : ContextualScorerBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Not", "If set to true, the logic is reversed, e.g. used if the desire is to score when there is no attack target (in this case)")]
    public bool not = false;

    public override float Score(ContextBase context)
    {
        if (context.Entity.CurrentAttackTarget == null) {
            return not ? score : 0f;
        }

        return not ? 0f : score;
    }
}
