public class Orc : Character
{
  protected override void Die()
  {
    base.Die();
    Destroy(gameObject, 2.0f);
  }
}