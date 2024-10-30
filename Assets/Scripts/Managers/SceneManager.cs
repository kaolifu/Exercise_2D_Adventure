using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
  [Header("监听")] public SceneLoadEventSO sceneLoadEvent;

  [Header("广播")] public VoidEventSO fadeEvent;
  public VoidEventSO sceneLoadedEvent;

  [Header("属性")] public SceneSO firstScene;
  public Vector3 firstScenePosition;
  private SceneSO _currentScene;

  private GameObject _player;

  private void Awake()
  {
    sceneLoadEvent.RaiseEvent += OnSceneLoadEvent;

    _player = GameObject.FindGameObjectWithTag("Player");

    OnSceneLoadEvent(firstScene, firstScenePosition, true);
  }

  private void OnDisable()
  {
    sceneLoadEvent.RaiseEvent += OnSceneLoadEvent;
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

    sceneLoadedEvent.RaiseEvent();
    if (fade)
    {
      // 淡出
      // fadeEvent.RaiseEvent();
    }
  }
}