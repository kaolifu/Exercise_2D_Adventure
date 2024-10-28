using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCheck : MonoBehaviour
{
  public bool hasFoundPlayer;

  private void OnTriggerEnter2D(Collider2D other)
  {
    hasFoundPlayer = other.CompareTag("Player");
  }
}