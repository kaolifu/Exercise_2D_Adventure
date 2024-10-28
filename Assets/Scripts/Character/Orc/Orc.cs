public class Orc : Enemy
{
  protected override void Awake()
  {
    base.Awake();

    idleState = new OrcIdleState();
    patrolState = new OrcPatrolState();
    chaseState = new OrcChaseState();

    currentState = idleState;
  }

  protected override void Die()
  {
    base.Die();
    Destroy(gameObject, 2.0f);
  }
}