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

  [Header("Movement")] [SerializeField] private float speed;

  private Vector2 _inputDirection;
  public bool isPlayingAnimation;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _anim = GetComponent<Animator>();

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
    PlayRunAnimation();
    Flip();
  }

  private void FixedUpdate()
  {
    Move();
  }

  private void Move()
  {
    _rb.velocity = new Vector2(_inputDirection.x * speed * Time.fixedDeltaTime,
      _inputDirection.y * speed * Time.fixedDeltaTime);

    _anim.SetBool("isRun", _inputDirection.magnitude > 0);
  }

  private void PlayRunAnimation()
  {
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
    _anim.SetTrigger("attack");
  }
}