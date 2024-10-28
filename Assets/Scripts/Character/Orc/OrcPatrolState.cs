using Enum;
using UnityEngine;

public class OrcPatrolState : BaseState
{
  private float _boundary = 5.0f;

  private Vector2 _destination;

  private float timer;

  public override void OnEnter(Enemy enemy)
  {
    currentEnemy = enemy;

    currentEnemy.anim.SetBool("isRun", true);

    _destination = GetRandomPosition();

    timer = currentEnemy.patrolDuration;
  }

  public override void OnLogicUpdate()
  {
    timer -= Time.deltaTime;
    if (timer < 0)
      currentEnemy.ChangeState(StateType.Idle);


    if (currentEnemy.isHit)
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