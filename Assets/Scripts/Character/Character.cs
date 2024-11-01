using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour
{
  [Header("Attribution")] [SerializeField]
  public float health;

  [SerializeField] public float maxHealth;

  [Header("Stats")] public bool isHit;
  public bool isDead;
  [SerializeField] private bool isInvulnerable;
  [SerializeField] private float invulnerabilityTime;
  private float invulnerabilityTimer;

  #region Components

  private HealthBar healthBar;
  [HideInInspector] public Animator anim;
  protected Rigidbody2D _rb;

  #endregion

  [Header("Events")] public UnityEvent onTakeDamage;

  [Header("监听")] public VoidEventSO newGameEvent;


  protected virtual void Awake()
  {
    anim = GetComponent<Animator>();
    _rb = GetComponent<Rigidbody2D>();
    healthBar = GetComponentInChildren<HealthBar>();

    isDead = false;
    MaximizeHealth();
    UpdateHealthBar();
  }

  protected virtual void OnEnable()
  {
    newGameEvent.OnEventRaised += OnNewGameEvent;
  }

  protected virtual void OnDisable()
  {
    newGameEvent.OnEventRaised -= OnNewGameEvent;
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

  private void MaximizeHealth()
  {
    health = maxHealth;
  }


  protected void UpdateHealthBar()
  {
    var percentage = health / maxHealth;
    healthBar.SetHealthFill(percentage);
  }

  public void TakeDamage(float damage, Vector2 direction, float impactForce)
  {
    // 设置无敌CD
    if (isInvulnerable) return;

    onTakeDamage?.Invoke();

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

  private void OnNewGameEvent()
  {
    isDead = false;
    MaximizeHealth();
    UpdateHealthBar();
  }
}