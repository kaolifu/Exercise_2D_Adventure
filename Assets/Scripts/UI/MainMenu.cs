using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
  private UIDocument _document;
  private VisualElement _gameOverScreen;
  private VisualElement _pauseScreen;
  private VisualElement _fadeScreen;

  private GroupBox _pauseButtons;
  private GroupBox _gameOverButtons;

  private List<Button> _buttons;

  [Header("监听")] public VoidEventSO deadEvent;
  public VoidEventSO pauseEvent;
  public VoidEventSO fadeEvent;

  [Header("广播")] public VoidEventSO newGameEvent;
  public VoidEventSO backToMenuEvent;
  public VoidEventSO saveDataEvent;
  public VoidEventSO loadGameEvent;

  [Header("属性")] public float fadeDuration = 0.5f;

  private void Awake()
  {
    _document = GetComponent<UIDocument>();

    _gameOverScreen = _document.rootVisualElement.Q<VisualElement>("GameOverScreen");
    _pauseScreen = _document.rootVisualElement.Q<VisualElement>("PauseScreen");
    _fadeScreen = _document.rootVisualElement.Q<VisualElement>("FadeScreen");

    _buttons = _document.rootVisualElement.Query<Button>().ToList();

    deadEvent.OnEventRaised += OnDeadEvent;
    pauseEvent.OnEventRaised += OnPauseEvent;
    fadeEvent.OnEventRaised += OnFadeEvent;

    foreach (var button in _buttons)
    {
      button.RegisterCallback<ClickEvent>(OnButtonClick);
    }
  }

  private void OnDisable()
  {
    deadEvent.OnEventRaised -= OnDeadEvent;
    pauseEvent.OnEventRaised -= OnPauseEvent;
    fadeEvent.OnEventRaised -= OnFadeEvent;

    foreach (var button in _buttons)
    {
      button.UnregisterCallback<ClickEvent>(OnButtonClick);
    }
  }

  private void OnButtonClick(ClickEvent evt)
  {
    var currentBtn = evt.target as Button;
    switch (currentBtn.name)
    {
      case "SaveBtn":
        saveDataEvent.RaiseEvent();
        break;
      case "LoadBtn":
        loadGameEvent.RaiseEvent();
        break;
      case "RestartBtn":
        NewGame();
        break;
      case "ExitBtn":
        ExitGame();
        break;
    }
  }

  private void NewGame()
  {
    newGameEvent.RaiseEvent();
    _gameOverScreen.style.display = DisplayStyle.None;
  }

  private void ExitGame()
  {
    backToMenuEvent.RaiseEvent();
    _gameOverScreen.style.display = DisplayStyle.None;
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