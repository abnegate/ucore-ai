using Apex.AI;
using Apex.Serialization;


/// <summary>
/// A base class to use for a specific utility curve scorer, utilizing an exponential curve.
/// </summary>
public abstract class UtilityCurveExponentialBaseScorer : UtilityCurveSpecificBaseScorer
{
    [ApexSerialization, FriendlyName("Exponential Factor", "The exponent to use for the exponential function, i.e. the input variable 'X'.")]
    public float exponentialFactor = 3f;

    [ApexSerialization, FriendlyName("Max", "The maximum expected value for the input factor")]
    public float max = 100f;

    /// <summary>
    /// Gets the utility curve score. Should always return values in the range of 0-1, although this can be overriden by adjusting the parameters.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>
    /// The utility score.
    /// </returns>
    public override float GetScore(float input)
    {
        return reversed ?
            ApexUtils.GetReverseExponential(input, exponentialFactor, max) * scoreMultiplier :
            ApexUtils.GetExponential(input, exponentialFactor, max) * scoreMultiplier;
    }
}
