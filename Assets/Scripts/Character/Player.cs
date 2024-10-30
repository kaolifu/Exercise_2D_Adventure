using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
  [Header("广播")] public VoidEventSO deadEvent;

  protected override void Die()
  {
    base.Die();
    deadEvent.RaiseEvent();
  }
}