using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "NewScene", menuName = "SO/Scene", order = 1)]
public class SceneSO : ScriptableObject
{
  public SceneType sceneType;
  public AssetReference sceneReference;
}