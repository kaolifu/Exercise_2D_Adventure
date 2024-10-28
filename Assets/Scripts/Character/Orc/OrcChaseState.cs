using UnityEngine;

public class OrcChaseState : BaseState
{
  private Transform playerTrans;

  public override void OnEnter(Enemy enemy)
  {
    playerTrans = GameObject.FindObjectOfType<Player>().transform;

    currentEnemy = enemy;
    currentEnemy.anim.SetBool("isRun", true);
  }

  public override void OnLogicUpdate()
  {
  }

  public override void OnPhysicsUpdate()
  {
    currentEnemy.MoveTo(playerTrans.position, currentEnemy.chaseSpeed);
  }

  public override void OnExit()
  {
    currentEnemy.anim.SetBool("isRun", false);
  }
}