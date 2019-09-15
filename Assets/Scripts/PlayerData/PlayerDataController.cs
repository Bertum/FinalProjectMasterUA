using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    private SaveLoadService saveLoadService;
    public PlayerData PlayerData;
    //Define if we can save, for example we cannot save in combat
    public bool CanSave;

    private void Awake()
    {
        PlayerData = new PlayerData();
        saveLoadService = new SaveLoadService();
        DontDestroyOnLoad(this.gameObject);
        CanSave = true;
    }

    public void Load()
    {
        PlayerData = saveLoadService.Load();
    }

    public void Save()
    {
        if (CanSave)
        {
            //We will need to add logic here
            saveLoadService.Save(PlayerData);
        }
    }
}
