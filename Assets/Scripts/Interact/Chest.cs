using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInterract
{
  public Sprite openSprite;
  public Sprite closedSprite;
  private SpriteRenderer _spriteRenderer;
  public bool isDone;

  private void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void OnEnable()
  {
    if (isDone)
    {
      _spriteRenderer.sprite = openSprite;
      transform.tag = "Untagged";
    }
    else
    {
      _spriteRenderer.sprite = closedSprite;
    }
  }


  private void OpenChest()
  {
    _spriteRenderer.sprite = openSprite;
    isDone = true;
    transform.tag = "Untagged";
  }

  public void Interact()
  {
    OpenChest();
  }
}