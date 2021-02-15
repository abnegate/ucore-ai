using Apex.AI.Visualization;

using UnityEngine;

public class MoveTargetVisualizerDebugComponent : ContextGizmoVisualizerComponent<EnemyContext>
{
    public Color gizmosColor = Color.yellow;

    [Range(0.5f, 4f)]
    public float sphereSize = 2f;

    protected override void DrawGizmos(EnemyContext context)
    {
        var moveTarget = context.entity.moveTarget;
        if (moveTarget.HasValue) {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(moveTarget.Value, sphereSize);
        }
    }
}
