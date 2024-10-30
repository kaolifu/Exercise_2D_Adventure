using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
  [Header("广播")] public VoidEventSO deadEvent;




  protected override void Die()
  {
    base.Die();
    StartCoroutine(DeadDelay());
  }

  private IEnumerator DeadDelay()
  {
    yield return new WaitForSeconds(0.5f);
    deadEvent.RaiseEvent();
  }

}