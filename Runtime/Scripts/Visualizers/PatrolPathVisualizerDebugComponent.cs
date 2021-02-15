using System.Collections.Generic;
using Apex.AI.Visualization;
using UnityEngine;

public class PatrolPathVisualizerDebugComponent : ContextGizmoVisualizerComponent<EnemyContext>
{
    public List<Transform> patrolPoints;
    public Color gizmosColor = Color.red;

    [Range(0.5f, 4f)]
    public float sphereSize = 1f;

    protected override void DrawGizmos(EnemyContext context)
    {
        var patrolPoints = GameObject.FindGameObjectsWithTag(Tags.patrolPoint);
        if (patrolPoints != null && patrolPoints.Length >= 0) {
            Gizmos.color = gizmosColor;

            foreach (var point in patrolPoints) {
                Gizmos.DrawWireSphere(point.transform.position, sphereSize);
            }
        }
    }
}
