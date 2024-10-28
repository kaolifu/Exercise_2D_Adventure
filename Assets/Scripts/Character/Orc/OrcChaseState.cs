using Enum;
using UnityEngine;

public class OrcChaseState : BaseState
{
  private Transform _playerTrans;

  private float _timer;

  public override void OnEnter(Enemy enemy)
  {
    _playerTrans = GameObject.FindObjectOfType<Player>().transform;

    currentEnemy = enemy;
    currentEnemy.anim.SetBool("isRun", true);
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
  }
}