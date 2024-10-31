using System;
using System.Collections;
using System.Collections.Generic;
using SaveAndLoad;
using UnityEngine;

public class Player : Character, ISavable
{
  [Header("广播")] public VoidEventSO deadEvent;

  [Header("监听")] public VoidEventSO loadGameEvent;

  private int _level;
  private int _currentExp;

  public int Level
  {
    get
    {
      if (_level == 0) _level = 1;
      return _level;
    }
    private set => _level = value;
  }

  public int NeededExp => _level * 100;

  public int CurrentExp
  {
    get => _currentExp;
    private set => _currentExp = value;
  }

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
      data.FloatDataDict[GetDataID().ID + "maxHealth"] = maxHealth;
      data.FloatDataDict[GetDataID().ID + "level"] = Level;
      data.FloatDataDict[GetDataID().ID + "exp"] = CurrentExp;
    }
    else
    {
      data.CharacterPosDict.Add(GetDataID().ID, serializableVector3);
      data.FloatDataDict.Add(GetDataID().ID + "health", health);
      data.FloatDataDict.Add(GetDataID().ID + "maxHealth", maxHealth);
      data.FloatDataDict.Add(GetDataID().ID + "level", Level);
      data.FloatDataDict.Add(GetDataID().ID + "exp", CurrentExp);
    }
  }

  public void LoadData(Data data)
  {
    if (data.CharacterPosDict.ContainsKey(GetDataID().ID))
    {
      transform.position = data.CharacterPosDict[GetDataID().ID].ToVector3();
      health = data.FloatDataDict[GetDataID().ID + "health"];
      maxHealth = data.FloatDataDict[GetDataID().ID + "maxHealth"];
      _level = (int)data.FloatDataDict[GetDataID().ID + "level"];
      _currentExp = (int)data.FloatDataDict[GetDataID().ID + "exp"];

      UpdateHealthBar();
    }
  }

  private void OnGameLoadedEvent()
  {
    isDead = false;
    UpdateHealthBar();
  }

  public void IncreaseExp(int amount)
  {
    CurrentExp += amount;

    if (CurrentExp >= NeededExp)
    {
      _currentExp -= NeededExp;
      _level += 1;
    }
  }
}