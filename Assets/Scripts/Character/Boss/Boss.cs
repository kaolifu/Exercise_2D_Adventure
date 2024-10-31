using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Boss : Enemy
{
  public float skillRange;
  public float skillCooldown;
  public float jumpPower;
  public float jumpDuration = 0.5f;

  public ParticleSystem jumpEffect;

  protected override void Awake()
  {
    base.Awake();

    idleState = new OrcIdleState();
    patrolState = new OrcPatrolState();
    chaseState = new BossChaseState();

    currentState = idleState;
  }
  
  public void Jump(Vector3 targetPos)
  {
    StartCoroutine(JumpSkill(targetPos));
  }

  private IEnumerator JumpSkill(Vector3 targetPos)
  {
    Instantiate(jumpEffect, transform.position, jumpEffect.transform.rotation);
    _rb.DOJump(targetPos, jumpPower, 1, jumpDuration);
    yield return new WaitForSeconds(jumpDuration);
    Instantiate(jumpEffect, transform.position, jumpEffect.transform.rotation);
  }
}