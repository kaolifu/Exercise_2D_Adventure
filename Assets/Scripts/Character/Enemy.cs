using System;
using Enum;
using UnityEngine;

public abstract class Enemy : Character
{
  public Vector2 Velocity
  {
    get => _rb.velocity;
    private set => _rb.velocity = value;
  }

  [Header("属性")] public int exp;

  [Header("Enemy Stats")] public float idleDuration;
  public float patrolSpeed;
  public float chaseSpeed;
  public float chaseDuration;
  public float chaseDistance;

  #region Components

  [HideInInspector] public ViewCheck viewCheck;
  [Header("Components")] public RectTransform canvasRectTransform;
  private Player _player;

  #endregion

  #region StateMachine

  protected BaseState currentState;
  protected BaseState idleState;
  protected BaseState patrolState;
  protected BaseState chaseState;

  #endregion

  protected override void Awake()
  {
    base.Awake();
    viewCheck = GetComponentInChildren<ViewCheck>();

    _player = FindObjectOfType<Player>();
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    currentState.OnEnter(this);
  }


  protected override void Update()
  {
    base.Update();
    currentState.OnLogicUpdate();
  }

  protected void FixedUpdate()
  {
    currentState.OnPhysicsUpdate();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    currentState.OnExit();
  }


  public void ChangeState(StateType type)
  {
    var newState = type switch
    {
      StateType.Idle => idleState,
      StateType.Patrol => patrolState,
      StateType.Chase => chaseState,
      _ => null
    };

    currentState.OnExit();
    currentState = newState;
    currentState?.OnEnter(this);
  }

  public void MoveTo(Vector2 destination, float speed)
  {
    if (isHit || isDead) return;

    Vector2 currentPosition = _rb.position; // 获取当前 Rigidbody2D 的位置
    Vector2 direction = (destination - currentPosition).normalized; // 计算方向并归一化

    // 移动方向与人物朝向不一致的，执行转向
    if (direction.x > 0 && transform.localScale.x < 0) Flip();
    else if (direction.x < 0 && transform.localScale.x > 0) Flip();

    // 通过速度向目标位置移动
    _rb.velocity = direction * speed;

    // 如果接近目标位置，则停止
    if (Vector2.Distance(currentPosition, destination) < 0.1f)
    {
      _rb.velocity = Vector2.zero; // 停止移动
    }
  }

  public void Flip()
  {
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    canvasRectTransform.localScale = new Vector3(-canvasRectTransform.localScale.x, canvasRectTransform.localScale.y,
      transform.localScale.z);
  }

  protected override void Die()
  {
    base.Die();
    Destroy(gameObject, 2.0f);

    _player.IncreaseExp(exp);
  }
}