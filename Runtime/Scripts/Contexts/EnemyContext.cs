using System.Collections.Generic;
using Apex.AI;
using UCore.Entities;
using UnityEngine;

public sealed class EnemyContext : IAIContext
{
    public EnemyContext(IAIEntity entity)
    {
        this.entity = entity;
        sampledPositions = new List<Vector3>(64);
        memory = new AIMemory();
    }

    public IAIEntity entity { get; private set; }

    public AIMemory memory { get; private set; }

    public List<Vector3> sampledPositions { get; private set; }

    public List<Vector3> hidingSpots { get; private set; }
}
