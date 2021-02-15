using Apex.AI;

/// <summary>
/// A specific utility curve scorer for calculating a utility score value depending on the entity's current health
/// </summary>
public sealed class UtilityCurveHealthScorer : UtilityCurveLinearBaseScorer
{
    public override float Score(IAIContext context)
    {
        return GetScore(((ContextBase)context).Entity.CurrentHealth);
    }
}