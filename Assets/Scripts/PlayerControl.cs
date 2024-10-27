using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
  private PlayerInputControl _inputControl;
  private Rigidbody2D _rb;

  [Header("Movement")] [SerializeField] private float speed;
  private Vector2 _inputDirection;

  private void Awake()
  {
    _inputControl = new PlayerInputControl();
    _rb = GetComponent<Rigidbody2D>();
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
  }

  private void FixedUpdate()
  {
    Move();
  }

  private void Move()
  {
    _rb.velocity = new Vector2(_inputDirection.x * speed * Time.fixedDeltaTime,
      _inputDirection.y * speed * Time.fixedDeltaTime);
  }
}