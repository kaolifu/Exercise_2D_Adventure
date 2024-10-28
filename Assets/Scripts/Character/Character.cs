using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour
{
  [Header("Attribution")] [SerializeField]
  private float health;

  [SerializeField] private float maxHealth;

  [Header("Stats")] public bool isHit;
  public bool isDead;
  [SerializeField] private float invulnerabilityTime;

  private bool isInvulnerable;
  private float invulnerabilityTimer;

  #region Components

  private HealthBar healthBar;
  [HideInInspector] public Animator anim;
  protected Rigidbody2D _rb;

  #endregion


  protected virtual void Awake()
  {
    anim = GetComponent<Animator>();
    _rb = GetComponent<Rigidbody2D>();
    healthBar = GetComponentInChildren<HealthBar>();

    InitHealth();
  }


  protected virtual void Update()
  {
    TimeCount();
  }


  private void TimeCount()
  {
    if (isInvulnerable)
    {
      invulnerabilityTimer -= Time.deltaTime;
      if (invulnerabilityTimer <= 0)
      {
        isInvulnerable = false;
        isHit = false;
        invulnerabilityTimer = invulnerabilityTime;
      }
    }
  }

  private void InitHealth()
  {
    health = maxHealth;
    healthBar.SetHealthFill(1);
  }

  public void TakeDamage(float damage, Vector2 direction, float impactForce)
  {
    // 设置无敌CD
    if (isInvulnerable) return;

    isInvulnerable = true;
    invulnerabilityTimer = invulnerabilityTime;

    // 计算伤害，更新HP Bar
    health -= damage;
    var healthPercent = health / maxHealth;
    healthBar.SetHealthFill(healthPercent);

    // 播放hit动画
    anim.SetTrigger("hit");

    // 设置isHit,以中断其他正在执行的一些动作
    isHit = true;

    // 受到 direction 方向，impactForce 大小的冲击力
    _rb.AddForce(direction * impactForce, ForceMode2D.Impulse);

    // if 死亡
    if (health <= 0)
      Die();
  }

  protected virtual void Die()
  {
    isDead = true;

    anim.SetBool("isDead", true);
  }
}