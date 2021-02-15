using System.Collections.Generic;
using Apex.AI;
using UnityEngine;

public class ContextBase : IAIContext
{
    public ContextBase(IAIEntity entity)
    {
        Entity = entity;
        SampledPositions = new List<Vector3>(64);
        Memory = new AIMemory();
    }

    public IAIEntity Entity { get; private set; }

    public AIMemory Memory { get; private set; }

    public List<Vector3> SampledPositions { get; private set; }

    public List<Vector3> HidingSpots { get; private set; }
}
