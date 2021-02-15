using UnityEngine;

public static class ApexUtils
{
    /// <summary>
    /// Gets whether the supplied 'to' position is visible in relation to the 'from' position, not extending the supplied range.
    /// </summary>
    /// <param name="from">From position.</param>
    /// <param name="to">To position.</param>
    /// <param name="range">The maximum range.</param>
    /// <param name="layers">The layers that linecast can hit (e.g. obstacles layer).</param>
    /// <returns>True if the to position is visible and within range of the from position.</returns>
    public static bool IsVisible(Vector3 from, Vector3 to, float range, LayerMask obstacleLayer)
    {
        if (range == 0f) {
            return false;
        }

        var sqrMag = (to - from).sqrMagnitude;
        if (sqrMag == 0f) {
            return true;
        }

        if (sqrMag > (range * range)) {
            return false;
        }

        var blockHit = Physics.Linecast(from, to, obstacleLayer);
        if (blockHit) {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Determines whether the supplied compare operator is true, by comparing the supplied value against the supplied comparison.
    /// </summary>
    /// <param name="op">The operation to use for comparing value to comparison.</param>
    /// <param name="value">The value.</param>
    /// <param name="comparison">The comparison to use for comparing value against.</param>
    /// <returns>True if the compare operator evaluates true</returns>
    public static bool IsOperatorTrue(RangeOperator op, float value, float comparison)
    {
        return (op == RangeOperator.None ||
               (op == RangeOperator.Equals && value == comparison) ||
               (op == RangeOperator.GreaterThan && value > comparison) ||
               (op == RangeOperator.GreaterThanOrEquals && value >= comparison) ||
               (op == RangeOperator.LessThan && value < comparison) ||
               (op == RangeOperator.LessThanOrEquals && value <= comparison));
    }

    /// <summary>
    /// Gets the exponential function/curve value at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="exp">The exponent.</param>
    /// <param name="max">The maximum expected value of the input.</param>
    /// <returns>The exponential value (Y-value) on the curve, based on the parameters.</returns>
    public static float GetExponential(float input, float exp, float max = 100f)
    {
        return Mathf.Pow((max - input), exp) / Mathf.Pow(max, exp);
    }

    /// <summary>
    /// Gets the reverse exponential value at input. Reversed in this case means that the exponential value is subtracted from 1.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="exp">The exponent.</param>
    /// <param name="max">The maximum expected value of the input.</param>
    /// <returns>The reversed exponential value (Y-value) on the curve, based on the parameters. Reversed in this case means that the exponential value is subtracted from 1.</returns>
    public static float GetReverseExponential(float input, float exp, float max = 100f)
    {
        return 1f - GetExponential(input, exp, max);
    }

    /// <summary>
    /// Gets the logistic function/curve value at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="logBase">The base for the logistic function.</param>
    /// <param name="midpoint">The midpoint of the logistic curve.</param>
    /// <returns>The logistic value (Y-value) on the logistic curve, based on the parameters.</returns>
    public static float GetLogistic(float input, float logBase, float midpoint)
    {
        return 1f / (1f + Mathf.Pow(logBase, (input + midpoint)));
    }

    /// <summary>
    /// Gets the reversed logistic function/curve value at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="logBase">The log base.</param>
    /// <param name="midpoint">The midpoint.</param>
    /// <returns>The reversed logistic value (Y-value) on the logistic curve, based on the parameters. Reversed in this case means that the logistic value is subtracted from 1.</returns>
    public static float GetReverseLogistic(float input, float logBase, float midpoint)
    {
        return 1f - GetLogistic(input, logBase, midpoint);
    }

    /// <summary>
    /// Gets the linear function/curve at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="slope">The slope of the curve.</param>
    /// <param name="max">The maximum expected value for the input.</param>
    /// <returns>The linear value (Y-value)</returns>
    public static float GetLinear(float input, float slope = 1f, float max = 100f)
    {
        return Mathf.Max(Mathf.Min(((input / max) * -slope) + slope, 1f), 0f);
    }

    /// <summary>
    /// Gets the reverse linear function at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="slope">The slope of the curve.</param>
    /// <param name="max">The maximum expected value for the input.</param>
    /// <returns>The reversed linear value (Y-value)</returns>
    public static float GetReverseLinear(float input, float slope = 1f, float max = 100f)
    {
        return 1f - GetLinear(input, slope, max);
    }

    /// <summary>
    /// Gets the logit function at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="logit">The logit.</param>
    /// <param name="max">The maximum.</param>
    /// <returns>The logit value</returns>
    public static float GetLogit(float input, float logit, float max = 10f)
    {
        return (Mathf.Log((input / 100f) / (1f - (input / 100f)), logit) + 5f) / max;
    }

    /// <summary>
    /// Gets the reverse logit function at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="logit">The logit.</param>
    /// <param name="max">The maximum.</param>
    /// <returns>The reversed logit value</returns>
    public static float GetReverseLogit(float input, float logit, float max = 10f)
    {
        return 1f - GetLogit(input, logit, max);
    }

    /// <summary>
    /// Gets the gaussian function at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="height">The height.</param>
    /// <param name="center">The center.</param>
    /// <param name="width">The width.</param>
    /// <returns>The Gaussian value</returns>
    public static float GetGaussian(float input, float height, float center, float width)
    {
        return height * Mathf.Exp(-(Mathf.Pow((input / 100f) - center, 2f) / (2f * Mathf.Pow(width, 2f))));
    }

    /// <summary>
    /// Gets the reversed gaussian function at input.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="height">The height.</param>
    /// <param name="center">The center.</param>
    /// <param name="width">The width.</param>
    /// <returns>The reversed Gaussian value</returns>
    public static float GetReversedGaussian(float input, float height, float center, float width)
    {
        return 1f - GetGaussian(input, height, center, width);
    }
}
