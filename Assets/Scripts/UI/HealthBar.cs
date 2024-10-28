using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private Image healthFill;
  [SerializeField] private Image healthFrame;

  public void SetHealthFill(float percentage)
  {
    if (percentage >= 1f)
    {
      percentage = 1f;
      healthFill.gameObject.SetActive(false);
      healthFrame.gameObject.SetActive(false);
    }
    else
    {
      healthFill.gameObject.SetActive(true);
      healthFrame.gameObject.SetActive(true);
    }

    healthFill.fillAmount = percentage;
  }
}