using Apex.AI.Visualization;

using UnityEngine;

public class AttackTargetVisualizerDebugComponent : ContextGizmoVisualizerComponent<EnemyContext>
{
    public Color gizmosColor = Color.red;

    [Range(0.5f, 4f)]
    public float sphereSize = 2f;

    protected override void DrawGizmos(EnemyContext context)
    {
        var attackTarget = context.entity.attackTarget;
        if (attackTarget != null) {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(attackTarget.position, sphereSize);
        }
    }
}
