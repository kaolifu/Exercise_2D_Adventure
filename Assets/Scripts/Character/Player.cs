using System;
using System.Collections;
using System.Collections.Generic;
using SaveAndLoad;
using UnityEngine;

public class Player : Character, ISavable
{
  [Header("广播")] public VoidEventSO deadEvent;

  protected override void OnEnable()
  {
    base.OnEnable();
    ISavable savable = this;
    savable.RegisterSaveData();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    ISavable savable = this;
    savable.UnregisterSaveData();
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

  public void SaveData(Data data)
  {
    if (data.CharacterPosDict.ContainsKey(name))
    {
      data.CharacterPosDict[name] = transform.position;
    }
    else
    {
      data.CharacterPosDict.Add(name, transform.position);
    }
  }

  public void LoadData(Data data)
  {
    if (data.CharacterPosDict.ContainsKey(name))
    {
      transform.position = data.CharacterPosDict[name];
    }
  }
}