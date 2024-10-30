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

  public UnityAction<SceneSO, Vector2, bool> OnEventRaised;

  public void RaiseEvent(SceneSO scene, Vector3 position, bool fadeInOut)
  {
    OnEventRaised?.Invoke(scene, position, fadeInOut);
  }
}