using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [Header("监听")] public VoidEventSO pauseEvent;
  public VoidEventSO playerDeadEvent;
  public VoidEventSO newGameEvent;

  private void OnEnable()
  {
    pauseEvent.OnEventRaised += OnPauseEvent;
    playerDeadEvent.OnEventRaised += StopGame;
    newGameEvent.OnEventRaised += StartGame;
  }

  private void OnDisable()
  {
    pauseEvent.OnEventRaised -= OnPauseEvent;
    playerDeadEvent.OnEventRaised -= StopGame;
    newGameEvent.OnEventRaised -= StartGame;
  }

  private void OnPauseEvent()
  {
    if (Mathf.Approximately(Time.timeScale, 0)) Time.timeScale = 1;
    else if (Mathf.Approximately(Time.timeScale, 1)) Time.timeScale = 0;
  }

  private void StopGame()
  {
    Time.timeScale = 0;
  }

  private void StartGame()
  {
    Time.timeScale = 1;
  }
}