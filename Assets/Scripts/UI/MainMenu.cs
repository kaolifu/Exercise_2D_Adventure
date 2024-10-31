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
  private VisualElement _infoScreen;

  private GroupBox _pauseButtons;
  private GroupBox _gameOverButtons;

  private List<Button> _buttons;

  [Header("监听")] public VoidEventSO deadEvent;
  public VoidEventSO pauseEvent;
  public VoidEventSO fadeEvent;
  public PlayerEventSO infoEvent;

  [Header("广播")] public VoidEventSO newGameEvent;
  public VoidEventSO backToMenuEvent;
  public VoidEventSO saveDataEvent;
  public VoidEventSO loadGameEvent;
  public VoidEventSO fadeCompleteEvent;

  [Header("属性")] public float fadeDuration = 0.5f;
  private bool _isFading;
  [SerializeField] private float _fadeTimer;

  private void Awake()
  {
    _document = GetComponent<UIDocument>();

    _gameOverScreen = _document.rootVisualElement.Q<VisualElement>("GameOverScreen");
    _pauseScreen = _document.rootVisualElement.Q<VisualElement>("PauseScreen");
    _fadeScreen = _document.rootVisualElement.Q<VisualElement>("FadeScreen");
    _infoScreen = _document.rootVisualElement.Q<VisualElement>("InfoScreen");

    _buttons = _document.rootVisualElement.Query<Button>().ToList();

    deadEvent.OnEventRaised += OnDeadEvent;
    pauseEvent.OnEventRaised += OnPauseEvent;
    fadeEvent.OnEventRaised += OnFadeEvent;
    infoEvent.OnEventRaised += OnInfoEvent;

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
    infoEvent.OnEventRaised -= OnInfoEvent;


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
        SaveGame();
        break;
      case "LoadBtn":
        LoadGame();
        break;
      case "RestartBtn":
        NewGame();
        break;
      case "ExitBtn":
        ExitGame();
        break;
    }
  }

  private void SaveGame()
  {
    saveDataEvent.RaiseEvent();
    _pauseScreen.style.display = DisplayStyle.None;
  }

  private void LoadGame()
  {
    loadGameEvent.RaiseEvent();
    _pauseScreen.style.display = DisplayStyle.None;
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

  private void OnInfoEvent(Player player)
  {
    Debug.Log(_infoScreen.style.display.value);


    if (_infoScreen.style.display.value == DisplayStyle.None)
    {
      _infoScreen.style.display = DisplayStyle.Flex;

      GameManager.StopGame();

      _infoScreen.Q("hp").Q<Label>("content").text = player.health + "/" + player.maxHealth;
      _infoScreen.Q("lv").Q<Label>("content").text = player.Level + "";
      _infoScreen.Q("exp").Q<Label>("content").text = player.CurrentExp + "/" + player.NeededExp;
    }
    else
    {
      _infoScreen.style.display = DisplayStyle.None;

      GameManager.StartGame();
    }
  }


  private void Update()
  {
    // if (_isFading)
    // {
    //   _fadeScreen.style.display = DisplayStyle.Flex;
    //
    //   _fadeTimer += Time.deltaTime;
    //   _fadeScreen.style.opacity = _fadeTimer / fadeDuration;
    //
    //   if (_fadeTimer >= fadeDuration)
    //   {
    //     _isFading = false;
    //     _fadeScreen.style.display = DisplayStyle.None;
    //     _fadeTimer = 0;
    //   }
    // }
  }


  // TODO: 有bug
  private void OnFadeEvent()
  {
    StartCoroutine(Fade());
  }

  private IEnumerator Fade()
  {
    _fadeScreen.style.display = DisplayStyle.Flex;

    _fadeTimer = 0;
    while (_fadeTimer < fadeDuration)
    {
      _fadeTimer += Time.deltaTime;
      _fadeScreen.style.opacity = _fadeTimer / fadeDuration * 100;
      yield return null;
    }

    fadeCompleteEvent.RaiseEvent();

    _fadeScreen.style.display = DisplayStyle.None;
  }
}