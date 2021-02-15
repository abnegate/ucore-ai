using System;
using Apex.AI;
using Apex.Serialization;

/// <summary>
/// Base class implementation for general utility curve scorers. 
/// This class exposes an enum field for selecting what type of curve is desired for this scorer.
/// Thereby, this class is easy to use for tweaking and figuring out which mathematical curve is the right one, since the values can be changed in run-time.
/// </summary>
public abstract class UtilityCurveGeneralBaseScorer : IContextualScorer
{
    [ApexSerialization, FriendlyName("Curve Type", "What type of (mathematical) curve to use for the utility curve scoring")]
    public UtilityCurveType curveType = UtilityCurveType.Linear;

    [ApexSerialization, FriendlyName("Curve Factor", "The factor to use for the utility curve scoring method, i.e. all utility curve methods take some factor values, which can be set here-")]
    public float curveFactor = 1f;

    [ApexSerialization, FriendlyName("Curve Max", "The maximum curve factor to use for the utility curve scoring method. The naming of this field can be a bit ambigous, because it covers a wide range of curve functions")]
    public float curveMax = 100f;

    [ApexSerialization, FriendlyName("Score Multiplier", "A factor used to multiply the final utility curve score, e.g. in order to scale the output vales to a certain range")]
    public float scoreMultiplier = 100f;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is disabled.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is disabled; otherwise, <c>false</c>.
    /// </value>
    public bool isDisabled
    {
        get;
        set;
    }

    /// <summary>
    /// Scores the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public abstract float Score(IAIContext context);

    /// <summary>
    /// Gets the utility curve score. Should always return values in the range of 0-1, although this can be overriden by adjusting the parameters.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The utility score</returns>
    /// <exception cref="NotImplementedException">In case new utility curve types are added, they should also have two cases in this method implementation (one case for reversed and one for normal).</exception>
    protected float GetUtilityScore(float input)
    {
        // switch on the desired curve type and return the appropriate scoring method's result, multiplied by the desired score multiplier
        switch (curveType) {
            case UtilityCurveType.Linear: {
                    return ApexUtils.GetLinear(input, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.ReversedLinear: {
                    return ApexUtils.GetReverseLinear(input, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.Exponential: {
                    return ApexUtils.GetExponential(input, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.ReversedExponential: {
                    return ApexUtils.GetReverseExponential(input, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.Logistic: {
                    return ApexUtils.GetLogistic(input, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.ReversedLogistic: {
                    return ApexUtils.GetReverseLogistic(input, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.Logit: {
                    return ApexUtils.GetLogit(input, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.ReversedLogit: {
                    return ApexUtils.GetReverseLogit(input, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.Gaussian: {
                    return ApexUtils.GetGaussian(input, 1f, curveFactor, curveMax) * scoreMultiplier;
                }

            case UtilityCurveType.ReversedGaussian: {
                    return ApexUtils.GetReversedGaussian(input, 1f, curveFactor, curveMax) * scoreMultiplier;
                }

            default: {
                    // throw a not implemented exception if the passed utility curve type has not been implemented in this switch
                    throw new NotImplementedException();
                }
        }
    }
}
