using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
  [SerializeField] private float damage;
  [SerializeField] private float impactForce;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.GetComponent<Character>() != null)
    {
      Vector2 direction = (other.transform.position - transform.position).normalized;

      other.GetComponent<Character>().TakeDamage(damage, direction, impactForce);
    }
  }
}