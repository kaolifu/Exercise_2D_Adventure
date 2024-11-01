using System;
using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
  private PlayerInputControl _inputControl;
  private Rigidbody2D _rb;
  private Animator _anim;
  private Player _player;
  private Weapon _weapon;
  private Vector2 _inputDirection;

  [Header("Movement")] [SerializeField] private float speed;

  [Header("Attack")] public bool canAttack;
  public float attackTimer;

  public bool isPlayingAnimation;

  [Header("监听")] public VoidEventSO sceneLoadedEvent;

  [Header("广播")] public VoidEventSO pauseEvent;
  public PlayerEventSO infoEvent;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _anim = GetComponent<Animator>();
    _player = GetComponent<Player>();
    _weapon = GetComponentInChildren<Weapon>();

    _inputControl = new PlayerInputControl();
    _inputControl.Gameplay.Attack.started += Attack;
    _inputControl.Gameplay.Escape.started += OnEscPressed;
    _inputControl.Gameplay.Info.started += OnInfoPressed;
  }


  private void OnEnable()
  {
    sceneLoadedEvent.OnEventRaised += OnSceneLoad;
  }


  private void OnDisable()
  {
    sceneLoadedEvent.OnEventRaised -= OnSceneLoad;

    _inputControl.Disable();
  }

  private void Update()
  {
    _inputDirection = _inputControl.Gameplay.Move.ReadValue<Vector2>();
    Flip();

    TimeCount();
  }

  private void FixedUpdate()
  {
    Move();
  }

  private void TimeCount()
  {
    if (!canAttack)
    {
      attackTimer -= Time.deltaTime;
      if (attackTimer < 0)
      {
        canAttack = true;
      }
    }
  }

  private void Move()
  {
    if (_player.isHit || _player.isDead) return;
    _rb.velocity = new Vector2(_inputDirection.x * speed * Time.fixedDeltaTime,
      _inputDirection.y * speed * Time.fixedDeltaTime);

    _anim.SetBool("isRun", _inputDirection.magnitude > 0);
  }

  private void Flip()
  {
    if (_inputDirection.x < 0 && transform.localScale.x > 0 && !isPlayingAnimation)
    {
      isPlayingAnimation = true;
      _anim.SetTrigger("turn");
    }
    else if (_inputDirection.x > 0 && transform.localScale.x < 0 && !isPlayingAnimation)
    {
      isPlayingAnimation = true;
      _anim.SetTrigger("turn");
    }
  }

  private void Attack(InputAction.CallbackContext obj)
  {
    if (canAttack)
    {
      _weapon.Attack();

      canAttack = false;
      attackTimer = _weapon.attackCooldown;
    }
  }

  private void OnEscPressed(InputAction.CallbackContext obj)
  {
    pauseEvent.RaiseEvent();
  }

  private void OnInfoPressed(InputAction.CallbackContext obj)
  {
    infoEvent.RaiseEvent(_player);
  }


  private void OnSceneLoad()
  {
    _inputControl.Enable();
  }
}