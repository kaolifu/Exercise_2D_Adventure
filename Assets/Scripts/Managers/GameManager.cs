using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;


  [Header("监听")] public VoidEventSO pauseEvent;
  public VoidEventSO playerDeadEvent;
  public VoidEventSO newGameEvent;
  public VoidEventSO backToMenuEvent;
  public VoidEventSO saveGameEvent;
  public VoidEventSO loadGameEvent;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void OnEnable()
  {
    pauseEvent.OnEventRaised += OnPauseEvent;
    playerDeadEvent.OnEventRaised += StopGame;
    newGameEvent.OnEventRaised += StartGame;
    backToMenuEvent.OnEventRaised += StartGame;
    saveGameEvent.OnEventRaised += StartGame;
    loadGameEvent.OnEventRaised += StartGame;
  }

  private void OnDisable()
  {
    pauseEvent.OnEventRaised -= OnPauseEvent;
    playerDeadEvent.OnEventRaised -= StopGame;
    newGameEvent.OnEventRaised -= StartGame;
    backToMenuEvent.OnEventRaised -= StartGame;
    saveGameEvent.OnEventRaised -= StartGame;
    loadGameEvent.OnEventRaised -= StartGame;
  }

  private void OnPauseEvent()
  {
    if (Mathf.Approximately(Time.timeScale, 0)) Time.timeScale = 1;
    else if (Mathf.Approximately(Time.timeScale, 1)) Time.timeScale = 0;
  }

  public static void StopGame()
  {
    Time.timeScale = 0;
  }

  public static void StartGame()
  {
    Time.timeScale = 1;
  }
}