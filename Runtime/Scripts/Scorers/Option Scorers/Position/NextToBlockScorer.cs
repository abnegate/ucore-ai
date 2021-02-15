using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// An AI option scorer for scoring options of type 'Vector3' (positions). 
/// </summary>
public sealed class NextToBlockScorer : OptionScorerWithScore<Vector3, ContextBase>
{
    [ApexSerialization]
    public float samplingDistance = 1f;

    public override float Score(ContextBase context, Vector3 pos)
    {
        var layer = context.Entity.NavMeshAgent.areaMask;

        // Sample in 4 directions (not diagonal) and use the NavMesh for sampling the position with a very small threshold (Mathf.Epsilon)
        var p1 = new Vector3(pos.x - samplingDistance, 0f, pos.z);
        if (!NavMesh.SamplePosition(p1, out var _, Mathf.Epsilon, layer)) {
            return score;
        }

        var p2 = new Vector3(pos.x + samplingDistance, 0f, pos.z);
        if (!NavMesh.SamplePosition(p2, out var _, Mathf.Epsilon, layer)) {
            return score;
        }

        var p3 = new Vector3(pos.x, 0f, pos.z - samplingDistance);
        if (!NavMesh.SamplePosition(p3, out var _, Mathf.Epsilon, layer)) {
            return score;
        }

        var p4 = new Vector3(pos.x, 0f, pos.z + samplingDistance);
        if (!NavMesh.SamplePosition(p4, out var _, Mathf.Epsilon, layer)) {
            return score;
        }

        return 0f;
    }
}