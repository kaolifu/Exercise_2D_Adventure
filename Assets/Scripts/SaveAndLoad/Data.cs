using System.Collections.Generic;
using UnityEngine;

public class Data
{
  public Dictionary<string, Vector3> CharacterPosDict = new Dictionary<string, Vector3>();
  public Dictionary<string, float> FloatDataDict = new Dictionary<string, float>();

  private string _sceneToSave;

  public void SaveGameScene(SceneSO scene)
  {
    _sceneToSave = JsonUtility.ToJson(scene);
  }

  public SceneSO LoadGameScene()
  {
    var newScene = ScriptableObject.CreateInstance<SceneSO>();
    JsonUtility.FromJsonOverwrite(_sceneToSave, newScene);
    return newScene;
  }
}