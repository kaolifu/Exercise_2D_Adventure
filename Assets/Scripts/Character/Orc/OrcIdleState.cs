using System.Collections;
using Enum;
using UnityEngine;

public class OrcIdleState : BaseState
{
  private float _idleTimer;

  public override void OnEnter(Enemy enemy)
  {
    currentEnemy = enemy;

    currentEnemy.anim.SetBool("isIdle", true);

    _idleTimer = currentEnemy.idleDuration;
  }

  public override void OnLogicUpdate()
  {
    _idleTimer -= Time.deltaTime;
    if (_idleTimer < 0)
      currentEnemy.ChangeState(StateType.Patrol);

    if (currentEnemy.isHit)
      currentEnemy.ChangeState(StateType.Chase);
  }

  public override void OnPhysicsUpdate()
  {
  }

  public override void OnExit()
  {
    currentEnemy.anim.SetBool("isIdle", false);
  }
}