using Apex.AI.Visualization;
using UnityEngine;

public sealed class AttackTargetHealthVisualizerDebugComponent : ContextGizmoGUIVisualizerComponent<EnemyContext>
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

    protected override void DrawGUI(EnemyContext context)
    {
        var attackTarget = context.entity.attackTarget;
        if (attackTarget != null) {
            var health = attackTarget.currentHealth / attackTarget.maxHealth;
            GUI.color = new Color((1f - health), 0f, 0f, 1f);

            var pos = Camera.main.WorldToScreenPoint(attackTarget.position);
            pos.y = Screen.height - pos.y;
            GUI.Label(new Rect(pos.x, pos.y, 60f, 30f), string.Concat("HP: ", health.ToString("F1"), "%"));
        }
    }
}
