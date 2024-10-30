using System;
using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
  [Header("监听")] public SceneLoadEventSO sceneLoadEvent;
  public VoidEventSO newGameEvent;

  [Header("广播")] public VoidEventSO fadeEvent;
  public VoidEventSO sceneLoadedEvent;

  [Header("属性")] public SceneSO menuSO;
  public SceneSO firstScene;
  public Vector3 firstScenePosition;
  private SceneSO _currentScene;

  private GameObject _player;

  private void Awake()
  {
    sceneLoadEvent.OnEventRaised += OnSceneLoadEvent;
    newGameEvent.OnEventRaised += OnNewGameEvent;


    _player = GameObject.FindGameObjectWithTag("Player");

    OnSceneLoadEvent(menuSO, firstScenePosition, true);
  }


  private void OnDisable()
  {
    sceneLoadEvent.OnEventRaised -= OnSceneLoadEvent;
    newGameEvent.OnEventRaised -= OnNewGameEvent;
  }

  private void OnNewGameEvent()
  {
    OnSceneLoadEvent(firstScene, firstScenePosition, true);
  }

  private void OnSceneLoadEvent(SceneSO sceneToLoad, Vector2 positionToGo, bool fade)
  {
    StartCoroutine(SceneLoad(sceneToLoad, positionToGo, fade));
  }

  private IEnumerator SceneLoad(SceneSO sceneToLoad, Vector2 positionToGo, bool fade)
  {
    yield return UnLoadScene(fade);
    yield return LoadScene(sceneToLoad, positionToGo, fade);
  }

  private IEnumerator UnLoadScene(bool fade)
  {
    if (fade)
    {
      // 淡入
      // fadeEvent.RaiseEvent();
    }

    if (_currentScene != null)
    {
      yield return _currentScene.sceneReference.UnLoadScene();
    }

    _player.SetActive(false);
  }

  private IEnumerator LoadScene(SceneSO sceneToLoad, Vector2 positionToGo, bool fade)
  {
    _player.SetActive(true);
    _player.transform.position = positionToGo;

    yield return sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    _currentScene = sceneToLoad;

    if (sceneToLoad.sceneType != SceneType.Menu)
      sceneLoadedEvent.RaiseEvent();


    if (fade)
    {
      // 淡出
      // fadeEvent.RaiseEvent();
    }
  }
}