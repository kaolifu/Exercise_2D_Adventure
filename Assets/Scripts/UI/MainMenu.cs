using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
  private UIDocument _document;
  private VisualElement _gameOverScreen;
  private VisualElement _pauseScreen;
  private VisualElement _fadeScreen;

  private Button _restartButton;
  private Button _exitButton;

  [Header("监听")] public VoidEventSO deadEvent;
  public VoidEventSO pauseEvent;
  public VoidEventSO fadeEvent;

  [Header("属性")] public float fadeDuration = 0.5f;

  private void Awake()
  {
    _document = GetComponent<UIDocument>();

    _gameOverScreen = _document.rootVisualElement.Q<VisualElement>("GameOverScreen");
    _pauseScreen = _document.rootVisualElement.Q<VisualElement>("PauseScreen");
    _fadeScreen = _document.rootVisualElement.Q<VisualElement>("FadeScreen");

    _restartButton = _document.rootVisualElement.Q<Button>("RestartBtn");
    _restartButton.RegisterCallback<ClickEvent>(OnRestartButtonClicked);

    _exitButton = _document.rootVisualElement.Q<Button>("ExitBtn");
    _exitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);

    deadEvent.OnEventRaised += OnDeadEvent;
    pauseEvent.OnEventRaised += OnPauseEvent;
    fadeEvent.OnEventRaised += OnFadeEvent;
  }

  private void OnDisable()
  {
    _restartButton.UnregisterCallback<ClickEvent>(OnRestartButtonClicked);
    _exitButton.UnregisterCallback<ClickEvent>(OnExitButtonClicked);

    deadEvent.OnEventRaised -= OnDeadEvent;
    pauseEvent.OnEventRaised -= OnPauseEvent;
    fadeEvent.OnEventRaised -= OnFadeEvent;
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

  
  // TODO: 有bug
  private void OnFadeEvent()
  {
    if (_fadeScreen.style.display == DisplayStyle.None)
    {
      Debug.Log("start fade in");
      _fadeScreen.style.display = DisplayStyle.Flex;

      DOTween.To(() => _fadeScreen.style.opacity.value,
        x => _fadeScreen.style.opacity = x,
        1f,
        fadeDuration);
      Debug.Log("over fade in");

    }
    else
    {
      Debug.Log("start fade out");

      DOTween.To(() => _fadeScreen.style.opacity.value,
        x => _fadeScreen.style.opacity = x,
        0f,
        fadeDuration);
      _fadeScreen.style.display = DisplayStyle.None;
      
      Debug.Log("over fade out");

    }
  }
}