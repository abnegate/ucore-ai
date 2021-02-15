using System;
using System.Collections.Generic;
using System.Linq;
using Apex.AI;
using Apex.AI.Components;
using UCore.Entities;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IStealthAIEntity, IContextProvider
{
    #region Ranges
    [Header("Ranges")]
    public float playerScanRange = 500f;
    public float hidingSpotScanRange = 500f;
    public float hearingRange = 300f;
    public float stalkingRange = 200f;
    public float chargeRange = 50f;
    public float attackingRange = 5f;
    #endregion

    #region View
    [Header("View")]
    [Range(1, 180)]
    public float fov = 60f;
    #endregion

    #region Speed
    [Header("Speed")]
    [Range(0f, 5f)]
    public float movementSpeed = 1f;
    public float rotateSpeed = 40f;
    public float sprintRotateSpeed = 60f;
    #endregion

    #region Patrol
    [Header("Patrol")]
    public Vector3[] defaultPatrolPoints;
    #endregion

    #region Attack
    [Header("Attack")]
    [Range(0f, 5f)]
    public float attackSphereCastRadius = 0.5f;
    public float minimumDamage = 1f;
    public float maximumDamage = 10f;
    #endregion

    #region Health
    [Header("Health")]
    public float maximumHealth = 100f;
    #endregion

    #region IStealthAIEntity properties
    public bool IsHiding { get; set; }
    #endregion

    #region IAIEntity properties
    public Vector3? MoveTarget { get; set; }

    public float EntityScanRange => playerScanRange;
    public float EntityHearingRange => hearingRange;

    public bool CanCommunicate => true;
    #endregion

    #region IEntity properties
    public float FieldOfView
    {
        get => fov;
        set => fov = value;
    }

    public float MoveSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public float CurrentHealth { get; set; }

    public float MaxHealth => maximumHealth;

    public bool IsDead => CurrentHealth <= 0f;

    public Vector3 Position => transform.position;

    public EntityType type => EntityType.Zombie;

    public float AttackRange => attackingRange;

    public float MinDamage => minimumDamage;

    public float MaxDamage => maximumDamage;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    public IEntity AttackTarget { get; set; }

    public Vector3[] PatrolPoints => defaultPatrolPoints;

    public bool IsPatrolling { get; set; }
    public bool IsPatrolPaused { get; set; }
    public int CurrentPatrolIndex { get; set; }
    #endregion

    #region Components
    private EnemyContext _enemyContext;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    #endregion

    #region Animations
    readonly int walkHash = Animator.StringToHash("walkTrigger");
    readonly int runStateHash = Animator.StringToHash("runTrigger");
    readonly int idleStateHash = Animator.StringToHash("idleTrigger");
    readonly int attackHash = Animator.StringToHash("attackTrigger");

    int currentStateHash;
    #endregion

    public IAIContext GetContext(Guid aiId)
    {
        return _enemyContext;
    }

    private void Awake()
    {
        _enemyContext = new EnemyContext(this);
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        currentStateHash = idleStateHash;

        defaultPatrolPoints = GameObject
            .FindGameObjectsWithTag(Tags.patrolPoint)
            .Select(p => p.transform.position)
            .ToArray();

        CurrentHealth = MaxHealth;

        EntityManager.instance.Register(gameObject, this);
    }

    private void Update()
    {
        if (IsDead) {
            gameObject.SetActive(false);
            return;
        }
    }


    private void OnDisable()
    {
        EntityManager.instance.Unregister(gameObject);
    }

    public void AttackTarget(IEntity target)
    {
        if (!PhysicsExtensions.ConeCastAll(
            transform.position + transform.forward,
            attackSphereCastRadius,
            transform.forward,
            out var _,
            AttackRange,
            60,
            LayersManager.instance.playerLayer)) {
            Debug.Log("Cone cast miss!");
            return;
        }

        if (target == null) {
            return;
        }

        if (currentStateHash != attackHash) {
            _animator.SetTrigger(attackHash);
            currentStateHash = attackHash;
        }

        var damage = UnityEngine.Random.Range(MinDamage, MaxDamage);
        target.currentHealth -= damage;
        Debug.Log("Dealt damage");
    }

    public void MoveTo(Vector3 destination)
    {
        _navMeshAgent.speed = movementSpeed;

        int animHash;
        switch (movementSpeed) {
            case var x when x >= 0f && x <= 2f:
                animHash = walkHash;
                break;
            case var x when x >= 2f && x <= 5f:
                animHash = runStateHash;
                break;
            default:
                animHash = idleStateHash;
                break;
        }

        if (animHash != currentStateHash) {
            _animator.SetTrigger(animHash);
            currentStateHash = animHash;
        }

        if ((destination - transform.position).sqrMagnitude < 1f) {
            return;
        }

        int mask = _navMeshAgent.areaMask;
        if (NavMesh.SamplePosition(destination, out var hit, 1f, mask)) {
            _navMeshAgent.SetDestination(hit.position);
        }
    }

    public void ReceiveCommunicatedMemory(IList<Observation> observations)
    {
        var count = observations.Count;
        for (int i = 0; i < count; i++) {
            if (ReferenceEquals(observations[i].entity, this)) {
                continue;
            }
            var newObs = new Observation(observations[i], false);
            _enemyContext.memory.AddOrUpdateObservation(newObs, true);
        }
    }
}