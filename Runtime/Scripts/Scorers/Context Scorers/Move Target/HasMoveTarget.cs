using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating whether the entity has a move target currently.
/// </summary>
public sealed class HasMoveTarget : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(EnemyContext context)
    {
        if (!context.entity.moveTarget.HasValue) {
            return not ? score : 0f;
        }

        return not ? 0f : score;
    }
}
