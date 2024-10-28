using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _anim = GetComponent<Animator>();
    _player = GetComponent<Player>();
    _weapon = GetComponentInChildren<Weapon>();

    _inputControl = new PlayerInputControl();
    _inputControl.Gameplay.Attack.started += Attack;
  }


  private void OnEnable()
  {
    _inputControl.Enable();
  }

  private void OnDisable()
  {
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
}