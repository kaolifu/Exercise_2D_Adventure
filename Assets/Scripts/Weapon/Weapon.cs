using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  private Animator _anim;
  public float attackCooldown;

  private void Awake()
  {
    _anim = GetComponent<Animator>();
  }

  public void Attack()
  {
    _anim.SetTrigger("attack");
  }
}