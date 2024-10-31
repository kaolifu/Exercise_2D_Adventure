namespace SaveAndLoad
{
  public interface ISavable
  {
    void RegisterSaveData()
    {
      DataManager.Instance.RegisterSavable(this);
    }

    void UnregisterSaveData()
    {
      DataManager.Instance.UnregisterSavable(this);
    }

    void SaveData(Data data);

    void LoadData(Data data);
  }
}