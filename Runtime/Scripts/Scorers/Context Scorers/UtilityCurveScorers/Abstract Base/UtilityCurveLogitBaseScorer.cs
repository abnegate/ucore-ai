using Apex.Serialization;

/// <summary>
/// A base class to use for a specific utility curve scorer, utilizing a logit (inverse of sigmoidal 'logistic' function) curve.
/// </summary>
public abstract class UtilityCurveLogitBaseScorer : UtilityCurveSpecificBaseScorer
{
    [ApexSerialization]
    public float logit = 1f;

    [ApexSerialization]
    public float max = 10f;

    public override float GetScore(float input)
    {
        return reversed ?
            ApexUtils.GetReverseLogit(input, logit, max) * scoreMultiplier :
            ApexUtils.GetLogit(input, logit, max) * scoreMultiplier;
    }
}
