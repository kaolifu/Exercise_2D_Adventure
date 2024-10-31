using System;
using System.Collections.Generic;
using SaveAndLoad;
using UnityEngine;

public class DataManager : MonoBehaviour
{
  public static DataManager Instance;

  [Header("observer")] public VoidEventSO saveDataEvent;
  public VoidEventSO loadDataEvent;

  private List<ISavable> _savableList = new List<ISavable>();

  private Data _saveData;

  private void Awake()
  {
    if (Instance == null)
      Instance = this;
    else
      Destroy(gameObject);
  }

  private void OnEnable()
  {
    saveDataEvent.OnEventRaised += OnSaveDataEvent;
    loadDataEvent.OnEventRaised += OnLoadDataEvent;
  }

  private void OnDisable()
  {
    saveDataEvent.OnEventRaised -= OnSaveDataEvent;
    loadDataEvent.OnEventRaised -= OnLoadDataEvent;
  }

  public void RegisterSavable(ISavable savable)
  {
    _savableList.Add(savable);
  }

  public void UnregisterSavable(ISavable savable)
  {
    _savableList.Remove(savable);
  }

  private void OnSaveDataEvent()
  {
    _saveData = new Data();

    foreach (ISavable savableItem in _savableList)
    {
      savableItem.SaveData(_saveData);
    }

    foreach (var item in _saveData.CharacterPosDict)
    {
      Debug.Log(item.Key + " : " + item.Value);
    }

    _saveData.SaveToFile(Application.persistentDataPath + "/data.json");
  }

  private void OnLoadDataEvent()
  {
    _saveData = Data.LoadFromFile(Application.persistentDataPath + "/data.json");


    foreach (ISavable savableItem in _savableList)
    {
      savableItem.LoadData(_saveData);
    }
  }
}