using Apex.AI;

/// <summary>
/// This AI action finds and sets the best move target by evaluating all the sampled positions by calculating the score for each position through the list of scorers on this action.
/// </summary>
public sealed class SetBestMoveTarget : SetMoveTargetBase
{
    public override void Execute(ContextBase context)
    {
        var best = GetBest(context, context.SampledPositions);
        if (best.sqrMagnitude == 0f) {
            return;
        }
        context.Entity.CurrentMoveTarget = best;
    }
}
