using System;
using System.Collections;
using System.Collections.Generic;
using SaveAndLoad;
using UnityEngine;

public class Player : Character, ISavable
{
  [Header("广播")] public VoidEventSO deadEvent;

  [Header("监听")] public VoidEventSO loadGameEvent;
  
  [Header("属性")] public int level = 1;
  public int currentExp;
  public int needExp;
  


  protected override void OnEnable()
  {
    base.OnEnable();
    ISavable savable = this;
    savable.RegisterSaveData();

    loadGameEvent.OnEventRaised += OnGameLoadedEvent;
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    ISavable savable = this;
    savable.UnregisterSaveData();

    loadGameEvent.OnEventRaised -= OnGameLoadedEvent;
  }


  protected override void Die()
  {
    base.Die();
    StartCoroutine(DeadDelay());
  }

  private IEnumerator DeadDelay()
  {
    yield return new WaitForSeconds(0.5f);
    deadEvent.RaiseEvent();
  }

  private DataDefinition GetDataID()
  {
    return GetComponent<DataDefinition>();
  }

  public void SaveData(Data data)
  {
    var serializableVector3 = new SerializableVector3(transform.position);
    if (data.CharacterPosDict.ContainsKey(GetDataID().ID))
    {
      data.CharacterPosDict[GetDataID().ID] = serializableVector3;
      data.FloatDataDict[GetDataID().ID + "health"] = health;
    }
    else
    {
      data.CharacterPosDict.Add(GetDataID().ID, serializableVector3);
      data.FloatDataDict.Add(GetDataID().ID + "health", health);
    }
  }

  public void LoadData(Data data)
  {
    if (data.CharacterPosDict.ContainsKey(GetDataID().ID))
    {
      transform.position = data.CharacterPosDict[GetDataID().ID].ToVector3();
      health = data.FloatDataDict[GetDataID().ID + "health"];

      UpdateHealthBar();
    }
  }

  private void OnGameLoadedEvent()
  {
    isDead = false;
    InitHealth();
  }
}