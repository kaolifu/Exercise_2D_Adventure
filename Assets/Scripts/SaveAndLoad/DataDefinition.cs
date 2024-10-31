using System;
using Enum;
using UnityEngine;

public class DataDefinition : MonoBehaviour
{
  public PersistentType persistentType;
  public string ID;

  private void OnValidate()
  {
    if (persistentType == PersistentType.ReadWrite)
    {
      if (ID == string.Empty)
      {
        ID = Guid.NewGuid().ToString();
      }
    }
    else
    {
      ID = string.Empty;
    }
  }
}