using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartMenu : MonoBehaviour
{
  private UIDocument _document;
  private VisualElement _clouds;
  private VisualElement _buttons;

  public VoidEventSO newGameEvent;

  public float moveSpeed;

  private void Awake()
  {
    _document = GetComponent<UIDocument>();
    _clouds = _document.rootVisualElement.Q<VisualElement>("clouds");
    _buttons = _document.rootVisualElement.Q<VisualElement>("buttons");

    for (int i = 0; i < _buttons.childCount; i++)
    {
      _buttons[i].RegisterCallback<ClickEvent>(OnButtonClicked);
    }
  }

  private void OnDisable()
  {
    for (int i = 0; i < _buttons.childCount; i++)
    {
      _buttons[i].UnregisterCallback<ClickEvent>(OnButtonClicked);
    }
  }


  private void Update()
  {
    for (var i = 0; i < _clouds.childCount; i++)
    {
      var currentX = _clouds[i].style.translate.value.x.value;

      var newX = currentX - moveSpeed * (i + 1) * Time.deltaTime;

      if (newX < -_clouds[i].resolvedStyle.width)
        newX = 0;

      _clouds[i].style.translate = new Translate(new Length(newX), new Length(0));
    }
  }

  private void OnButtonClicked(ClickEvent evt)
  {
    var clickedButton = evt.target as VisualElement;

    switch (clickedButton.name)
    {
      case "StartBtn":
        newGameEvent.RaiseEvent();
        break;
      // TODO:读取存档
      case "ContinueBtn":
        Debug.Log("ContinueBtn");
        break;
      case "ExitBtn":
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        break;
    }
  }
}