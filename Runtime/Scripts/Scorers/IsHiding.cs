using Apex.AI;

public class IsHiding : ContextualScorerBase<ContextBase>
{
    public override float Score(ContextBase context)
    {
        if (((Enemy)context.Entity).IsHiding) {
            return score;
        }
        return 0f;
    }
}
