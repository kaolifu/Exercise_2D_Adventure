using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "NewScene", menuName = "SO/Scene", order = 1)]
public class SceneSO : ScriptableObject
{
  public AssetReference sceneReference;
}