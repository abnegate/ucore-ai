using Apex.AI.Visualization;
using UnityEngine;

public sealed class AttackTargetHealthVisualizerDebugComponent : ContextGizmoGUIVisualizerComponent<ContextBase>
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

    protected override void DrawGUI(ContextBase context)
    {
        var attackTarget = context.Entity.CurrentAttackTarget;
        if (attackTarget != null) {
            var health = attackTarget.CurrentHealth / attackTarget.MaxHealth;
            GUI.color = new Color((1f - health), 0f, 0f, 1f);

            var pos = Camera.main.WorldToScreenPoint(attackTarget.Position);
            pos.y = Screen.height - pos.y;
            GUI.Label(new Rect(pos.x, pos.y, 60f, 30f), string.Concat("HP: ", health.ToString("F1"), "%"));
        }
    }
}
