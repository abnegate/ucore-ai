using Apex.AI;
using Apex.Serialization;

/// <summary>
/// Base class implementation for specific utility curve scorers
/// </summary>
public abstract class UtilityCurveSpecificBaseScorer : IContextualScorer
{
    [ApexSerialization, FriendlyName("Score Multiplier", "A factor used to multiply the final score by, e.g. to scale the potential output values to a certain range")]
    public float scoreMultiplier = 1f;

    [ApexSerialization, FriendlyName("Reversed", "Whether the curve scoring method should be reversed or not. Reversed usually means that the score is subtracted from 1, before multiplying with the score multiplier")]
    public bool reversed = false;

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
    /// <returns>The score</returns>
    public abstract float Score(IAIContext context);

    /// <summary>
    /// Gets the utility curve score. Should always return values in the range of 0-1, although this can be overriden by adjusting the parameters.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>The utility score</returns>
    public abstract float GetScore(float input);
}
