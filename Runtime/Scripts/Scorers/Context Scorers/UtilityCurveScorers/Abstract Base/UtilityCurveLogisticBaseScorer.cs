using Apex.AI;
using Apex.Serialization;

/// <summary>
/// A base class to use for a specific utility curve scorer, utilizing a logistic (sigmoid) curve.
/// </summary>
public abstract class UtilityCurveLogisticBaseScorer : UtilityCurveSpecificBaseScorer
{
    [ApexSerialization, FriendlyName("Log Base", "The base for the logistic curve.")]
    public float logBase = 1f;

    [ApexSerialization, FriendlyName("Midpoint", "The midpoint for the logistic curve.")]
    public float midpoint = 1f;

    /// <summary>
    /// Gets the utility curve score. Should always return values in the range of 0-1, although this can be overriden by adjusting the parameters.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>
    /// The utility score
    /// </returns>
    public override float GetScore(float input)
    {
        return reversed ?
            ApexUtils.GetReverseLogistic(input, logBase, midpoint) * scoreMultiplier :
            ApexUtils.GetLogistic(input, logBase, midpoint) * scoreMultiplier;
    }
}
