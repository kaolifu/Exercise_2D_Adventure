using UnityEngine;

public class Ghost : Enemy
{
  private SpriteRenderer _spriteRenderer;
  private float _alpha = 0.1f;
  private float _flickerSpeed = 0.1f;
  private bool _isIncreasing;
  public bool isChasing;

  protected override void Awake()
  {
    base.Awake();

    _spriteRenderer = GetComponent<SpriteRenderer>();

    idleState = new GhostIdleState();
    patrolState = new GhostPatrolState();
    chaseState = new GhostChaseState();

    currentState = idleState;
  }

  protected override void Update()
  {
    base.Update();
    Flicker();
  }



  private void Flicker()
  {
    if (isChasing)
    {
      _spriteRenderer.color = Color.white;
      return;
    }

    if (_isIncreasing)
    {
      _alpha += Time.deltaTime * _flickerSpeed;
      if (_alpha >= 0.3f)
      {
        _alpha = 0.3f;
        _isIncreasing = false;
      }
    }
    else
    {
      _alpha -= Time.deltaTime * _flickerSpeed;
      if (_alpha <= 0.0f)
      {
        _alpha = 0.0f;
        _isIncreasing = true;
      }
    }

    _spriteRenderer.color = new Color(1f, 1f, 1f, _alpha);
  }
}