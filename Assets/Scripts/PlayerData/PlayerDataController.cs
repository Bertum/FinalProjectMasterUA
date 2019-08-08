using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    private SaveLoadService SaveLoadService;
    public PlayerData PlayerData;
    //Define if we can save, for example we cannot save in combat
    public bool CanSave;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        CanSave = true;
    }

    public void Load()
    {
        PlayerData = SaveLoadService.Load();
    }

    public void Save()
    {
        if (CanSave)
        {
            //We will need to add logic here
            SaveLoadService.Save(PlayerData);
        }
    }
}
