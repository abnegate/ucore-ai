using System.Collections.Generic;
using UCore.Entities;
using UnityEngine;
using UnityEngine.AI;


public interface IAIEntity : IEntity
{
    /// <summary>
    /// Gets the Unity NavMeshAgent.
    /// </summary>
    /// <value>
    /// The navigation mesh agent.
    /// </value>
    NavMeshAgent NavMeshAgent { get; }

    /// <summary>
    /// Gets or sets the current move target.
    /// </summary>
    /// <value>
    /// The move target.
    /// </value>
    Vector3? CurrentMoveTarget { get; set; }

    /// <summary>
    /// Gets or sets the current attack target.
    /// </summary>
    /// <value>
    /// The attack target.
    /// </value>
    IEntity CurrentAttackTarget { get; set; }

    /// <summary>
    /// Gets the scan range.
    /// </summary>
    /// <value>
    /// The scan range.
    /// </value>
    float EntityScanRange { get; }

    /// <summary>
    /// Gets the scan range.
    /// </summary>
    /// <value>
    /// The scan range.
    /// </value>
    float EntityHearingRange { get; }

    /// <summary>
    /// Gets a value indicating whether this instance can communicate.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance can communicate; otherwise, <c>false</c>.
    /// </value>
    bool CanCommunicate { get; }

    /// <summary>
    /// Gets the patrol points.
    /// </summary>
    /// <value>
    /// The patrol points.
    /// </value>
    Vector3[] PatrolPoints { get; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is patrolling.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is patrolling; otherwise, <c>false</c>.
    /// </value>
    bool IsPatrolling { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instances patrol is paused.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instances patrol is paused; otherwise, <c>false</c>.
    /// </value>
    bool IsPatrolPaused { get; set; }

    /// <summary>
    /// Gets the index of the current patrol destination.
    /// </summary>
    /// <value>
    /// The index of the current patrol destination.
    /// </value>
    int CurrentPatrolIndex { get; set; }

    /// <summary>
    /// Orders this entity to move to the supplied destination by utilizing the NavMeshAgent.
    /// </summary>
    /// <param name="destination">The destination.</param>
    void MoveTo(Vector3 destination);

    /// <summary>
    /// Receives a list of communicated memory observations and adds newer observations to own memory.
    /// </summary>
    /// <param name="observations">The observations.</param>
    void ReceiveCommunicatedMemory(IList<Observation> observations);
}
