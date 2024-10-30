using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, IInterract
{
  [Header("广播")] public SceneLoadEventSO sceneLoadEvent;

  [Header("属性")] public SceneSO sceneToLoad;
  public Vector3 positionToGo;
  public bool fade;

  public void Interact()
  {
    sceneLoadEvent.OnEventRaised(sceneToLoad, positionToGo, fade);
  }
}