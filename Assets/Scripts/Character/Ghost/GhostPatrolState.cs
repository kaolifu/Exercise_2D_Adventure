using Enum;
using UnityEngine;

public class GhostPatrolState : BaseState
{
  private float _boundary = 5.0f;

  private Vector2 _destination;

  private float _patrolTimer;
  private float _buggedTimer = 0.5f;

  public override void OnEnter(Enemy enemy)
  {
    currentEnemy = enemy;

    currentEnemy.anim.SetBool("isRun", true);

    _destination = GetRandomPosition();
  }

  public override void OnLogicUpdate()
  {
    // 当角色速度的绝对值小于0.1，则启动被卡住的timer，timer到时则重新进入patrol state
    if (currentEnemy.Velocity.magnitude < 0.1)
    {
      _buggedTimer -= Time.deltaTime;
      if (_buggedTimer < 0)
      {
        currentEnemy.ChangeState(StateType.Patrol);
      }
    }

    // 当角色与目标地点的距离绝对值小于0.1，则意味着到达目的，切换到idle state
    if ((_destination - (Vector2)currentEnemy.transform.position).magnitude < 0.1)
      currentEnemy.ChangeState(StateType.Idle);

    // 当角色被攻击或者视线检查中发现玩家，则切换到chase state
    if (currentEnemy.isHit || currentEnemy.viewCheck.hasFoundPlayer)
      currentEnemy.ChangeState(StateType.Chase);
  }

  public override void OnPhysicsUpdate()
  {
    if (_destination != Vector2.zero)
    {
      currentEnemy.MoveTo(_destination, currentEnemy.patrolSpeed);

      Debug.DrawLine(currentEnemy.transform.position, _destination, Color.red);
    }
  }

  public override void OnExit()
  {
    currentEnemy.anim.SetBool("isRun", false);
  }

  private Vector2 GetRandomPosition()
  {
    var random = Random.insideUnitCircle;
    var randomPos = new Vector2(random.x * _boundary, random.y * _boundary) + (Vector2)currentEnemy.transform.position;
    return randomPos;
  }
}