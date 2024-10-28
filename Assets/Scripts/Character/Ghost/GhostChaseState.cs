using Enum;
using UnityEngine;

public class GhostChaseState : BaseState
{
  private Transform _playerTrans;

  private float _timer;

  private Ghost _ghost;

  public override void OnEnter(Enemy enemy)
  {
    _playerTrans = GameObject.FindObjectOfType<Player>().transform;

    currentEnemy = enemy;
    currentEnemy.anim.SetBool("isRun", true);

    _ghost = (Ghost)currentEnemy;
    _ghost.isChasing = true;
  }

  public override void OnLogicUpdate()
  {
    if ((_playerTrans.position - currentEnemy.transform.position).magnitude > currentEnemy.chaseDistance)
    {
      _timer -= Time.deltaTime;
      if (_timer < 0)
      {
        currentEnemy.ChangeState(StateType.Idle);
      }
    }
    else
    {
      _timer = currentEnemy.chaseDuration;
    }
  }

  public override void OnPhysicsUpdate()
  {
    currentEnemy.MoveTo(_playerTrans.position, currentEnemy.chaseSpeed);
  }

  public override void OnExit()
  {
    currentEnemy.anim.SetBool("isRun", false);

    _ghost.isChasing = false;
  }
}