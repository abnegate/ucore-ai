using Apex.AI.Visualization;

using UnityEngine;

public class AttackTargetVisualizerDebugComponent : ContextGizmoVisualizerComponent<ContextBase>
{
    public Color gizmosColor = Color.red;

    [Range(0.5f, 4f)]
    public float sphereSize = 2f;

    protected override void DrawGizmos(ContextBase context)
    {
        var attackTarget = context.Entity.CurrentAttackTarget;
        if (attackTarget != null) {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(attackTarget.Position, sphereSize);
        }
    }
}
