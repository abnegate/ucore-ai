using Apex.AI;

public class IsHiding : ContextualScorerBase<EnemyContext>
{
    public override float Score(EnemyContext context)
    {
        if (((Enemy)context.entity).IsHiding) {
            return score;
        }
        return 0f;
    }
}
