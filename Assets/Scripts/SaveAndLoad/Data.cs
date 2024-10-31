using System.Collections.Generic;
using System.IO;
using SaveAndLoad;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Serialization;

[System.Serializable]
public class Data
{
  public Dictionary<string, SerializableVector3> CharacterPosDict = new Dictionary<string, SerializableVector3>();
  public Dictionary<string, float> FloatDataDict = new Dictionary<string, float>();

  public string sceneToSave;

  public void SaveGameScene(SceneSO scene)
  {
    sceneToSave = JsonUtility.ToJson(scene);
  }

  public SceneSO LoadGameScene()
  {
    var newScene = ScriptableObject.CreateInstance<SceneSO>();
    JsonUtility.FromJsonOverwrite(sceneToSave, newScene);
    return newScene;
  }

  public void SaveToFile(string filePath)
  {
    // string json = JsonUtility.ToJson(this, true);
    string json = JsonConvert.SerializeObject(this, Formatting.Indented);
    Debug.Log(json);
    File.WriteAllText(filePath, json);
  }

  public static Data LoadFromFile(string filePath)
  {
    if (File.Exists(filePath))
    {
      string json = File.ReadAllText(filePath);
      // return JsonUtility.FromJson<Data>(json);
      return JsonConvert.DeserializeObject<Data>(json);
    }

    return new Data();
  }
}