using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneLoadEvent", menuName = "SO/Event/Scene Load Event")]
public class SceneLoadEventSO : ScriptableObject
{
  public SceneSO sceneToLoad;
  public Vector3 positionToGo;
  public bool fade;

  public UnityAction<SceneSO, Vector2, bool> RaiseEvent;

  public void OnEventRaised(SceneSO scene, Vector3 position, bool fadeInOut)
  {
    RaiseEvent?.Invoke(scene, position, fadeInOut);
  }
}