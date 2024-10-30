using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [Header("监听")] public VoidEventSO pauseEvent;

  private void OnEnable()
  {
    pauseEvent.OnEventRaised += OnPauseEvent;
  }

  private void OnDisable()
  {
    pauseEvent.OnEventRaised -= OnPauseEvent;
  }

  private void OnPauseEvent()
  {
    if (Mathf.Approximately(Time.timeScale, 0)) Time.timeScale = 1;
    else if (Mathf.Approximately(Time.timeScale, 1)) Time.timeScale = 0;
  }
}