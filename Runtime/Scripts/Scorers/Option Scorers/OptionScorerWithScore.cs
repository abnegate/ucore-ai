using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An OptionScorerBase-deriving base class which simply adds a 'Score' field for inputting a score in the AI Editor inspector.
/// </summary>
/// <typeparam name="T">The type for the option scorer to operate on.</typeparam>
public abstract class OptionScorerWithScore<T1, T2> : OptionScorerBase<T1>
    where T2 : IAIContext
{
    [ApexSerialization, FriendlyName("Score", "How much this scorer can score at maximum")]
    public float score = 0f;

    public override float Score(IAIContext context, T1 option)
    {
        return Score((T2)context, option);
    }

    public abstract float Score(T2 context, T1 option);
}
