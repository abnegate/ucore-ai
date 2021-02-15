using UnityEngine;
using Apex.AI.Visualization;

public class SampledPositionsVisualizerDebugComponent : ContextGizmoVisualizerComponent<ContextBase>
{
    public Color gizmosColor = Color.cyan;

    [Range(0.5f, 1f)]
    public float sphereSize = 2f;

    protected override void DrawGizmos(ContextBase context)
    {
        var positions = context.SampledPositions;
        if (positions != null) {
            Gizmos.color = gizmosColor;

            foreach (var position in positions) {
                Gizmos.DrawWireSphere(position, sphereSize);
            }
        }
    }
}
