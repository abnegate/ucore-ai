using Apex.AI;
using Apex.Serialization;


/// <summary>
/// A base class to use for a specific utility curve scorer, utilizing an gaussian curve.
/// </summary>
public abstract class UtilityCurveGaussianBaseScorer : UtilityCurveSpecificBaseScorer
{
    [ApexSerialization, FriendlyName("Curve Height", "The maximum height of the gaussian curve (the amplitude)")]
    public float curveHeight = 1f;

    [ApexSerialization, FriendlyName("Curve Center", "The center point for the gaussian curve")]
    public float curveCenter = 0.5f;

    [ApexSerialization, FriendlyName("Curve Width", "The width of the gaussian curve.")]
    public float curveWidth = 0.2f;

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
            ApexUtils.GetReversedGaussian(input, curveHeight, curveCenter, curveWidth) * scoreMultiplier :
            ApexUtils.GetGaussian(input, curveHeight, curveCenter, curveWidth) * scoreMultiplier;
    }
}
