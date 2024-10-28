using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
  public Enemy currentEnemy;

  public abstract void OnEnter(Enemy enemy);
  public abstract void OnLogicUpdate();
  public abstract void OnPhysicsUpdate();
  public abstract void OnExit();
}