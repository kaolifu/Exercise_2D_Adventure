using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BossChaseState : BaseState
{
  private Transform _playerTrans;

  private Boss _boss;
  private Rigidbody2D _rb;

  private float _skillTimer;

  public override void OnEnter(Enemy enemy)
  {
    _playerTrans = GameObject.FindObjectOfType<Player>().transform;

    currentEnemy = enemy;
    currentEnemy.anim.SetBool("isRun", true);
    _rb = currentEnemy.GetComponent<Rigidbody2D>();

    _boss = (Boss)currentEnemy;
  }

  public override void OnLogicUpdate()
  {
    var distance = (currentEnemy.transform.position - _playerTrans.position).magnitude;

    if (distance < _boss.skillRange && _skillTimer <= 0)
    {
      _boss.Jump(_playerTrans.position);
      _skillTimer = _boss.skillCooldown;
    }

    _skillTimer -= Time.deltaTime;
  }

  public override void OnPhysicsUpdate()
  {
    currentEnemy.MoveTo(_playerTrans.position, currentEnemy.chaseSpeed);
  }

  public override void OnExit()
  {
    currentEnemy.anim.SetBool("isRun", false);
  }
}