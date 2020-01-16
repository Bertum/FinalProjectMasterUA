using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject PauseMenu;
    private bool paused;
    private SceneLoaderService sceneLoaderService;
    private PlayerDataController playerDataController;

    private void Awake()
    {
        sceneLoaderService = new SceneLoaderService();
        paused = false;
        PauseMenu.SetActive(false);
        playerDataController = FindObjectOfType<PlayerDataController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    private void PauseUnPause()
    {
        paused = !paused;
        PauseMenu.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void LoadMainMenu()
    {
        playerDataController.Save();
        sceneLoaderService.LoadScene(SceneNames.MAIN_MENU);
    }

    public void ExitGame()
    {
        playerDataController.Save();
        Application.Quit();
    }
}
