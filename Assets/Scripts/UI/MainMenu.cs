using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
  private UIDocument _document;
  private VisualElement _gameOverScreen;
  private VisualElement _pauseScreen;

  private Button _restartButton;
  private Button _exitButton;

  [Header("监听")] public VoidEventSO deadEvent;
  public VoidEventSO pauseEvent;

  private void Awake()
  {
    _document = GetComponent<UIDocument>();

    _gameOverScreen = _document.rootVisualElement.Q<VisualElement>("GameOverScreen");
    _pauseScreen = _document.rootVisualElement.Q<VisualElement>("PauseScreen");

    _restartButton = _document.rootVisualElement.Q<Button>("RestartBtn");
    _restartButton.RegisterCallback<ClickEvent>(OnRestartButtonClicked);

    _exitButton = _document.rootVisualElement.Q<Button>("ExitBtn");
    _exitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);

    deadEvent.OnEventRaised += OnDeadEvent;
    pauseEvent.OnEventRaised += OnPauseEvent;
  }

  private void OnDisable()
  {
    _restartButton.UnregisterCallback<ClickEvent>(OnRestartButtonClicked);
    _exitButton.UnregisterCallback<ClickEvent>(OnExitButtonClicked);

    deadEvent.OnEventRaised -= OnDeadEvent;
    pauseEvent.OnEventRaised -= OnPauseEvent;
  }


  private void OnExitButtonClicked(ClickEvent evt)
  {
    // TODO:exit action
    Debug.Log("exit clicked");
  }

  private void OnRestartButtonClicked(ClickEvent evt)
  {
    // TODO:restart action
    Debug.Log("restart clicked");
  }

  private void OnDeadEvent()
  {
    _gameOverScreen.style.display = DisplayStyle.Flex;
  }

  private void OnPauseEvent()
  {
    _pauseScreen.style.display =
      _pauseScreen.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
  }
}