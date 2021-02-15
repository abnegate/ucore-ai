using Apex.AI.Visualization;

using UnityEngine;

public class MoveTargetVisualizerDebugComponent : ContextGizmoVisualizerComponent<ContextBase>
{
    public Color gizmosColor = Color.yellow;

    [Range(0.5f, 4f)]
    public float sphereSize = 2f;

    protected override void DrawGizmos(ContextBase context)
    {
        var moveTarget = context.Entity.CurrentMoveTarget;
        if (moveTarget.HasValue) {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(moveTarget.Value, sphereSize);
        }
    }
}
